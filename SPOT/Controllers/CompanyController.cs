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
  

    public class CompanyController : ApiController
    {
        private SpotEntities db = new SpotEntities();
        public  PagingService _pagingservice =new PagingService();
        //public CompanyController() { }

        //public CompanyController(IPagingService pagingservice)
        //{
        //    _pagingservice = pagingservice;
        //}

        [HttpPost]
        [Route("api/company/registrations")]
        public IHttpActionResult CompanyRegistration(CompanyDetailsModel companydetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var companyexs = db.CompanyDetails.Where(s => s.CompanyName.ToLower() == companydetail.CompanyName.ToLower()).FirstOrDefault();
                    if (companyexs == null)
                    {
                        var company = new CompanyDetail();
                        company.CompanyName = companydetail.CompanyName;
                        company.Address = companydetail.Address;
                        company.City = companydetail.City;
                        company.StateName = companydetail.StateName;
                        company.PostalCode = companydetail.PostalCode;
                        company.Phone = companydetail.Phone;
                        company.Website = companydetail.Website;
                        company.ContactName = companydetail.ContactName;
                        company.Title = companydetail.Title;
                        company.EmailAddress = companydetail.EmailAddress;
                        company.PhoneNumber = companydetail.PhoneNumber;
                        company.SiteInfo = companydetail.SiteInfo;
                        company.SiteName = companydetail.SiteName;
                        company.OrganizationId = companydetail.OrganizationId;
                        db.CompanyDetails.Add(company);
                        db.SaveChanges();
                        return Ok(company);
                    }
                    else
                        return Ok("Company Name Already Taken.");
                }
                else
                {
                    return Ok("Need to Fill All the Field Except CompanyId ");
                }
            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpGet]
        [Route("api/company/companies")]
        public IHttpActionResult GetAllCompanies(int pageNumber,int pageSize)
        {
            try
            {

                var CompanyDetails = db.CompanyDetails.ToList();
                if (CompanyDetails != null)
                {
                    int count = CompanyDetails.Count();
                    int CurrentPage = pageNumber;
                    int PageSize = pageSize;
                    var retunCompanyDetails = CompanyDetails.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    var paginationMetadata = _pagingservice.Pagination(count, CurrentPage, PageSize);
                    HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                    return Ok(retunCompanyDetails);

                }
                else
                    return Ok("No Records Found");


            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpGet]
        [Route("api/company/companies/{id}")]
        public IHttpActionResult GetCompanyById(int id)
        {
            try
            {

                var CompanyDetails = db.CompanyDetails.FirstOrDefault(s => s.CompanyId == id);
                if (CompanyDetails != null)
                    return Ok(CompanyDetails);
                else
                    return Ok("No Records Found");


            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
    }
}