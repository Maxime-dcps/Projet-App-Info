using Projet_mvc.Core.Domain;

namespace Projet_mvc.Models.Listing
{
    public class ListingIndexViewModel
    {
        public List<ListingSummaryViewModel> Listings { get; set; }

        public List<TagViewModel> Tags { get; set; }

        public TagViewModel NewTag { get; set; }
    }
}
