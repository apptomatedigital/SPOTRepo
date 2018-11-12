using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPOT.Models
{
    public class WorkstationInfoModel
    {
        public int WorkStationId { get; set; }
        [Required]
        public string HostName { get; set; }
        [Required]
        public string DNSName { get; set; }
        [Required]
        public string SelectManufacturer { get; set; }
        [Required]
        public string SelectModel { get; set; }
        [Required]
        public string SelectManufacturerPartNumber { get; set; }
        [Required]
        public string SelectOS { get; set; }
        [Required]
        public string SelectOSVersion { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public System.DateTime InstallDate { get; set; }
        [Required]
        public System.DateTime WarrantyStarted { get; set; }
        [Required]
        public System.DateTime WarrantyExpires { get; set; }
        [Required]
        public string SelectServerRoles { get; set; }
        [Required]
        public string Notes { get; set; }
        [Required]
        public string SelectTag { get; set; }
        [Required]
        public string SoftwareType { get; set; }
        [Required]
        public string SoftwareVersion { get; set; }
        [Required]
        public string LicenseKey { get; set; }
        [Required]
        public System.DateTime ExpiresOn { get; set; }
        [Required]
        public int CompanyId { get; set; }
    }
}