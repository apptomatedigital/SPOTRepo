using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPOT.Models
{
   
    public class CompanyDetailsModel
    {
        public int CompanyId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string StateName { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Website { get; set; }

        [Required]
        public string ContactName { get; set; }


        [Required]
        public string Title { get; set; }


        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }


        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string SiteInfo { get; set; }

        [Required]
        public string SiteName { get; set; }

        [Required]
        public int OrganizationId { get; set; }

        const int maxPageSize = 20;

     

        //public int pageSize
        //{

        //    get { return _pageSize; }
        //    set
        //    {
        //        _pageSize = (value > maxPageSize) ? maxPageSize : value;
        //    }
        //}
    }
  
}