using Projet_mvc.Core.Domain;

namespace Projet_mvc.Core.Repository
{
    public interface IFavoriteRepository
    {
        public Task AddAsync(int userId, int listingId);
        public Task RemoveAsync(int userId, int listingId);
        public Task<bool> ExistsAsync(int userId, int listingId);
        public Task<List<Listing>> GetFavoritesForUserAsync(int userId);
    }
}
