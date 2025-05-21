using System.ComponentModel.DataAnnotations;

namespace Projet_mvc.Models
{
    public class TagViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom du tag est requis.")]
        public string Label { get; set; }
    }
}
