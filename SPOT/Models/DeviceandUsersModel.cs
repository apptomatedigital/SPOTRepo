using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPOT.Models
{
    public class DeviceandUsersModel
    {
        [Required]
        public int DeviceId { get; set; }

        [Required]
        public int UserId { get; set; }

        public string FirstName { get; set; }
    }
}