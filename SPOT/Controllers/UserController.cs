using SPOT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SPOT.Controllers
{
    public class UserController : ApiController
    {
        private SpotEntities db = new SpotEntities();

        [HttpPost]
        [Route("api/user/users")]
        public IHttpActionResult CreateUser(UserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var chkuserexs = db.Users.Where(s => s.EmailAddress == model.EmailAddress).FirstOrDefault();
                    if (chkuserexs == null)
                    {
                        var user = new User();
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.UserName = model.UserName;
                        user.EmailAddress = model.EmailAddress;
                        user.PrimaryPhone = model.PrimaryPhone;
                        user.Extension = model.Extension;
                        user.AlternatePhone = model.AlternatePhone;
                        user.Title = model.Title;
                        user.Department = model.Department;
                        user.IsPrimaryContact = model.IsPrimaryContact;
                        user.Notes = model.Notes;
                        db.Users.Add(user);
                        db.SaveChanges();
                        return Ok(user);
                    }
                    else
                        return Ok("" + model.EmailAddress + " is Already Taken, Take another one");
                }
                else
                    return Ok("Please Enter All the mandatory Fields");
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("api/users/deviceassignments")]
        public IHttpActionResult AssigningDevices(DeviceandUsersModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var validdevice = db.DevicePasswords.Where(s => s.DeviceId == model.DeviceId).ToList();
                    var validuser = db.Users.Where(s => s.UserId == model.UserId).ToList();
                    //var chkexists = db.DeviceforUsers.Where(s => s.DeviceId==model.DeviceId || s.UserId==model.UserId).ToList();
                    if ((validdevice.Count != 0) && (validuser.Count != 0))
                    {
                        var adddeviceusers = new DeviceforUser();
                        adddeviceusers.DeviceId = model.DeviceId;
                        adddeviceusers.UserId = model.UserId;
                        adddeviceusers.FirstName = model.FirstName;
                        db.DeviceforUsers.Add(adddeviceusers);
                        db.SaveChanges();
                        return Ok(adddeviceusers);
                    }
                    else if (validdevice.Count == 0)
                        return Ok("Device Not Exists");
                    else if (validuser.Count == 0)
                        return Ok("User Not Exists");
                    else
                        return NotFound();

                }
                else
                    return Ok("Please Enter All the mandatory Fields");
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("api/users/revokedeviceassignments")]
        public IHttpActionResult RevokeAssigningDevices(DeviceandUsersModel model)
        {
            try
            {
                var validdevice = db.DevicePasswords.Where(s => s.DeviceId == model.DeviceId).ToList();
                var validuser = db.Users.Where(s => s.UserId == model.UserId).ToList();
                var device = db.DeviceforUsers.Where(s => s.DeviceId == model.DeviceId).FirstOrDefault();
                if ((validdevice.Count != 0) && (validuser.Count != 0) && (device != null))
                {
                    device.UserId = model.UserId;
                    device.FirstName = model.FirstName;
                    db.SaveChanges();
                    return Ok(device);
                }
                else if (validdevice.Count == 0)
                    return Ok("Device Not Exists");
                else if (validuser.Count == 0)
                    return Ok("User Not Exists");
                else if (device == null)
                    return Ok("The device till not assigned. Need to assign first.");
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("api/users/groups")]
        public IHttpActionResult CreatingGroup(string GroupName)
        {
            try
            {
                if (GroupName != null)
                {
                    var group = new GroupDetail();
                    group.GroupName = GroupName;
                    db.GroupDetails.Add(group);
                    db.SaveChanges();
                    return Ok(group);
                }
                else
                    return Ok("Group Name is required");
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("api/users/gruopassignments")]
        public IHttpActionResult AssigningGroup(int groupid, int userid)
        {
            try
            {
                if ((groupid != 0) && (userid != 0))
                {
                    var user = db.UsersGroups.Where(s => s.UserId == userid).ToList();
                    var chkgruoup = db.GroupDetails.Where(s => s.GroupId == groupid).ToList();
                    if ((user.Count == 0) && (chkgruoup.Count != 0))
                    {
                        var usergroup = new UsersGroup();
                        usergroup.GroupId = groupid;
                        usergroup.UserId = userid;
                        db.UsersGroups.Add(usergroup);
                        db.SaveChanges();
                        return Ok(usergroup);
                    }
                    else if (user.Count != 0)
                        return Ok("User Already Assigned to a group");
                    else
                        return Ok("Group Not Exists");

                }
                else
                    return Ok("Group Id and User Id is required");
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("api/users/applications")]
        public IHttpActionResult CreatingApplication(string ApplicationName)
        {
            try
            {
                if (ApplicationName != null)
                {
                    var application = new ApplicationDetail();
                    application.ApplicationName = ApplicationName;
                    db.ApplicationDetails.Add(application);
                    db.SaveChanges();
                    return Ok(application);
                }
                else
                    return Ok("Application Name is required");
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("api/users/applicationassignments")]
        public IHttpActionResult AssigningApplicationtoUser(int applicationid, int userid)
        {
            try
            {
                if (applicationid != 0 && userid != 0)
                {
                    var applicationusers = new UserApplication();
                    applicationusers.ApplicationId = applicationid;
                    applicationusers.UserId = userid;
                    db.UserApplications.Add(applicationusers);
                    db.SaveChanges();
                    return Ok(applicationusers);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}