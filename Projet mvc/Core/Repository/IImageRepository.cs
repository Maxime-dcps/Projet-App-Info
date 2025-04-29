using Projet_mvc.Models;

namespace Projet_mvc.Core.Repository
{
    public interface IImageRepository
    {
        public Task<List<ImageViewModel>> GetImagesByIdAsync(int id);
    }
}
