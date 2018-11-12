using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOT.Models.SwaggerModels
{
    public class AccountViewModel
    {
        public string EmailId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string DisplayName { get; set; }

        public string Password { get; set; }

        public bool IsAccountActivated { get; set; }
    }
}