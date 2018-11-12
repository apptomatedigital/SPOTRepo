using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPOT.Models
{
    public class NetworkDomainStoryModel
    {
       
        public string SiteDomain { get; set; }

        [Required]
        public string NetworkId { get; set; }

        [Required]
        public string SubnetMask { get; set; }

        [Required]
        public string Gateway { get; set; }

        public string VLANNumber { get; set; }

        public string VLANName { get; set; }

        [Required]
        public string DNSServer { get; set; }

        [Required]
        public string DHCPServer { get; set; }

        [Required]
        public string DHCPIPRanges { get; set; }
    }
}