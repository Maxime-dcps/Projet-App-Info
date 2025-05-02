using Microsoft.AspNetCore.Mvc;
using Projet_mvc.Core.Repository;
using Projet_mvc.Models.Listing;

namespace Projet_mvc.Controllers
{
    public class ListingController : Controller
    {

        private readonly IListingRepository _listingRepository;
        private readonly IImageRepository _imageRepository;
        private readonly ITagRepository _tagRepository;

        public ListingController(IListingRepository listingRepository, IImageRepository imageRepository, ITagRepository tagRepository)
        {
            _listingRepository = listingRepository;
            _imageRepository = imageRepository;
            _tagRepository = tagRepository;
        }

        public async Task<IActionResult> Details(int id)
        {
            var listingData = await _listingRepository.GetListingByIdAsync(id);
            if (listingData == null)
            {
                return NotFound();
            }

            var images = await _imageRepository.GetImagesByIdAsync(id);
            var tags = await _tagRepository.GetTagsByIdAsync(id);

            var model = new ListingDetailViewModel
            {
                ListingData = listingData,
                Images = images,
                Tags = tags
            };

            return View(model);
        }
    }
}
