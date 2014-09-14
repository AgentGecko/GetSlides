using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GetSlides.API.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public object Login(string email, string password) 
        {
            BLL.UserRepository bllRepo = new BLL.UserRepository();
            AuthenticationToken userToken = new AuthenticationToken(email);
            if (bllRepo.LogInBaseValidation(email, Hash.CreateHash(password), Hash.CreateHash(userToken.Token)))
                return userToken.Token;
            else
                return false;
        }
    }
}
