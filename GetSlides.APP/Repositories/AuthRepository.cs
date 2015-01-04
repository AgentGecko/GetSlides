using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using GetSlides.APP.Models;


namespace GetSlides.APP.Repositories
{
    public class AuthRepository : IDisposable
    {
        private AuthContext ctx;
        private UserManager<IdentityUser> userMgr;

        public AuthRepository()
        {
            ctx = new AuthContext();
            userMgr = new UserManager<IdentityUser>(new UserStore<IdentityUser>(ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser { UserName = userModel.UserName };
            var result = await userMgr.CreateAsync(user, userModel.Password);
            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await userMgr.FindAsync(userName, password);
            return user;
        }

        public void Dispose()
        {
            ctx.Dispose();
            ctx = null;
            userMgr = null;
        }
    }
}