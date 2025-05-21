using Microsoft.AspNetCore.Mvc;

namespace Projet_mvc.Controllers
{
    public class FavoriteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
