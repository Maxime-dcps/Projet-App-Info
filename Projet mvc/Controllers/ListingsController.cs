using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projet_mvc.Core.Domain;
using Projet_mvc.Core.Repository;
using Projet_mvc.Models;
using Projet_mvc.Models.Listing;

namespace Projet_mvc.Controllers
{
    
    public class ListingsController : Controller
    {
        private readonly IListingRepository _listingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IImageRepository _imageRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ListingsController(IListingRepository listingRepository, IUserRepository userRepository, IImageRepository imageRepository, ITagRepository tagRepository, IWebHostEnvironment webHostEnvironment)
        {
            _listingRepository = listingRepository;
            _userRepository = userRepository;
            _imageRepository = imageRepository;
            _tagRepository = tagRepository;
            _webHostEnvironment = webHostEnvironment; // used for image upload
        }

        public async Task<IActionResult> Details(int id)
        {
            var listingData = await _listingRepository.GetListingByIdAsync(id);
            if (listingData == null)
            {
                return NotFound();
            }

            var images = await _imageRepository.GetImagesByIdAsync(id);
            var tags = await _tagRepository.GetTagsByIdAsync(id);

            var model = new ListingDetailViewModel
            {
                ListingData = listingData,
                Images = images,
                Tags = tags
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var allTags = await _tagRepository.GetAllTagsAsync();

            var model = new ListingFormViewModel
            {
                AvailableTags = allTags.Select(tag => new SelectListItem
                {
                    Value = tag.Id.ToString(),
                    Text = tag.Label
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        
        public async Task<IActionResult> Create(ListingFormViewModel model)
        {

            if(model == null)
            {
                ModelState.Clear();

                var errorViewModel = new ListingFormViewModel();

                ModelState.AddModelError("", "Échec de l'envoi : Veuillez réessayer avec moins d'images ou des images plus petites.");

                await RePopulateAvailableTags(errorViewModel);

                return View(errorViewModel);
            }

            var username = User.Identity?.Name;
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null) return RedirectToAction("Login", "Account");

            ValidateUploadedImages(model.Images); // Validate the uploaded images

            if (!ModelState.IsValid)
            {
                await RePopulateAvailableTags(model);
                return View(model);
            }


            var listing = new Listing
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                UserId = user.User_Id,
                IsAvailable = true,
                CreationDate = DateTime.Now
            };

            var newListingId = await _listingRepository.CreateListingAsync(listing);

            if (model.SelectedTagIds != null && model.SelectedTagIds.Any())
            {
                await _tagRepository.AddTagsToListingAsync(newListingId, model.SelectedTagIds);
            }

            if (model.Images != null && model.Images.Any())
            {
                await SaveUploadedImagesAsync(model.Images, newListingId);

            }

                return RedirectToAction("Profile", "Account");
        }

        private async Task RePopulateAvailableTags(ListingFormViewModel model)
        {
            if (model.AvailableTags == null || !model.AvailableTags.Any())
            {
                var allTags = await _tagRepository.GetAllTagsAsync();

                var selectedTagIds = model.SelectedTagIds ?? new List<int>();

                model.AvailableTags = allTags.Select(tag => new SelectListItem
                {
                    Value = tag.Id.ToString(),
                    Text = tag.Label,
                    Selected = selectedTagIds.Contains(tag.Id)
                }).ToList();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var authorizationResult = await CheckListingAuthorizationAsync(id);
            if (authorizationResult != null)
            {
                return authorizationResult;
            }

            var listing = await _listingRepository.GetListingByIdAsync(id);

            var allTags = await _tagRepository.GetAllTagsAsync();

            var selectedTags = await _tagRepository.GetTagsByIdAsync(id);

            var model = new ListingFormViewModel
            {
                Id = listing.Id,
                Title = listing.Title,
                Description = listing.Description,
                Price = listing.Price,

                AvailableTags = allTags.Select(tag => new SelectListItem
                {
                    Value = tag.Id.ToString(),
                    Text = tag.Label,
                    Selected = selectedTags.Any(t => t.Id == tag.Id)
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ListingFormViewModel model)
        {
            var authorizationResult = await CheckListingAuthorizationAsync(model.Id);
            if (authorizationResult != null)
            {
                return authorizationResult;
            }

            var listing = await _listingRepository.GetListingByIdAsync(model.Id);

            if (!ModelState.IsValid)
                return View(model);

            listing.Title = model.Title;
            listing.Description = model.Description;
            listing.Price = model.Price;

            await _listingRepository.UpdateListingAsync(listing);

            if (model.SelectedTagIds != null && model.SelectedTagIds.Any())
            {
                await _tagRepository.UpdateTagsToListingAsync(model.Id, model.SelectedTagIds);
            }
            return RedirectToAction("Profile", "Account");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var authorizationResult = await CheckListingAuthorizationAsync(id);
            if (authorizationResult != null)
            {
                return authorizationResult;
            }

            await _listingRepository.DeleteListingAsync(id);

            return RedirectToAction("Index", "Home");
        }

        private void ValidateUploadedImages(List<IFormFile>? images)
        {
            long maxFileSize = 5 * 1024 * 1024; // 5 MB
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" , ".webp"};

            if (images != null && images.Any())
            {
                if (images.Count > 5)
                {
                    ModelState.AddModelError("Images", "Vous ne pouvez pas télécharger plus de 5 images.");
                }

                foreach (var file in images)
                {
                    if (file.Length == 0)
                    {
                        ModelState.AddModelError("Images", $"Le fichier {file.FileName} est vide.");
                    }

                    if (file.Length > maxFileSize)
                    {
                        ModelState.AddModelError("Images", $"La taille de l'image {file.FileName} ne doit pas dépasser 5 Mo.");
                    }

                    var fileExtension = Path.GetExtension(file.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("Images", $"Image non valide {file.FileName} (Formats autorisés : jpg, jpeg, png).");
                    }
                }
            }
        }

        private async Task SaveUploadedImagesAsync(List<IFormFile> images, int listingId)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "uploads"); // Absolute path to the uploads folder

            int imageOrder = 0;

            foreach (var file in images)
            {

                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                var uniqueFileName = $"{Guid.NewGuid()}{extension}";
                var fullPhysicalPath = Path.Combine(uploadsFolder, uniqueFileName);

                await using (var stream = new FileStream(fullPhysicalPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var relativePath = $"/images/uploads/{uniqueFileName}";

                var imageEntity = new Image
                {
                    ListingId = listingId,
                    FilePath = relativePath,
                    AltText = Path.GetFileNameWithoutExtension(file.FileName),
                    ImageOrder = imageOrder++,
                    UploadDate = DateTime.Now
                };

                await _imageRepository.AddImageAsync(imageEntity);
            }
        }

        private async Task<IActionResult?> CheckListingAuthorizationAsync(int listingId)
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
            {
                return Forbid();
            }

            var listing = await _listingRepository.GetListingByIdAsync(listingId);
            if (listing == null)
            {
                return NotFound();
            }

            if (listing.UserId != user.User_Id && user.Role != "Admin")
            {
                return Forbid();
            }

            return null;
        }
    }
}
