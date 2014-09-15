using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GetSlides.BLL;

namespace GetSlides.API.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public object Login(string email, string password) 
        {
            UserRepository bllUserRepo = new UserRepository();
            AuthenticationToken userAuthenticationToken = new AuthenticationToken(email);
            AuthTokenRepository bllAuthTokenRepo = new AuthTokenRepository();
            User currentUser = bllUserRepo.SelectByEmail(email);
            if (Hash.ValidateContent(password, currentUser.PasswordHash))
            {
                bllAuthTokenRepo.Create(new AuthToken { 
                                                        StartDateTime = DateTime.Now,
                                                        Token = Hash.CreateHash(userAuthenticationToken.Token), 
                                                        UserID = currentUser.ID,
                                                        Timespan = 20
                                                       });
                return userAuthenticationToken.Token;
            }
            return false;
        }
     
    }
}
