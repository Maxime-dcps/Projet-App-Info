using System.ComponentModel.DataAnnotations;

namespace Projet_mvc.Models.User
{
    public class RegisterUserViewModel
    {
        [Required, StringLength(50)]
        public string Username { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(100, MinimumLength = 3)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
