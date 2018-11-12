using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPOT.Models
{
    public class RouterInfoModel
    {
        [Required]
        public string HostName { get; set; }

        [Required]
        public string DNSName { get; set; }

        [Required]
        public string DeviceType { get; set; }

        [Required]
        public string SelectManufacturer { get; set; }

        [Required]
        public string ModelType { get; set; }

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
        public string UploadRouterConfiguration { get; set; }

        [Required]
        public string Notes { get; set; }

        [Required]
        public string Tag { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}