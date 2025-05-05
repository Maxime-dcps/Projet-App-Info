using Projet_mvc.Core.Domain;
using Projet_mvc.Models;

namespace Projet_mvc.Core.Repository
{
    public interface IImageRepository
    {
        public Task<List<ImageViewModel>> GetImagesByIdAsync(int id);

        public Task AddImageAsync(Image image);
    }
}
