using Newtonsoft.Json;
using SPOT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SPOT.Service;
namespace SPOT.Controllers
{
    public class DeviceInterfaceController : ApiController
    {
        private SpotEntities db = new SpotEntities();
        public PagingService _pagingservice = new PagingService();
        [HttpPost]
        [Route("api/device-interface/networkinterfaces")]
        public IHttpActionResult CreateNetworkInterface(NetworkInterfaceModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var networkfordevice = new NetworkInterface();
                    networkfordevice.HostName = model.HostName;
                    networkfordevice.SelectManufacturer = model.SelectManufacturer;
                    networkfordevice.Model = model.Model;
                    networkfordevice.OS = model.OS;
                    networkfordevice.OSVersion = model.OSVersion;
                    networkfordevice.ManufacturerPartNumber = model.ManufacturerPartNumber;
                    networkfordevice.SerialNumber = model.SerialNumber;
                    networkfordevice.InstallDate = model.InstallDate;
                    networkfordevice.WarrantyStarted = model.WarrantyStarted;
                    networkfordevice.WarrantyExpires = model.WarrantyExpires;
                    networkfordevice.UploadNetworkSwitchConfiguration = model.UploadNetworkSwitchConfiguration;
                    networkfordevice.Notes = model.Notes;
                    networkfordevice.SelectTag = model.SelectTag;
                    networkfordevice.CompanyId = model.CompanyId;
                    db.NetworkInterfaces.Add(networkfordevice);
                    db.SaveChanges();
                    return Ok(networkfordevice);
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
        [Route("api/device-interface/networkinterfaces")]
        public IHttpActionResult GetAllNetworkInterfaces(int pageNumber, int pageSize)
        {
            try
            {

                var networkInterfaces = db.NetworkInterfaces.ToList();
                if (networkInterfaces != null)
                {
                    int count = networkInterfaces.Count();
                    int CurrentPage = pageNumber;
                    int PageSize = pageSize;
                    var returnNetworkInterfaces = networkInterfaces.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    var paginationMetadata = _pagingservice.Pagination(count, CurrentPage, PageSize);
                    HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                    return Ok(returnNetworkInterfaces);
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
        [Route("api/device-interface/networkinterfaces/{id}")]
        public IHttpActionResult GetSiteNetworkDomainsById(int id)
        {
            try
            {

                var networkInterfaces = db.NetworkInterfaces.FirstOrDefault(s => s.Id == id);
                if (networkInterfaces != null)
                    return Ok(networkInterfaces);
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