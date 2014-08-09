using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public override AuthToken Select(string ID)
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                return context.AuthTokens.FirstOrDefault(t => t.ID == ID);
            }
        }
        public override void Create(AuthToken token)
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                token.ID = this.GenerateID();
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
