using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPOT.Models
{
    public class OrganizationDetailsModel
    {
        public int OrganizationId { get; set; }

        [Required]
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [EmailAddress]
        [Required]
        [Display(Name = "Email Address")]
        public string EmailId { get; set; }

        public string Otptype { get; set; }
        public string OTP { get; set; }
        public string CreatedBy { get; set; }
    }
    public class SendOTPModel
    {
        public int OrganizationId { get; set; }

        [Required]
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [EmailAddress]
        [Required]
        [Display(Name = "Email Address")]
        public string EmailId { get; set; }

    }
    public class OrganizationLogin
    {
        public int OrganizationId { get; set; }

        [EmailAddress]
        [Required]
        [Display(Name = "Email Address")]
        public string EmailId { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
