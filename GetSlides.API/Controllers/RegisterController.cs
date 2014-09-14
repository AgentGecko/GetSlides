using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GetSlides.BLL;
using System.Web;


namespace GetSlides.API.Controllers
{
    /// <summary>
    /// RegisterController is the subclass of the ApiController class which is used for the application registration process.
    /// </summary>
    public class RegisterController : ApiController
    {
        [HttpPost]
        public object Register(string username, string email, string password, string confirmPassword)
        {
            try
            {
                UserRepository userBLLRepo = new UserRepository();
                userBLLRepo.Create(username, email, password, confirmPassword, Hash.CreateHash(password));
                EmailManagementSystem.SendConfirmationLink();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
