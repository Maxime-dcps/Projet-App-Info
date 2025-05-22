using Projet_mvc.Core.Domain;

namespace Projet_mvc.Core.Repository
{
    public interface IUserRepository
    {
        public Task<int> CreateUserAsync(User user);
        public Task<bool> UsernameExist(string username);
        public Task<bool> EmailExist(string email);
        Task<User?> GetByUsernameAsync(string username);

        public Task<User?> GetByIdAsync(int userId);

        public Task<User?> UpdateUserAsync(User user);

        public Task<bool> DeleteUserAsync(int userId);
    }
}
