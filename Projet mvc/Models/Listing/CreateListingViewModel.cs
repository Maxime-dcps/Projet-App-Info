using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Projet_mvc.Models.Listing
{
    public class CreateListingViewModel
    {
        [Required, MaxLength(100, ErrorMessage = "Votre titre est trop long")]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public List<SelectListItem>? AvailableTags { get; set; }
        public List<int>? SelectedTagIds { get; set; }
    }
}
