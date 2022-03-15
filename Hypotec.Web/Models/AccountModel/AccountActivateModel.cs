using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hypotec.Web.Models
{
    public class AccountActivateModel
    {
        [Required(ErrorMessage = "Password is required")]
        [StringLength(16, ErrorMessage = "Password must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The new password and confirm password should be match.")]
        public string ConfirmPassword { get; set; }
        public string ReturnToken { get; set; }

        public string Email { get; set; }
    }
}
