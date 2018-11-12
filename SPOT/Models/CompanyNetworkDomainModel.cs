using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPOT.Models
{
    public class CompanyNetworkDomainModel
    {
        public int CNCId { get; set; }

        [Required]
        public string Domain { get; set; }

        [Required]
        public string NetworkTimeServer { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}