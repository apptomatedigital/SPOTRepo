using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOT.Models
{
    public class DomainRegisterModel
    {
        public  OrganizationDetailsModel Register {get;set;}
        public SendOTPModel OTPModel { get; set; }
    }
}