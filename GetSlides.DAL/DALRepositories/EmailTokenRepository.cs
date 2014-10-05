using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetSlides.Utility;

namespace GetSlides.DAL
{
    public class EmailTokenRepository : DALRepository<EmailToken>
    {
        #region CRUD
        public override ICollection<EmailToken> Select()
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                return context.EmailTokens.ToList();
            }
        }
        public override EmailToken Select(string ID)
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                return context.EmailTokens.FirstOrDefault(t => t.ID == ID);
            }
        }
        public override void Create(EmailToken token)
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                token.ID = this.GenerateID();
                context.EmailTokens.Add(token);
                context.SaveChanges();
            }
        }
        public void Create(EmailToken token, out string emailTokenID)
        {
            emailTokenID = "";
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                emailTokenID = token.ID = this.GenerateID();
                token.StartDateTime = DateTime.Now;
                context.EmailTokens.Add(token);
                context.SaveChanges();
            }
        }
        
        public override void Update(EmailToken token)
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                var DBToken = context.EmailTokens.FirstOrDefault(t => t.ID == token.ID);
                DBToken = token;
                context.SaveChanges();
            }
        }

        public override void Delete(EmailToken token)
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                context.EmailTokens.Remove(token);
                context.SaveChanges();
            }
        } 
        #endregion
    }
}
