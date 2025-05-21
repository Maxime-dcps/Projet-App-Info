using Projet_mvc.Core.Services;
using Projet_mvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Projet_mvc.Models.User;
using Microsoft.AspNetCore.Authorization;
using Projet_mvc.Core.Repository;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Projet_mvc.Core.Domain;


namespace Projet_mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IPasswordHasher _passwordHasher;

        private readonly IListingRepository _listingRepository;

        public AccountController(IUserRepository userRepository, IAuthService authService, IPasswordHasher passwordHasher, IListingRepository listingRepository)
        {
            _userRepository = userRepository;
            _authService = authService;
            _passwordHasher = passwordHasher;
            _listingRepository = listingRepository;
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

            var result = await _authService.RegisterUserAsync(model);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.ErrorMessage);

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

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);


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


        //Affichage du profil
        [Authorize] // S'assure que seul un utilisateur connecté peut accéder
        public async Task<IActionResult> Profile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("Login");
            }

            if (!int.TryParse(userIdClaim.Value, out var userId))
            {
                return RedirectToAction("Login");
            }
                

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var userListings = await _listingRepository.GetListingsByUserIdAsync(userId);

            var viewModel = new UserViewModel
            {
                User_Id = user.User_Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Creation_Date = user.Creation_Date,
                User = user,
                Listings = userListings.ToList()
            };

            return View(viewModel);
        }

        // Page d'édition du profil
        [HttpGet]
        public async Task<IActionResult> Edit()
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))                            
            { 
                return RedirectToAction("Login"); 
            }

            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            { 
                return NotFound(); 
            }

            var model = new EditUserViewModel
            {
                User_Id = user.User_Id,
                Username = user.Username,
                Email = user.Email
            };

            return View(model);
        }

        // Enregistrement des modifications
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {

            if (!ModelState.IsValid) return View(model);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
                return RedirectToAction("Login");
           

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return NotFound();

            user.Username = model.Username;
            user.Email = model.Email;

            if (!string.IsNullOrWhiteSpace(model.NewPassword))
            {
                var (hash, salt) = _passwordHasher.Hash(model.NewPassword);
                user.Password_Hash = hash;
                user.Salt = salt;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.User_Id.ToString()),  // <-- ID de l'utilisateur
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role ?? "User")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            
            var updatedUser = await _userRepository.UpdateUserAsync(user);

            if (updatedUser == null)
            {
                return View(model);  // Retourne à la vue si la mise à jour a échoué
            }

            return RedirectToAction("Profile", "Account");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteAccount()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Account");

            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
                return NotFound();

            // Suppression de l'utilisateur
            var deleted = await _userRepository.DeleteUserAsync(user.User_Id);
            if (deleted)
            {
                // Déconnexion de l'utilisateur
                await HttpContext.SignOutAsync();

                // Rediriger vers la page d'accueil après la suppression
                return RedirectToAction("Index", "Home");
            }

            // Si la suppression échoue
            ModelState.AddModelError("", "Une erreur est survenue lors de la suppression de votre compte.");
            return RedirectToAction("Profile", "Account");
        }

    }

}
