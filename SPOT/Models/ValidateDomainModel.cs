using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPOT.Models
{
    public class ValidateDomainModel
    {
        public int DomainId { get; set; }

        [Required]
        public string DomailName { get; set; }
    }
}