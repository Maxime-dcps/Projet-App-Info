using Projet_mvc.Models;

namespace Projet_mvc.Core.Repository
{
    public interface IListingRepository
    {
        public Task<List<ListingSummaryViewModel>> GetRecentListingsAsync(int count);
    }
}
