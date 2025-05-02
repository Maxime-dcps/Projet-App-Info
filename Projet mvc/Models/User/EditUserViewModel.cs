using System.ComponentModel.DataAnnotations;

namespace Projet_mvc.Models.User
{
    public class EditUserViewModel
    {
        public int User_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(100)]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [StringLength(100)]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string? ConfirmPassword { get; set; }
    }
}
