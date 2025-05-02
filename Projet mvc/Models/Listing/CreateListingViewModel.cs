using System.ComponentModel.DataAnnotations;

namespace Projet_mvc.Models.Listing
{
    public class CreateListingViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
