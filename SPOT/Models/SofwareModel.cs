using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPOT.Models
{
    public class SofwareModel
    {
        public int SoftwareId { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string SelectSoftwareCategory { get; set; }
        [Required]
        public string OnPremises { get; set; }
        [Required]
        public string Cloud { get; set; }
        [Required]
        public string SoftwareType { get; set; }
        [Required]
        public string Name { get; set; }
    }
}