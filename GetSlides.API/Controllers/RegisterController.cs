using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GetSlides.BLL;

namespace GetSlides.API.Controllers
{
    public class RegisterController : ApiController
    {
        [HttpPost]
        public object Register(string username, string email, string password, string confirmPassword)
        {
            try
            {
                UserRepository userBLLRepo = new UserRepository();
                userBLLRepo.Create(username, email, password, confirmPassword, Hash.CreateHash(password));
                //SO! Email confirmation should be sent only if the BLL sends a positive responce from saving into DB
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
