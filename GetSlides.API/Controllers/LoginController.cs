using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GetSlides.BLL;
using GetSlides.Utility;

namespace GetSlides.API.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public object Login(string email, string password) 
        {
            UserRepository userRepo = new UserRepository();
            User currentUser = userRepo.SelectByEmail(email);
            if (Hash.ValidateContent(Hash.CreateHash(password), currentUser.PasswordHash))
            {
                AuthTokenRepository authTokenRepo = new AuthTokenRepository();
                string newToken;
                authTokenRepo.Create(new AuthToken { UserID = currentUser.ID, Timespan = 20 }, out newToken);
                return newToken;
            }
            return false;
        }
     
    }
}
