using Projet_mvc.Core.Domain;
using Projet_mvc.Models;

namespace Projet_mvc.Core.Repository
{
    public interface ITagRepository
    {
        public Task<List<TagViewModel>> GetTagsByIdAsync(int id);
        public Task<List<TagViewModel>> GetAllTagsAsync();
        Task<int> CreateTagsAsync(Tags tags);
        Task<bool> TagExistsAsync(string label);
        public Task AddTagsToListingAsync(int newListingId, List<int> selectedTagsIds);
        public Task UpdateTagsToListingAsync(int listingId, List<int> selectedTagsIds);
    }
}
