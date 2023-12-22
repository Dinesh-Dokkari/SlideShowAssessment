using System.ComponentModel.DataAnnotations;

namespace SlidesShow.DTOs
{
    public class LoginDetails
    {
        [Required(ErrorMessage = "Username required")]
        [EmailAddress(ErrorMessage = "Enter proper email id")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
