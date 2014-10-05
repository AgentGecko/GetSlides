using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GetSlides.BLL;
using System.Web;
using System.Net.Http;
using GetSlides.Utility;

namespace GetSlides.API.Controllers
{
    /// <summary>
    /// RegisterController is the subclass of the ApiController class which is used for the application registration process.
    /// </summary>
    public class RegisterController : ApiController
    {
        [HttpPost]
        public void Register(string username, string email, string password, string confirmPassword)
        {
            try
            {
                UserRepository bllUserRepo = new UserRepository();
                EmailTokenRepository bllEmailTokenRepo = new EmailTokenRepository();
                bllUserRepo.Create(username, email, password, confirmPassword, Hash.CreateHash(password));
                EmailToken Token = new EmailToken { UserID = bllUserRepo.SelectByEmail(email).ID, StartDateTime = DateTime.Now };
                bllEmailTokenRepo.Create(Token);
                EmailManagementSystem.SendConfirmationLink(email, Token.ID );
            }
            catch(Exception ex)
            {
                
            }
        }

        [HttpGet]
        public object ConfirmRegistration(string email, string token)
        {
            UserRepository bllUserRepo = new UserRepository();
            if (bllUserRepo.ConfirmEmailToken(token, bllUserRepo.SelectByEmail(email)))
                return true;
            else
                return false;
        }
    }
}
