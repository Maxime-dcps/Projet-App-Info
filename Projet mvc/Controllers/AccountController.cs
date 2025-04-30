using Projet_mvc.Core.Services;
using Projet_mvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Projet_mvc.Models.User;


namespace Projet_mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var success = await _authService.RegisterUserAsync(model);
            if (!success)
            {
                ModelState.AddModelError("", "Le nom d'utilisateur existe déjà");
                return View();
            }

            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await _authService.AuthenticateAsync(model.Username, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Nom d'utilisateur ou mot de passe incorrect");
                return View();
            }

            var principal = _authService.CreateClaimsPrincipalAsync(user);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    //IsPersistent = model.RememberMe,
                    //ExpiresUtc = DateTime.UtcNow.AddDays(model.RememberMe ? 14 : 1)
                });

            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
