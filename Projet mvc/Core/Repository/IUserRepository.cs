using Projet_mvc.Core.Domain;

namespace Projet_mvc.Core.Repository
{
    public interface IUserRepository
    {
        Task<int> CreateUserAsync(User user);
        Task<bool> UsernameExist(string username);
        Task<User?> GetByUsernameAsync(string username);
    }
}
