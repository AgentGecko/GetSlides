using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetSlides.Utility;

namespace GetSlides.DAL
{
    public class AuthTokenRepository : DALRepository<AuthToken>
    {
        #region CRUD
        public override ICollection<AuthToken> Select()
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                return context.AuthTokens.ToList();
            }
        }
        public override AuthToken Select(int ID)
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                return context.AuthTokens.FirstOrDefault(t => t.ID == ID);
            }
        }
        public void Create(AuthToken token, out string authenticationToken)
        {
            authenticationToken = "";
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                
                token.StartDateTime = DateTime.Now;
                authenticationToken = token.Token = Hash.CreateHash(context.Users.FirstOrDefault(t => t.ID == token.UserID).Email
                                                                    + token.StartDateTime);
                authenticationToken = token.Token + ";" + token.ID + ";" + MD5Hash.CreateHash(token.Token + token.ID + token.UserID);
                context.AuthTokens.Add(token);
                context.SaveChanges();
            }
        }
        public override void Create(AuthToken token)
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                
                context.AuthTokens.Add(token);
                context.SaveChanges();
            }
        }
        public override void Update(AuthToken token)
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                var DBToken = context.AuthTokens.FirstOrDefault(t => t.ID == token.ID);
                DBToken = token;
                context.SaveChanges();
            }
        }
        public override void Delete(AuthToken token)
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                context.AuthTokens.Remove(token);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
