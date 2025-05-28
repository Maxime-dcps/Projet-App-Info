using Projet_mvc.Core.Domain;
using Projet_mvc.Models;

namespace Projet_mvc.Core.Repository
{
    public interface IImageRepository
    {
        public Task<List<ImageViewModel>> GetImagesByIdAsync(int listingid);
        public Task AddImageAsync(Image image);
        public Task<int> GetLastImageOrderAsync(int listingId);
        public Task<ImageViewModel?> GetImageByImageIdAsync(int imageId);
        public Task DeleteImageAsync(int imageId);
    }
}
