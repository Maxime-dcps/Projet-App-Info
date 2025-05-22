using Microsoft.AspNetCore.Mvc;
using Projet_mvc.Core.Repository;

namespace Projet_mvc.Controllers
{
    
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IUserRepository _userRepository;

        public FavoriteController(IFavoriteRepository favoriteRepository, IUserRepository userRepository)
        {
            _favoriteRepository = favoriteRepository;
            _userRepository = userRepository;
        }
        public IActionResult Index()
        { 

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Toggle(int listingId)
        {
            var username = User.Identity?.Name;
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null) 
            {
                return Forbid();
            } 

            var alreadyFavorited = await _favoriteRepository.ExistsAsync(user.User_Id, listingId);

            if (alreadyFavorited) 
            {
                await _favoriteRepository.RemoveAsync(user.User_Id, listingId);
            }
            else
            {
                await _favoriteRepository.AddAsync(user.User_Id, listingId);
            }
               
            return RedirectToAction("Details", "Listings", new { id = listingId });
        }
    }
}
