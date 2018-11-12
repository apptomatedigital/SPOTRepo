using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPOT.Models
{
    public class GeneratePasswordModel
    {
        [Required]
        public int DeviceId { get; set; }

        public string Password { get; set; }
    }
}