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
    public class DeviceTagController : ApiController
    {
        private SpotEntities db = new SpotEntities();
        public PagingService _pagingservice = new PagingService();
        [HttpPost]
        [Route("api/device-tag/usertags")]
        public IHttpActionResult AddUserTag(UserTagModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usertag = new UserTag();
                    usertag.Name = model.Name;
                    usertag.Color = model.Color;
                    usertag.DeviceId = model.DeviceId;
                    db.UserTags.Add(usertag);
                    db.SaveChanges();
                    return Ok(usertag);
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
        [Route("api/device-tag/usertags")]
        public IHttpActionResult GetAllUserTags(int pageNumber, int pageSize)
        {
            try
            {

                var usertag = db.UserTags.ToList();
                if (usertag != null)
                {
                    int count = usertag.Count();
                    int CurrentPage = pageNumber;
                    int PageSize = pageSize;
                    var returnUserTags= usertag.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    var paginationMetadata = _pagingservice.Pagination(count, CurrentPage, PageSize);
                    HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                    return Ok(returnUserTags);
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
        [Route("api/device-tag/usertags/{id}")]
        public IHttpActionResult GetUserTagsById(int id)
        {
            try
            {

                var usertag = db.UserTags.FirstOrDefault(s => s.TagId == id);
                if (usertag != null)
                    return Ok(usertag);
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