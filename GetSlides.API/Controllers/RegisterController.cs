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
        public void Register(HttpRequestMessage request) 
        {

        }

        [HttpGet]
        public string Register(string poruka, bool auth)
        {
            if (auth) return poruka; else return null;
        }

        [HttpPost]
        public object Register(string username, string email, string password, string confirmPassword)
        {
            try
            {
                UserRepository userBLLRepo = new UserRepository();
                userBLLRepo.Create(username, email, password, confirmPassword, Hash.CreateHash(password));
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
