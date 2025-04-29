using Projet_mvc.Core.Domain;

namespace Projet_mvc.Models
{
    public class ListingSummaryViewModel
    {
        public int ListingId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string PrimaryImageUrl { get; set; }
        public string PrimaryImageAlt { get; set; }
    }
}
