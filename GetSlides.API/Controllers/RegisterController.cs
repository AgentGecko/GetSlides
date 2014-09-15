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
                UserRepository bllUserRepo = new UserRepository();
                EmailTokenRepository bllEmailTokenRepo = new EmailTokenRepository();
                bllUserRepo.Create(username, email, password, confirmPassword, Hash.CreateHash(password));
                EmailToken Token = new EmailToken { UserID = bllUserRepo.SelectByEmail(email).ID, StartDateTime = DateTime.Now };
                bllEmailTokenRepo.Create(Token);
                EmailManagementSystem.SendConfirmationLink(email, Token.ID );
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
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
