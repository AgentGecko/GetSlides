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
            return null;
        }
     
    }
}
