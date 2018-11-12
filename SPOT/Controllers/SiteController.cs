using Newtonsoft.Json;
using SPOT.Models;
using SPOT.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SPOT.Controllers
{
    public class SiteController : ApiController
    {
        private SpotEntities db = new SpotEntities();
        public PagingService _pagingservice = new PagingService();
        [HttpPost]
        [Route("api/sites/companysites")]
        public IHttpActionResult CreateCompanySite(CompanySiteModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var companysite = db.CompanySites.FirstOrDefault(s => s.SiteName == model.SiteName);
                    if (companysite == null)
                    {
                        var addcompanysite = new CompanySite();
                        addcompanysite.SiteName = model.SiteName;
                        addcompanysite.Address = model.Address;
                        addcompanysite.City = model.City;
                        addcompanysite.State = model.State;
                        addcompanysite.PostalCode = model.PostalCode;
                        addcompanysite.Phone = model.Phone;
                        addcompanysite.Website = model.Website;
                        addcompanysite.ContactName = model.ContactName;
                        addcompanysite.EmailAddress = model.EmailAddress;
                        addcompanysite.PhoneNumber = model.PhoneNumber;
                        addcompanysite.CompanyId = model.CompanyId;
                        db.CompanySites.Add(addcompanysite);
                        db.SaveChanges();
                        return Ok(addcompanysite);
                    }
                    else
                    {
                        return Ok("Site Name Already Exists");
                    }
                }
                else
                {
                    return Ok("Fill All the Mandatory Fields");
                }
            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpGet]
        [Route("api/sites/companysites")]
        public IHttpActionResult GetAllCompanySites(int pageNumber, int pageSize)
        {
            try
            {
                var companysites = db.CompanySites.ToList();
                if (companysites != null)
                {
                    int count = companysites.Count();
                    int CurrentPage = pageNumber;
                    int PageSize = pageSize;
                    var returnCompanySites = companysites.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    var paginationMetadata = _pagingservice.Pagination(count, CurrentPage, PageSize);
                   
                    HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                    return Ok(returnCompanySites);
                }
                else
                {
                    return Ok("No Records Found");
                }

            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpGet]
        [Route("api/sites/companysites/{id}")]
        public IHttpActionResult GetCompanySitesById(int companyid)
        {
            try
            {
                var companysites = db.CompanySites.Where(s => s.CompanyId == companyid).FirstOrDefault();
                if (companysites != null)
                {
                    return Ok(companysites);
                }
                else
                {
                    return Ok("No Records Found");
                }

            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
    }
}