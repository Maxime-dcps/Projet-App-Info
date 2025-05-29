using Microsoft.AspNetCore.Mvc;
using Projet_mvc.Core.Repository;
using System.Security.Claims;


namespace Projet_mvc.Controllers
{
    
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IUserRepository _userRepository;
        private readonly IListingRepository _listingRepository;

        public FavoriteController(IFavoriteRepository favoriteRepository, IUserRepository userRepository, IListingRepository listingRepository)
        {
            _favoriteRepository = favoriteRepository;
            _userRepository = userRepository;
            _listingRepository = listingRepository;

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
            if (!User.Identity.IsAuthenticated) { 
                return Unauthorized();
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var listing = await _listingRepository.GetListingByIdAsync(listingId);
            if (listing == null)
            {
                return NotFound();
            }

            if (user == null || listing.UserId == userId) 
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

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
