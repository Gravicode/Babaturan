using Babaturan.Data;
using Babaturan.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Babaturan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        StorageObjectService blob;
        UserProfileService UserSvc;
        public UserController(UserProfileService userSvc, StorageObjectService blob)
        {
            this.blob = blob;

            this.UserSvc = userSvc;
        }
        // /api/User/GetPicByUsername
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPicByUsername(string Username)
        {
            try
            {

                var usr = UserSvc.GetItemByUsername(Username);
                if (usr != null)
                {
                    var fname = usr.PicUrl.Replace("/api/dms/getfile?filename=", string.Empty);
                    var item = await blob.DownloadByKey(fname);
                    var file = item.Data;
                    if (file != null)
                    {

                        var mime = MimeTypeHelper.GetMimeType(Path.GetExtension(fname));
                        return File(file, mime, fname);
                    }
                }
               
               
            }
            catch (Exception)
            {

                throw;
            }
            return NotFound();
        }
        // /api/User/GetUser
        [HttpGet("[action]")]
        public UserModel GetUser()
        {
            // Instantiate a UserModel
            var userModel = new UserModel
            {
                UserName = "[]",
                IsAuthenticated = false
            };
            // Detect if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Set the username of the authenticated user
                userModel.UserName =
                    User.Identity.Name;
                userModel.IsAuthenticated =
                    User.Identity.IsAuthenticated;
            };
            return userModel;
        }
    }
    // Class to hold the UserModel
    public class UserModel
    {
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
