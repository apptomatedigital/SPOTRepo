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
    public class NetworkController : ApiController
    {
        private SpotEntities db = new SpotEntities();
        public PagingService _pagingservice = new PagingService();
        [HttpPost]
        [Route("api/netwok/sitedomains")]
        public IHttpActionResult CreateSiteNetworkDomain(NetworkDomainStoryModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var sitenetwork = new SiteNetworkDomainstory();
                    sitenetwork.SiteDomain = model.SiteDomain;
                    sitenetwork.NetworkId = model.NetworkId;
                    sitenetwork.SubnetMask = model.SubnetMask;
                    sitenetwork.Gateway = model.Gateway;
                    sitenetwork.VLANNumber = model.VLANNumber;
                    sitenetwork.VLANName = model.VLANName;
                    sitenetwork.DNSServer = model.DNSServer;
                    sitenetwork.DHCPServer = model.DHCPServer;
                    sitenetwork.DHCPIPRanges = model.DHCPIPRanges;
                    db.SiteNetworkDomainstories.Add(sitenetwork);
                    db.SaveChanges();
                    return Ok(sitenetwork);

                }
                else
                    return Ok("Please Enter All the mandatory Fields");
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api/netwok/sitedomains")]
        public IHttpActionResult GetAllSiteNetworkDomains(int pageNumber, int pageSize)
        {
            try
            {

                var sitenetwork = db.SiteNetworkDomainstories.ToList();
                if (sitenetwork != null)
                {
                    int count = sitenetwork.Count();
                    int CurrentPage = pageNumber;
                    int PageSize = pageSize;
                    var returnSiteNetworks = sitenetwork.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    var paginationMetadata = _pagingservice.Pagination(count, CurrentPage, PageSize);

                    HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                    return Ok(returnSiteNetworks);
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
        [Route("api/netwok/sitedomains/{id}")]
        public IHttpActionResult GetSiteNetworkDomainsById(int id)
        {
            try
            {

                var sitenetwork = db.SiteNetworkDomainstories.FirstOrDefault(s => s.DomainStoryId == id);
                if (sitenetwork != null)
                    return Ok(sitenetwork);
                else
                    return Ok("No Records Found");


            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpPost]
        [Route("api/netwok/companydomains")]
        public IHttpActionResult CreateCompanyNetworkDomain(CompanyNetworkDomainModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var networkdomain = new CompanyNetworkDomain();
                    networkdomain.Domain = model.Domain;
                    networkdomain.NetworkTimeServer = model.NetworkTimeServer;
                    networkdomain.CompanyId = model.CompanyId;
                    db.CompanyNetworkDomains.Add(networkdomain);
                    db.SaveChanges();
                    return Ok(networkdomain);

                }
                else
                {
                    return Ok("Fill All the Mandatory Fields");
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api/netwok/companydomains")]
        public IHttpActionResult GetAllCompanyNetworkDomains(int pageNumber, int pageSize)
        {
            try
            {

                var companynetwork = db.CompanyNetworkDomains.ToList();
                if (companynetwork != null)
                {

                    int count = companynetwork.Count();
                    int CurrentPage = pageNumber;
                    int PageSize = pageSize;
                  
                    var returnCompanyNetworks = companynetwork.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    var paginationMetadata = _pagingservice.Pagination(count, CurrentPage, PageSize);

                  
                    HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                    return Ok(returnCompanyNetworks);
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
        [Route("api/netwok/companydomains/{id}")]
        public IHttpActionResult GetCompanyNetworkDomainsById(int id)
        {
            try
            {

                var companynetwork = db.CompanyNetworkDomains.FirstOrDefault(s => s.CNCId == id);
                if (companynetwork != null)
                    return Ok(companynetwork);
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