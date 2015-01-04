using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GetSlides.APP.Repositories;
using GetSlides.DL;
using Microsoft.AspNet.Identity;

namespace GetSlides.APP.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        [Authorize]
        [Route("abc")]
        public IHttpActionResult Get()
        {
            return Ok("Test test");
        }

        [Authorize]
        [Route("123")]
        public IHttpActionResult Get1()
        {
            using (PresentationRepository presentationRepository = new PresentationRepository())
            {
                presentationRepository.Create(new Presentation
                {
                    DateUploaded = DateTime.UtcNow,
                    Info = "cao ljudi kako'ste",
                    Name = "Ne seri"
                }, User.Identity.Name);
            }
            return Ok("faco");
        }
    }
}

