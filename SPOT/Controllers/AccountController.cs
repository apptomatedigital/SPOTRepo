using SPOT.Models;
using SPOT.Models.SwaggerModels;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SPOT.Controllers
{
    public class AccountController : ApiController
    {
        private SpotEntities db = new SpotEntities();
        [HttpPost]
        [Route("api/account/invitations/")]
        public IHttpActionResult InvitationSending(string EmailAddress)
        {
            try
            {
                var invitation = new Invitation();
                if (ModelState.IsValid)
                {
                    invitation.CreatedDateTime = ConvertDatetimeToIST();
                    invitation.ExpiryDate = ConvertDatetimeToISTExpiry();
                    invitation.EmailAddress = EmailAddress;
                    invitation.IsRegistered = false;
                    db.Invitations.Add(invitation);
                    db.SaveChanges();
                    //var lnkHref = "<a href='" + Url.Action("Create", "SpotAccount", new { email = EmailAddress }, "http") + "'>Register</a>";
                    string subject = "Spot Registration ";
                    string body = "<b> You are Invited to Register into SPOT. This Invitation Only Valid for 7 days </b><br/>";
                    //_mailsender.SendEmail(model.Email, body, subject, "falcon@noreply.com");

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
                        if (!string.IsNullOrWhiteSpace(EmailAddress))
                        {
                            message.To.Add(EmailAddress);
                            message.Subject = subject;
                            message.IsBodyHtml = true;
                            message.Body = body;
                            Task.Run(() => client.Send(message));
                        }
                    }
                    return Ok(invitation);
                    ////return View("InvitationSuccessful");

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
        [HttpPost]
        [Route("api/account/registrations")]
        public IHttpActionResult Register(AccountModel account)
        {
            var accountsnew = new Account();
            if (ModelState.IsValid)
            {
                var  updateinvitationtable = db.Invitations.Where(s => s.EmailAddress == account.EmailId && s.IsRegistered==false).FirstOrDefault();
                if (updateinvitationtable != null)
                {
                    var exsAccount= db.Accounts.Where(s => s.EmailId == account.EmailId ).FirstOrDefault();
                    if (exsAccount == null)
                    {
                        if (updateinvitationtable.ExpiryDate >= DateTime.Now)
                        {
                            accountsnew.EmailId = account.EmailId;
                            accountsnew.FirstName = account.FirstName;
                            accountsnew.LastName = account.LastName;
                            accountsnew.MiddleName = account.MiddleName;
                            accountsnew.DisplayName = account.DisplayName;
                            accountsnew.Password = account.Password;
                            db.Accounts.Add(accountsnew);
                            db.SaveChanges();
                            updateinvitationtable.IsRegistered = true;
                            db.SaveChanges();
                            //var passwordattempts = new PasswordAttempt();
                            //passwordattempts.UserId = accountsnew.AccountId;
                            //passwordattempts.EmailAddress = accountsnew.EmailId;
                            //passwordattempts.Attempts = 0;
                            //db.PasswordAttempts.Add(passwordattempts);
                            //db.SaveChanges();

                            //return Json(accountsnew, JsonRequestBehavior.AllowGet);
                            return Ok(accountsnew);
                        }
                        else
                            return Ok("Registraion Expired please contact SPOT Admin");
                    }
                    else
                        return Ok("Already Registered Email Address");
                }
                else
                    return Ok("Officialy Not Invited");

                //return View("RegisterSuccess");
            }
            return Ok("Please Enter All the mandatory Fields");
        }
        [HttpGet]
        [Route("api/account/login")]
        public IHttpActionResult Login(string EmailAddress, string Password)
        {
            var loginerror = string.Empty;
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var loggeduser = db.Accounts.FirstOrDefault(s => s.EmailId == EmailAddress && s.Password == Password);
                if (loggeduser != null)
                {
                    ////HttpContext.Current.Session["loggeduserid"] = loggeduser.AccountId;
                    return Ok(loggeduser);
                }
                else
                {


                    var passwordattempt = new PasswordAttempt();
                    var updatevalidEmail = db.PasswordAttempts.FirstOrDefault(s => s.EmailAddress == EmailAddress);
                    if (updatevalidEmail != null)
                    {
                        if (updatevalidEmail.Attempts < 3)
                        {
                            updatevalidEmail.Attempts = updatevalidEmail.Attempts + 1;
                            db.SaveChanges();
                            var attempts = 3 - updatevalidEmail.Attempts;
                            if (attempts != 0)
                                loginerror = "Login Failed..Invalid Password Entry.. Only " + attempts + " Attempts Left! ";
                            else
                                loginerror = "Account Blocked Contact Admin";
                        }
                        else if (updatevalidEmail.Attempts == 3)
                            loginerror = "Account Blocked Contact Admin";

                    }
                    else
                        loginerror = "Login Failed..Invalid User";
                    //return View();
                    return Ok("" + loginerror + "");
                }
            }
            catch (Exception ex)
            {
                //return View();
                return Ok("" + ex.Message + "");
            }
        }
        [HttpPost]
        [Route("api/account/forgetpassword")]
        public IHttpActionResult ResetPassword(string EmailAddress, string Password, string ConfirmPassword)
        {
            try
            {
                var user = db.Accounts.Where(s => s.EmailId == EmailAddress).FirstOrDefault();

                if (user == null)
                    return Ok("The EmailAddress  has No Account In SPOT");
                else
                {

                    user.Password = Password;
                    db.SaveChanges();
                    return Ok("Your Password Has been Reset Successfully!!!");
                }



            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        public DateTime ConvertDatetimeToISTExpiry()
        {
            try
            {
                var tz = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                var convertedDate = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);
                var expirydate = convertedDate.AddDays(7);
                return expirydate;
            }
            catch (Exception ex)
            {

                return DateTime.Now;
            }
        }
        public DateTime ConvertDatetimeToIST()
        {
            try
            {
                var tz = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                var convertedDate = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tz);
                return convertedDate;
            }
            catch (Exception ex)
            {

                return DateTime.Now;
            }
        }
    }
}