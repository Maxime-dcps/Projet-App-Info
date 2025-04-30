using System.Security.Claims;
using Projet_mvc.Core.Domain;
using Projet_mvc.Models;
using Projet_mvc.Models.User;

namespace Projet_mvc.Core.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(RegisterUserViewModel model);
        Task<User?> AuthenticateAsync(string username, string password);
        ClaimsPrincipal CreateClaimsPrincipalAsync(User user);
    }
}
