using System.ComponentModel.DataAnnotations;

namespace Hypotec.Web.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Password is required")]
        [StringLength(16, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string ReturnToken { get; set; }

        public string Email { get; set; }
    }
}
