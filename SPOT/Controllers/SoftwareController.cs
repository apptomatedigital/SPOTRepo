using SPOT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SPOT.Controllers
{
    public class SoftwareController : ApiController
    {
        private SpotEntities db = new SpotEntities();
        [HttpPost]
        [Route("api/software/softwarepackages")]
        public IHttpActionResult CreateSoftwarePackage(SoftwarePackageModel model)
        {
            try
            {
                var packageexists = db.SoftwarePackageDetails.Where(s => s.PackageName == model.PackageName).FirstOrDefault();
                if (packageexists == null)
                {
                    var softwarepackage = new SoftwarePackageDetail();
                    softwarepackage.PackageName = model.PackageName;
                    db.SoftwarePackageDetails.Add(softwarepackage);
                    db.SaveChanges();
                    return Ok(softwarepackage);
                }
                else
                    return Ok("" + model.PackageName + " is Already Taken, Take another one");

            }
            catch(Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpPost]
        [Route("api/software/createsoftware")]
        public IHttpActionResult CreateSoftware(SofwareModel model)
        {
            try
            {
                var chksoftwarename = db.SoftwarePackageDetails.Where(s => s.PackageName == model.Name).FirstOrDefault();
                if (chksoftwarename != null)
                {
                    var software = new SoftwareDetail();
                    software.Manufacturer = model.Manufacturer;
                    software.SelectSoftwareCategory = model.SelectSoftwareCategory;
                    software.OnPremises = model.OnPremises;
                    software.Cloud = model.Cloud;
                    software.SoftwareType = model.SoftwareType;
                    software.Name = model.Name;
                    db.SoftwareDetails.Add(software);
                    db.SaveChanges();
                    return Ok(software);
                }
                else
                    return Ok("Software Name " + model.Name + " Does not exists in Software Package.");

            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
    }
}