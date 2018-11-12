using SPOT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace SPOT.Controllers
{
    public class DevicePasswordsController : ApiController
    {
        private SpotEntities db = new SpotEntities();
        [HttpPost]
        [Route("api/device-passwords/device")]
        public IHttpActionResult AddDevicePassword(DevicePasswordModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var adddevicepass = new DevicePassword();
                    var checkdevice = db.DevicePasswords.Where(s => s.Username == model.Username).ToList();
                    if (checkdevice.Count == 0)
                    {
                        adddevicepass.Username = model.Username;
                        adddevicepass.Password = model.Password;
                        adddevicepass.Description = model.Description;
                        db.DevicePasswords.Add(adddevicepass);
                        db.SaveChanges();
                        return Ok(adddevicepass);
                    }
                    else
                    {
                        return Ok("Username Already Exists");
                    }
                }
                else
                    return Ok("Please Enter All the mandatory Fields");
            }
            catch (Exception ex)
            {
                return Ok("" + ex.Message + "");
            }
        }
        [HttpPost]
        [Route("api/device-passwords/passwords")]
        public IHttpActionResult GenerateDevicePassword(int DeviceId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createpassword = new GeneratePassword();
                    var Password = GenerateRandomPassword();
                    createpassword.DeviceId = DeviceId;
                    createpassword.Password = Password;
                    db.GeneratePasswords.Add(createpassword);
                    db.SaveChanges();

                    return Ok(createpassword);
                }
                else
                    return Ok("Please Enter All the mandatory Fields");
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GenerateRandomPassword()
        {
            try
            {
                int RequiredLength = 15;
                int RequiredUniqueChars = 7;
                bool RequireDigit = true;
                bool RequireLowercase = true;
                bool RequireNonAlphanumeric = true;
                bool RequireUppercase = true;

                string[] randomChars = new[] {
                    "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                    "abcdefghijkmnopqrstuvwxyz",    // lowercase
                    "0123456789",                   // digits
                    "!@$?_-"                        // non-alphanumeric
                };

                Random rand = new Random(Environment.TickCount);
                List<char> chars = new List<char>();

                if (RequireUppercase)
                    chars.Insert(rand.Next(0, chars.Count),
                        randomChars[0][rand.Next(0, randomChars[0].Length)]);

                if (RequireLowercase)
                    chars.Insert(rand.Next(0, chars.Count),
                        randomChars[1][rand.Next(0, randomChars[1].Length)]);

                if (RequireDigit)
                    chars.Insert(rand.Next(0, chars.Count),
                        randomChars[2][rand.Next(0, randomChars[2].Length)]);

                if (RequireNonAlphanumeric)
                    chars.Insert(rand.Next(0, chars.Count),
                        randomChars[3][rand.Next(0, randomChars[3].Length)]);

                for (int i = chars.Count; i < RequiredLength
                    || chars.Distinct().Count() < RequiredUniqueChars; i++)
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