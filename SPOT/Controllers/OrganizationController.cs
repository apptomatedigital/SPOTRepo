using Newtonsoft.Json;
using SPOT.Models;
using SPOT.Models.SwaggerModels;
using SPOT.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace SPOT.Controllers
{
    public class OrganizationController : ApiController
    {
        private SpotEntities db = new SpotEntities();
        public PagingService _pagingservice = new PagingService();
        [HttpPost]
        [Route("api/organization/domainvalidate")]
        public IHttpActionResult ValidateDomain(string domainname)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var validDomain = db.ValidateDomains.FirstOrDefault(s => s.DomailName.ToLower().Trim() == domainname.ToLower().Trim());
                    if (validDomain != null)
                        return Ok("Valid Domain");
                    else
                        return Ok("Not a Valid Domain");
                    //return View();
                }

                return Ok("Internal Error Occured");
                //return View();
            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
                //return View();
            }
        }
        [HttpPost]
        [Route("api/organization/registrations")]
        public IHttpActionResult OrganizationRegister(OrganizationViewModel model)
        {
            var addOrganization = new OrganizationDetail();
            try
            {
                if (ModelState.IsValid)
                {
                    var chkOrganizationexs = db.OrganizationDetails.Where(s => s.EmailId == model.EmailId).FirstOrDefault();
                    if (chkOrganizationexs == null)
                    {
                        addOrganization.EmailId = model.EmailId;
                        addOrganization.OrganizationName = model.OrganizationName;
                        addOrganization.FirstName = model.FirstName;
                        addOrganization.LastName = model.LastName;
                        addOrganization.Password = model.Password;
                        addOrganization.OTP = GenerateOTP(0, 0, false, false, false, false);
                        addOrganization.IsActivated = false;
                        addOrganization.CreatedBy = model.CreatedBy;
                        db.OrganizationDetails.Add(addOrganization);
                        db.SaveChanges();
                        var subject = "OTP for SPOT Organization Registration";
                        var body = "<b> The OTP is " + addOrganization.OTP + "</b><br/>";


                        var senderEmail = ConfigurationManager.AppSettings["SenderGmailId"];
                        var senderPassword = ConfigurationManager.AppSettings["SenderGmailPassword"];
                        var smtpServer = ConfigurationManager.AppSettings["GmailServer"];
                        var portNumber = Convert.ToInt32(ConfigurationManager.AppSettings["GmailPort"]);
                        var IsSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["EmailSSL"]);
                        var senderName = ConfigurationManager.AppSettings["SenderName"];

                        if (!string.IsNullOrWhiteSpace(senderEmail) && !string.IsNullOrWhiteSpace(senderPassword) && !string.IsNullOrWhiteSpace(smtpServer) && portNumber != 0 && IsSSL)
                        {
                            var client = new SmtpClient(smtpServer, portNumber)
                            {
                                Credentials = new NetworkCredential(senderEmail, senderPassword),
                                EnableSsl = Convert.ToBoolean(IsSSL)
                            };

                            var message = new MailMessage();
                            message.From = new MailAddress(senderEmail, senderName);
                            if (!string.IsNullOrWhiteSpace(addOrganization.EmailId))
                            {
                                message.To.Add(addOrganization.EmailId);
                                message.Subject = subject;
                                message.IsBodyHtml = true;
                                message.Body = body;
                                Task.Run(() => client.Send(message));
                            }
                        }

                        return Ok(addOrganization);
                    }
                    else
                        return Ok("Organization Email Address Already Taken.");
                }
                else
                    return Ok("Fill All the Mandatory Fields");
            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpPost]
        [Route("api/organization/otpverification")]
        public IHttpActionResult OTPVerficationforOrganization(string EmailAddress, string OTP)
        {
            try
            {
                var chkOrganization = db.OrganizationDetails.FirstOrDefault(s => s.EmailId == EmailAddress && s.OTP == OTP);
                if (chkOrganization != null)
                {
                    if (!chkOrganization.IsActivated)
                    {
                        chkOrganization.IsActivated = true;
                        db.SaveChanges();
                        return Ok(chkOrganization);
                    }
                    else
                    {
                        return Ok("Already Verified");
                    }
                }
                else
                {
                    return Ok("Invalid Otp");
                }
            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpGet]
        [Route("api/organization/organizations")]
        public IHttpActionResult GetAllOrganizations(int pageNumber, int pageSize)
        {
            try
            {

                var organization = db.OrganizationDetails.ToList();
                if (organization != null)
                {
                    int count = organization.Count();
                    int CurrentPage = pageNumber;
                    int PageSize = pageSize;
                    var returnOrganizations = organization.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
                    var paginationMetadata = _pagingservice.Pagination(count, CurrentPage, PageSize);

                    HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
                    return Ok(returnOrganizations);
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
        [Route("api/organization/organizations/{id}")]
        public IHttpActionResult GetOrganizationsById(int id)
        {
            try
            {
                
                var organization = db.OrganizationDetails.FirstOrDefault(s => s.OrganizationId == id);
                if (organization != null)
                    return Ok(organization);
                else
                    return Ok("No Records Found");


            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
      
        [HttpPost]
        [Route("api/organization/login")]
        public IHttpActionResult OrganizationLogin(string EmailAddress, string Password)
        {
            try
            {
                var organization = db.OrganizationDetails.FirstOrDefault(s => s.EmailId == EmailAddress && s.Password == Password && s.IsDeleted == false);
                if (organization != null)
                    return Ok(organization);
                else
                    return Ok("Invalid Credentials");
            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
      
        [HttpPost]
        [Route("api/organization/forgetpassword")]
        public IHttpActionResult ResetPasswordOrganization(string EmailAddress, string Password, string ConfirmPassword)
        {
            try
            {
                var organization = db.OrganizationDetails.Where(s => s.EmailId == EmailAddress).FirstOrDefault();

                if (organization == null)
                    return Ok("Organization Not Found");
                else
                {

                    organization.Password = Password;
                    db.SaveChanges();
                    return Ok("Your Password Has been Reset Successfully!!!");
                }



            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }

        }
      

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GenerateOTP(int RequiredLength, int RequiredUniqueChars, bool RequireDigit, bool RequireLowercase, bool RequireNonAlphanumeric, bool RequireUppercase)
        {
            try
            {

                RequiredLength = 4;
                RequiredUniqueChars = 4;
                RequireDigit = true;

                string[] randomChars = new[] { "0123456789", "0123456789", "0123456789", "0123456789" };

                Random rand = new Random(Environment.TickCount);
                List<char> chars = new List<char>();

                for (int i = chars.Count; i < RequiredLength; i++)
                {
                    string rcs = randomChars[rand.Next(0, randomChars.Length)];
                    chars.Insert(rand.Next(0, chars.Count),
                        rcs[rand.Next(0, rcs.Length)]);
                }

                return new string(chars.ToArray());
            }
            catch (Exception ex)
            {

                return string.Empty;
            }
        }
    }
}