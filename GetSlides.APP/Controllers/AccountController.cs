﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using GetSlides.APP.Models;
using GetSlides.APP.Repositories;

namespace GetSlides.APP.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private AuthRepository repo = null;

        public AccountController()
        {
            repo = new AuthRepository();
        }

        [Authorize]
        [Route("ping")]
        [HttpGet]
        public string Ping()
        {
            return "Ok";
        }

        [Authorize]
        [Route("username")]
        [HttpGet]
        public string GetUserName()
        {
            return User.Identity.Name;
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            IdentityResult result = await repo.RegisterUser(userModel);
            IHttpActionResult errorResult = GetErrorResult(result);
            if (errorResult != null) return errorResult;
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) repo.Dispose();
            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null) return InternalServerError();
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
                if (ModelState.IsValid) return BadRequest();
                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
