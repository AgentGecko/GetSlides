using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetSlides.DAL;

namespace GetSlides.BLL
{
    public class UserRepository : IRepository<User>
    {
        DAL.UserRepository userDALRepo;

       
        public UserRepository() 
        {
            userDALRepo = new DAL.UserRepository();
        }

        #region CRUD 
        public ICollection<User> Select() 
        {
            return this.CollectionFromDAL(this.userDALRepo.Select());
        }
        public User Select(string ID) 
        {
            return User.FromDALObject(this.userDALRepo.Select(ID));
        }
        public void Update(User user) 
        {
            this.userDALRepo.Update(user.ToDALObject());
        }
        public void Delete(User user) 
        {
            this.userDALRepo.Delete(user.ToDALObject());
        }
        public void Create(User user)
        {
            this.userDALRepo.Create(user.ToDALObject());
        }
        public void Create(string username, string email, string password, string confirmPassword, string passwordHash)
        {
            var user = new User { Username = username, Email = email, PasswordHash = passwordHash };
            if(user.Validate(password) && !UsernameExists(username) && !EmailExists(email))
                this.Create(user);
            // else ask for new information.
        }
        #endregion

        public bool UsernameExists(string username)
        {
            DAL.UserRepository dalRepo = new DAL.UserRepository();
            return dalRepo.UsernameExists(username);
        }
        public bool EmailExists(string email) 
        {
            DAL.UserRepository dalRepo = new DAL.UserRepository();
            return dalRepo.EmailExists(email);
        }
        private ICollection<User> CollectionFromDAL(ICollection<DAL.User> users) 
        {
            var BLLList = new HashSet<User>();
            foreach (var user in users)
            {
                BLLList.Add(User.FromDALObject(user));
            }
            return BLLList;
        }
        public bool ConfirmEmailToken(string tokenID, User user)
        {
            return tokenID == this.userDALRepo.GetLatestEmailToken(user.ToDALObject()).ID;
        }
        public bool PasswordExists(string passwordHash)
        {
            DAL.UserRepository dalRepo = new DAL.UserRepository();
            return dalRepo.PasswordExists(passwordHash);
        }
        public bool LogInValidation(string email, string passwordHash)
        {
            DAL.UserRepository dalRepo = new DAL.UserRepository();
            return dalRepo.LogInValidation(email, passwordHash);
        }
    }
}
