using Projet_mvc.Core.Domain;

namespace Projet_mvc.Core.Repository
{
    public interface IUserRepository
    {
        Task<int> CreateUserAsync(User user);
        Task<bool> UsernameExist(string username);
        Task<bool> EmailExist(string email);
        Task<User?> GetByUsernameAsync(string username);

        Task<User?> GetByIdAsync(int userId);

        Task<User?> UpdateUserAsync(User user);

        Task<bool> DeleteUserAsync(int userId);
    }
}
