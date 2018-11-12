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
    public class DeviceController : ApiController
    {
        private SpotEntities db = new SpotEntities();
        public PagingService _pagingservice = new PagingService();
        [HttpPost]
        [Route("api/device/switches")]
        public IHttpActionResult CreateSwitchInfo(SwitchInfoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    var switchinformation = new SwitchInfo();
                    switchinformation.HostName = model.HostName;
                    switchinformation.SelectManufacturer = model.SelectManufacturer;
                    switchinformation.Model = model.Model;
                    switchinformation.OS = model.OS;
                    switchinformation.OSVersion = model.OSVersion;
                    switchinformation.ManufacturerPartNumber = model.ManufacturerPartNumber;
                    switchinformation.SerialNumber = model.SerialNumber;
                    switchinformation.InstallDate = model.InstallDate;
                    switchinformation.WarrantyStarted = model.WarrantyStarted;
                    switchinformation.WarrantyExpires = model.WarrantyExpires;
                    switchinformation.UploadNetworkSwitchConfiguration = model.UploadNetworkSwitchConfiguration;
                    switchinformation.Notes = model.Notes;
                    switchinformation.SelectTag = model.SelectTag;
                    switchinformation.CompanyId = model.CompanyId;
                    db.SwitchInfoes.Add(switchinformation);
                    db.SaveChanges();
                    return Ok(switchinformation);
                }
                else
                    return Ok("Please Enter All the mandatory Fields");
            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpGet]
        [Route("api/device/switches")]
        public IHttpActionResult GetAllSwitches(int pageNumber, int pageSize)
        {
            try
            {

                var switchDetails = db.SwitchInfoes.ToList();
                if (switchDetails != null)
                {
                    int count = switchDetails.Count();
                    int CurrentPage = pageNumber;
                    int PageSize = pageSize;
                    var returnSwitchDetails = switchDetails.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    var paginationMetadata = _pagingservice.Pagination(count, CurrentPage, PageSize);
                    HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                    return Ok(returnSwitchDetails);
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
        [Route("api/device/switches/{id}")]
        public IHttpActionResult GetSwitchById(int id)
        {
            try
            {

                var switchDetails = db.SwitchInfoes.FirstOrDefault(s => s.Id == id);
                if (switchDetails != null)
                    return Ok(switchDetails);
                else
                    return Ok("No Records Found");


            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpPost]
        [Route("api/device/routers")]
        public IHttpActionResult CreateRouter(RouterInfoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var router = new RouterInfo();
                    router.HostName = model.HostName;
                    router.DNSName = model.DNSName;
                    router.DeviceType = model.DeviceType;
                    router.SelectManufacturer = model.SelectManufacturer;
                    router.ModelType = model.ModelType;
                    router.Model = model.Model;
                    router.OS = model.OS;
                    router.OSVersion = model.OSVersion;
                    router.ManufacturerPartNumber = model.ManufacturerPartNumber;
                    router.SerialNumber = model.SerialNumber;
                    router.InstallDate = model.InstallDate;
                    router.WarrantyStarted = model.WarrantyStarted;
                    router.WarrantyExpires = model.WarrantyExpires;
                    router.UploadRouterConfiguration = model.UploadRouterConfiguration;
                    router.Notes = model.Notes;
                    router.Tag = model.Tag;
                    router.CompanyId = model.CompanyId;
                    db.RouterInfoes.Add(router);
                    db.SaveChanges();
                    return Ok(router);
                }
                else
                    return Ok("Please Enter All the mandatory Fields");

            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpGet]
        [Route("api/device/routers")]
        public IHttpActionResult GetAllRouters(int pageNumber, int pageSize)
        {
            try
            {

                var routerDetails = db.RouterInfoes.ToList();
                if (routerDetails != null)
                {
                    int count = routerDetails.Count();
                    int CurrentPage = pageNumber;
                    int PageSize = pageSize;

                    var returnRouterDetails = routerDetails.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    var paginationMetadata = _pagingservice.Pagination(count, CurrentPage, PageSize);
                    HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                    return Ok(returnRouterDetails);
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
        [Route("api/device/routers/{id}")]
        public IHttpActionResult GetRoutersById(int id)
        {
            try
            {

                var routerDetails = db.RouterInfoes.FirstOrDefault(s => s.RouterId == id);
                if (routerDetails != null)
                    return Ok(routerDetails);
                else
                    return Ok("No Records Found");


            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpPost]
        [Route("api/device/firewalls")]
        public IHttpActionResult CreateFirewall(FirewallInfoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var firewall = new Firewallinformation();
                    firewall.HostName = model.HostName;
                    firewall.DNSName = model.DNSName;
                    firewall.SelectManufacturer = model.SelectManufacturer;
                    firewall.Model = model.Model;
                    firewall.OS = model.OS;
                    firewall.OSVersion = model.OSVersion;
                    firewall.ManufacturerPartNumber = model.ManufacturerPartNumber;
                    firewall.SerialNumber = model.SerialNumber;
                    firewall.InstallDate = model.InstallDate;
                    firewall.WarrantyStarted = model.WarrantyStarted;
                    firewall.WarrantyExpires = model.WarrantyExpires;
                    firewall.Notes = model.Notes;
                    firewall.UploadFirewallConfiguration = model.UploadFirewallConfiguration;
                    firewall.SelectTag = model.SelectTag;
                    firewall.CompanyId = model.CompanyId;
                    db.Firewallinformations.Add(firewall);
                    db.SaveChanges();
                    return Ok(firewall);
                }
                else
                    return Ok("Please Enter All the mandatory Fields");

            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpGet]
        [Route("api/device/firewalls")]
        public IHttpActionResult GetAllFirewalls(int pageNumber, int pageSize)
        {
            try
            {

                var firewallDetails = db.Firewallinformations.ToList();
                if (firewallDetails != null)
                {
                    int count = firewallDetails.Count();
                    int CurrentPage = pageNumber;
                    int PageSize = pageSize;
                    var returnfirewallDetails = firewallDetails.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    var paginationMetadata = _pagingservice.Pagination(count, CurrentPage, PageSize);
                    HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                    return Ok(returnfirewallDetails);
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
        [Route("api/device/firewalls/{id}")]
        public IHttpActionResult GetFirewallsById(int id)
        {
            try
            {

                var firewallDetails = db.Firewallinformations.FirstOrDefault(s => s.FirewallId == id);
                if (firewallDetails != null)
                    return Ok(firewallDetails);
                else
                    return Ok("No Records Found");


            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpPost]
        [Route("api/device/servers")]
        public IHttpActionResult CreateServer(ServerInfoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var server = new ServerInfo();
                    server.HostName = model.HostName;
                    server.DNSName = model.DNSName;
                    server.SelectManufacturer = model.SelectManufacturer;
                    server.SelectModel = model.SelectModel;
                    server.SelectManufacturerPartNumber = model.SelectManufacturerPartNumber;
                    server.SelectOS = model.SelectOS;
                    server.SelectOSVersion = model.SelectOSVersion;
                    server.SerialNumber = model.SerialNumber;
                    server.InstallDate = model.InstallDate;
                    server.WarrantyStarted = model.WarrantyStarted;
                    server.WarrantyExpires = model.WarrantyExpires;
                    server.SelectServerRoles = model.SelectServerRoles;
                    server.Notes = model.Notes;
                    server.Tag = model.Tag;
                    server.SoftwareType = model.SoftwareType;
                    server.SoftwareVersion = model.SoftwareVersion;
                    server.LicenseKey = model.LicenseKey;
                    server.ExpiresOn = model.ExpiresOn;
                    server.CompanyId = model.CompanyId;
                    db.ServerInfoes.Add(server);
                    db.SaveChanges();
                    return Ok(server);
                }
                else
                    return Ok("Please Enter All the mandatory Fields");

            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpGet]
        [Route("api/device/servers")]
        public IHttpActionResult GetAllServers(int pageNumber, int pageSize)
        {
            try
            {

                var serverDetails = db.ServerInfoes.ToList();
                if (serverDetails != null)
                {
                    int count = serverDetails.Count();
                    int CurrentPage = pageNumber;
                    int PageSize = pageSize;
                    var returnServerDetails = serverDetails.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    var paginationMetadata = _pagingservice.Pagination(count, CurrentPage, PageSize);
                    HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                    return Ok(returnServerDetails);
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
        [Route("api/device/servers/{id}")]
        public IHttpActionResult GetServersById(int id)
        {
            try
            {

                var serverDetails = db.ServerInfoes.FirstOrDefault(s => s.ServerId == id);
                if (serverDetails != null)
                    return Ok(serverDetails);
                else
                    return Ok("No Records Found");


            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpPost]
        [Route("api/device/workstations")]
        public IHttpActionResult CreateWorkstation(WorkstationInfoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var workstation = new WorkStationInfo();
                    workstation.HostName = model.HostName;
                    workstation.DNSName = model.DNSName;
                    workstation.SelectManufacturer = model.SelectManufacturer;
                    workstation.SelectModel = model.SelectModel;
                    workstation.SelectManufacturerPartNumber = model.SelectManufacturerPartNumber;
                    workstation.SelectOS = model.SelectOS;
                    workstation.SelectOSVersion = model.SelectOSVersion;
                    workstation.SerialNumber = model.SerialNumber;
                    workstation.InstallDate = model.InstallDate;
                    workstation.WarrantyStarted = model.WarrantyStarted;
                    workstation.WarrantyExpires = model.WarrantyExpires;
                    workstation.SelectServerRoles = model.SelectServerRoles;
                    workstation.Notes = model.Notes;
                    workstation.SelectTag = model.SelectTag;
                    workstation.SoftwareType = model.SoftwareType;
                    workstation.SoftwareVersion = model.SoftwareVersion;
                    workstation.LicenseKey = model.LicenseKey;
                    workstation.ExpiresOn = model.ExpiresOn;
                    workstation.CompanyId = model.CompanyId;
                    db.WorkStationInfoes.Add(workstation);
                    db.SaveChanges();
                    return Ok(workstation);
                }
                else
                    return Ok("Please Enter All the mandatory Fields");

            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpGet]
        [Route("api/device/workstations")]
        public IHttpActionResult GetAllWorkstation(int pageNumber, int pageSize)
        {
            try
            {

                var workstationDetails = db.WorkStationInfoes.ToList();
                if (workstationDetails != null)
                {
                    int count = workstationDetails.Count();
                    int CurrentPage = pageNumber;
                    int PageSize = pageSize;
                    var returnWorkstations = workstationDetails.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    var paginationMetadata = _pagingservice.Pagination(count, CurrentPage, PageSize);
                    HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                    return Ok(returnWorkstations);
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
        [Route("api/device/workstations/{id}")]
        public IHttpActionResult GetWorkstationById(int id)
        {
            try
            {

                var workstationDetails = db.WorkStationInfoes.FirstOrDefault(s => s.WorkStationId == id);
                if (workstationDetails != null)
                    return Ok(workstationDetails);
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