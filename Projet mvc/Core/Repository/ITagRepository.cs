using Projet_mvc.Models;

namespace Projet_mvc.Core.Repository
{
    public interface ITagRepository
    {
        public Task<List<TagViewModel>> GetTagsByIdAsync(int id);
        public Task<List<TagViewModel>> GetAllTagsAsync();
        public Task AddTagsToListingAsync(int newListingId, List<int> selectedTagsIds);
    }
}
