using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPOT.Models
{
    public class AccountModel
    {
        public int AccountId { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailId { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "MiddleName")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "DisplayName")]
        public string DisplayName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

     
        public bool IsAccountActivated { get; set; }
    }
}