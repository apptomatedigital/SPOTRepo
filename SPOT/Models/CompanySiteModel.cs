using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPOT.Models
{
    public class CompanySiteModel
    {
        [Required]
        public string SiteName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Website { get; set; }

        public string ContactName { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public int CompanyId { get; set; }
    }
}