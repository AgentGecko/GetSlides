using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSlides.BLL
{
    public class EmailTokenRepository : IRepository<EmailToken>
    {
        DAL.EmailTokenRepository emailRepo;

        #region CRUD
        public ICollection<EmailToken> Select()
        {
            return this.CollectionFromDAL(this.emailRepo.Select());
        }
        public EmailToken Select(string ID)
        {
            return EmailToken.FromDALObject(this.emailRepo.Select(ID));
        }
        public void Update(EmailToken emailToken)
        {
            this.emailRepo.Update(emailToken.ToDALObject());
        }
        public void Delete(EmailToken emailToken)
        {
            this.emailRepo.Delete(emailToken.ToDALObject());
        }
        public void Create(EmailToken emailToken)
        {
            this.emailRepo.Create(emailToken.ToDALObject());
        }
        public void Create(EmailToken emailToken, out string mailToken, string controlDigit)
        {
            this.emailRepo.Create(emailToken.ToDALObject(), out mailToken, controlDigit);
        }
        #endregion

        public EmailTokenRepository() 
        {
            this.emailRepo = new DAL.EmailTokenRepository();
        }
        private ICollection<EmailToken> CollectionFromDAL(ICollection<DAL.EmailToken> emailTokens)
        {
            var BLLList = new HashSet<EmailToken>();
            foreach (var token in emailTokens)
            {
                BLLList.Add(EmailToken.FromDALObject(token));
            }
            return BLLList;
        }    
    
    }
}
