using Projet_mvc.Core.Domain;
using Projet_mvc.Models;

namespace Projet_mvc.Core.Repository
{
    public interface IListingRepository
    {
        public Task<List<ListingSummaryViewModel>> GetRecentListingsAsync(int count);
        public Task<Listing> GetListingByIdAsync(int id);
        Task<int> CreateListingAsync(Listing listing);
        public Task<IEnumerable<Listing>> GetListingsByUserIdAsync(int userId);
        public Task<int> UpdateListingAsync(Listing listing);
    }
}
