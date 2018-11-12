using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SPOT.Models
{
    public class NetworkInterfaceModel
    {
        [Required]
        public string HostName { get; set; }

        [Required]
        public string SelectManufacturer { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string OS { get; set; }

        [Required]
        public string OSVersion { get; set; }

        [Required]
        public string ManufacturerPartNumber { get; set; }

        [Required]
        public string SerialNumber { get; set; }

        [Required]
        public System.DateTime InstallDate { get; set; }

        [Required]
        public System.DateTime WarrantyStarted { get; set; }

        [Required]
        public System.DateTime WarrantyExpires { get; set; }

        [Required]
        public string UploadNetworkSwitchConfiguration { get; set; }

        [Required]
        public string Notes { get; set; }

        [Required]
        public string SelectTag { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}