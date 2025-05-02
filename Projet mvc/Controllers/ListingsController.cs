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

        public ListingsController(IListingRepository listingRepository, IUserRepository userRepository)
        {
            _listingRepository = listingRepository;
            _userRepository = userRepository;
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
                creationDate = DateTime.UtcNow
            };

            await _listingRepository.CreateListingAsync(listing);
            return RedirectToAction("Profile", "Account");
        }
    }

}
