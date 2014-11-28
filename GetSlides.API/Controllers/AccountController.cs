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
            BLL.EmailTokenRepository emailTokenRepo = new BLL.EmailTokenRepository();
            string tokenID;
            BLL.EmailToken emailToken = new BLL.EmailToken { StartDateTime = DateTime.Now };
            emailTokenRepo.Create(emailToken, out tokenID);
            EmailManagementSystem.SendPasswordReset(email, tokenID);
            HttpContext.Current.Response.Redirect("",true);
        }

        [HttpPost]
        public void SetNewPassword(string email, string newPassword, string confirmPass, int tokenID) 
        {
            BLL.UserRepository userRepo = new BLL.UserRepository();
            BLL.User user = userRepo.SelectByEmail(email);
            if (newPassword == confirmPass && userRepo.ConfirmEmailToken(tokenID,user))
            {
                user.PasswordHash = Hash.CreateHash(newPassword);
                userRepo.Update(user);
            }
        }

    }
}
