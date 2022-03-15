using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hypotec.Web.Models
{
    public class UserModel
    {
        [RegularExpression(@".*\S+.*$", ErrorMessage = "Field cannot be blank")]
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [RegularExpression(@".*\S+.*$", ErrorMessage = "Field cannot be blank")]
        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email address is not valid")]
        [Remote(action: "IsEmailInUse", controller: "Account", ErrorMessage = "Email already exist")]
        public string Email { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Confirm Email required")]
        [Compare("Email", ErrorMessage = "Email doesn't match.")]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }
        [NotMapped]
        public bool TermAndCondition { get; set; }
        //[Required(ErrorMessage = "Email is required")]
        //[EmailAddress(ErrorMessage = "Email address is not valid")]
        ////[Remote(action: "IsEmailInUse", controller: "Account", AdditionalFields = "initialEmail")]
        //public string ConfirmEmail { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password, ErrorMessage = "Password is not valid")]
        [Remote(action: "ValidatePassword", controller: "Account")]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Primary number is not valid.")]
        [Required(ErrorMessage = "Primary Phone is required")]
        [Display(Name = "Primary Phone")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please select  the property state")]
        [Display(Name = "Property State")]
        public string PropertyState { get; set; }
        [Display(Name = "How did you hear about us?")]
        public string HowDidYouAboutUs { get; set; }
        public string SecurityStamp { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }


    }
}
