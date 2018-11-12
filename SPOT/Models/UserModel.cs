using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPOT.Models
{
    public class UserModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string PrimaryPhone { get; set; }

        public string Extension { get; set; }

        public string AlternatePhone { get; set; }

        public string Title { get; set; }

        public string Department { get; set; }

        [Required]
        public bool IsPrimaryContact { get; set; }

        public string Notes { get; set; }
    }
}