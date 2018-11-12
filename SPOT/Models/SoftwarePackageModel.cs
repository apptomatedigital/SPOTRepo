using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPOT.Models
{
    public class SoftwarePackageModel
    {
        public int PackageId { get; set; }

        [Required]
        public string PackageName { get; set; }
    }
}