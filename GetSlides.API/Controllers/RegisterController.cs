using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GetSlides.BLL;
using System.Web;


namespace GetSlides.API.Controllers
{
   
    public class RegisterController : ApiController
    {
        [HttpPost]
        public object Register(string username, string email, string password, string confirmPassword)
        {
            try
            {
               /* UserRepository userBLLRepo = new UserRepository();
                userBLLRepo.Create(username, email, password, confirmPassword, Hash.CreateHash(password));
                EmailManagementSystem.SendConfirmationLink();
                */
                HttpCookie sessionCookie = new HttpCookie("userInfo");
                sessionCookie.Values["userName"] = "nj";
                sessionCookie.Values["lastSession"] = DateTime.Now.ToString();
                sessionCookie.Expires = DateTime.Now.AddDays(1);
                HttpContext.Current.Response.AppendCookie(sessionCookie);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
