using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using GetSlides.Utility;

namespace GetSlides.API.Controllers
{
    public class AccountController : ApiController
    {
        [HttpPost]
        public void ResetPassword(string email) 
        {
            
            EmailManagementSystem.SendPasswordReset(email);
            HttpContext.Current.Response.Redirect("");
        }

        [HttpPost]
        public void SetNewPassword(string email, string newPassword, string confirmPass) 
        {
            BLL.UserRepository userRepo = new BLL.UserRepository();
            BLL.User user = userRepo.SelectByEmail(email);
            if (newPassword == confirmPass)
            {
                user.PasswordHash = Hash.CreateHash(newPassword);
                userRepo.Update(user);
            }
        }

    }
}
