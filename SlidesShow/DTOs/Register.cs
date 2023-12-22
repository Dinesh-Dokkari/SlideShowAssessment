using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SlidesShow.DTOs
{
    public class Register
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "first name is required"), MaxLength(50)]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "email is required")]
        [EmailAddress(ErrorMessage = "enter valid email address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "phone number is required"), MinLength(10, ErrorMessage = "enter correct phone number"), MaxLength(10, ErrorMessage = "enter correct phone number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "password is required")]
        [MaxLength(16, ErrorMessage = "password less than 16 characters")]
        [MinLength(6, ErrorMessage = "need atleast 6 characters")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;


        [NotMapped]
        [Required(ErrorMessage = "confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "password and confirm password are not match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
