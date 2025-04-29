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

        public HomeController(IListingRepository listingRepository, ILogger<HomeController> logger)
        {
            _listingRepository = listingRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var recentListings = await _listingRepository.GetRecentListingsAsync(5);

            var viewModel = new HomePageViewModel
            {
                // Assigne les listes récupérées (le service retourne déjà le bon type de ViewModel)
                PopularListings = recentListings.ToList(), // Utilise les listings récents ici
            };

            return View();
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
