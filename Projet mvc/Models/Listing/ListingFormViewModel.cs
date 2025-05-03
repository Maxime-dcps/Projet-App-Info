using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Projet_mvc.Models.Listing
{
    public class ListingFormViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le titre est obligatoire"), MaxLength(100, ErrorMessage = "Votre titre est trop long")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Le titre est obligatoire"), Range(0.01, 100000, ErrorMessage = "Le prix doit être positif")]
        public decimal Price { get; set; }
        public List<SelectListItem>? AvailableTags { get; set; }
        public List<int>? SelectedTagIds { get; set; }
    }
}
