using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSlides.BLL
{
    public class AuthTokenRepository : IRepository<AuthToken>
    {
        DAL.AuthTokenRepository authRepo;

        #region CRUD
        public ICollection<AuthToken> Select()
        {
            return this.CollectionFromDAL(this.authRepo.Select());
        }
        public AuthToken Select(string ID)
        {
            return AuthToken.FromDALObject(this.authRepo.Select(ID));
        }
        public void Update(AuthToken authToken)
        {
            this.authRepo.Update(authToken.ToDALObject());
        }
        public void Delete(AuthToken authToken)
        {
            this.authRepo.Delete(authToken.ToDALObject());
        }
        public void Create(AuthToken authToken, out string authenticationToken)
        {
            this.authRepo.Create(authToken.ToDALObject(),out authenticationToken);
        }
        public void Create(AuthToken authToken)
        {
            this.authRepo.Create(authToken.ToDALObject());
        }
        #endregion

        public AuthTokenRepository() 
        {
            this.authRepo = new DAL.AuthTokenRepository();
        }
        private ICollection<AuthToken> CollectionFromDAL(ICollection<DAL.AuthToken> authTokens)
        {
            var BLLList = new HashSet<AuthToken>();
            foreach (var token in authTokens)
            {
                BLLList.Add(AuthToken.FromDALObject(token));
            }
            return BLLList;
        }    
    }
}
