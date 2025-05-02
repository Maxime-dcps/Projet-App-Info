using System.Security.Claims;
using Projet_mvc.Core.Domain;
using Projet_mvc.Core.Repository;
using Projet_mvc.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Projet_mvc.Models.User;

namespace Projet_mvc.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;

        public AuthService(IPasswordHasher passwordHasher, IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }

        public async Task<ResultViewModel> RegisterUserAsync(RegisterUserViewModel registerDto)
        {
            if(await _userRepository.UsernameExist(registerDto.Username))
            {
                return new ResultViewModel
                {
                    Success = false,
                    ErrorMessage = "Cet nom d'utilisateur est déjà utilisé."
                };
            }

            if(await _userRepository.EmailExist(registerDto.Email))
            {
                return new ResultViewModel
                {
                    Success = false,
                    ErrorMessage = "Cette adresse mail est déjà utilisée."
                };
            }

            var (hash, salt) = _passwordHasher.Hash(registerDto.Password);

            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                Password_Hash = hash,
                Salt = salt,
                Role = UserRole.User
            };

            await _userRepository.CreateUserAsync(user);

            return new ResultViewModel
            {
                Success = true,
            };
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null)
                return null;

            bool verified = _passwordHasher.VerifyPassword(password, user.Password_Hash, user.Salt);
            if (!verified)
                return null;

            return user;
        }

        public ClaimsPrincipal CreateClaimsPrincipalAsync(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.User_Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }
    }
}
