using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projet_mvc.Core.Repository;
using Projet_mvc.Models;
using Projet_mvc.Models.Home;

namespace Projet_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IListingRepository _listingRepository;
        private readonly ITagRepository _tagRepository;
<<<<<<< HEAD
        private readonly IFavoriteRepository _favoriteRepository;

        public HomeController(IListingRepository listingRepository, ITagRepository tagRepository, ILogger<HomeController> logger, IFavoriteRepository favoriteRepository)
=======

        public HomeController(IListingRepository listingRepository, ITagRepository tagRepository, ILogger<HomeController> logger)
>>>>>>> 6ee7f94ca1c0bda8cd9255815e500fa4d2f2cba0
        {
            _listingRepository = listingRepository;
            _tagRepository = tagRepository;
            _logger = logger;
            _favoriteRepository = favoriteRepository;

        }

        public async Task<IActionResult> Index()
        {
            var recentListings = await _listingRepository.GetRecentListingsAsync(5);
            var popularListing = await  _listingRepository.GetPopularListingsAsync(5);
            var popularTags = await _tagRepository.GetPopularTagsAsync(8);
<<<<<<< HEAD
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            int? currentUserId = null;
=======
>>>>>>> 6ee7f94ca1c0bda8cd9255815e500fa4d2f2cba0

            if (userIdClaim != null && int.TryParse(userIdClaim, out int parsedUserId))
            {
                currentUserId = parsedUserId;
            }

            List<int> userFavorites = new();

            if (currentUserId.HasValue)
            {
                var favListings = await _favoriteRepository.GetFavoritesForUserAsync(currentUserId.Value);
                userFavorites = favListings.Select(f => f.Id).ToList();
            }

            foreach (var listing in recentListings)
            {
                listing.IsFavorited = userFavorites.Contains(listing.ListingId);
            }

            foreach (var listing in popularListing)
            {
                listing.IsFavorited = userFavorites.Contains(listing.ListingId);
            }
            var viewModel = new HomePageViewModel
            {
                RecentListings = recentListings,
                PopularListings = popularListing,
                PopularTags = popularTags
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
