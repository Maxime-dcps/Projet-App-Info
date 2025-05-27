using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Projet_mvc.Models.Listing
{
    public class ListingFilterViewModel
    {
        [StringLength(100, ErrorMessage = "Maximum 100 caractères.")]
        public string? SearchTerm   { get; set; }
        public string? SortOrder { get; set; }
        [Range(0, 1000000, ErrorMessage = "Le prix minimum doit être positif.")]
        public decimal? MinPrice { get; set; }
        [Range(0, 1000000, ErrorMessage = "Le prix maximum doit être positif.")]
        public decimal? MaxPrice { get; set; }
        public List<int>? SelectedTagIds { get; set; }
        public List<SelectListItem>? AvailableTags { get; set; }
    }
}
    