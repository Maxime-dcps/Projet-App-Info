using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projet_mvc.Core.Domain;
using Projet_mvc.Core.Repository;
using Projet_mvc.Models.Listing;

namespace Projet_mvc.Controllers
{
    
    public class ListingsController : Controller
    {
        private readonly IListingRepository _listingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IImageRepository _imageRepository;
        private readonly ITagRepository _tagRepository;

        public ListingsController(IListingRepository listingRepository, IUserRepository userRepository, IImageRepository imageRepository, ITagRepository tagRepository)
        {
            _listingRepository = listingRepository;
            _userRepository = userRepository;
            _imageRepository = imageRepository;
            _tagRepository = tagRepository;
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
            if (!ModelState.IsValid)
                return View(model);

            var username = User.Identity?.Name;
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null)
                return RedirectToAction("Login", "Account");

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

                return RedirectToAction("Profile", "Account");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var username = User.Identity?.Name;
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null)
                return RedirectToAction("Login", "Account");

            var listing = await _listingRepository.GetListingByIdAsync(id);

            if (listing == null)
                return NotFound();

            if (listing.UserId != user.User_Id && user.Role != "Admin")
                return Forbid();

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
            var username = User.Identity?.Name;
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null)
                return RedirectToAction("Login", "Account");

            var listing = await _listingRepository.GetListingByIdAsync(model.Id);

            if (listing == null)
                return NotFound();

            if (listing.UserId != user.User_Id && user.Role != "Admin")
                return Forbid();

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
    }

}
