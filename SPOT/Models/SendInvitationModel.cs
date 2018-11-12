using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SPOT.Models
{
    public class SendInvitationModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public System.DateTime ExpiryDate { get; set; }
        public bool IsRegistered { get; set; }
    }
}