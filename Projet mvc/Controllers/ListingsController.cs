using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Create(CreateListingViewModel model)
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
                CreationDate = DateTime.UtcNow
            };

            await _listingRepository.CreateListingAsync(listing);
            return RedirectToAction("Profile", "Account");
        }
    }

}
