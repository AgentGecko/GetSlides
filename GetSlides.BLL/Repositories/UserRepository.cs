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

        public ICollection<User> Select() 
        {
            return this.CollectionFromDAL(this.userDALRepo.Select());
        }
        public User Select(string ID) 
        {
            return User.FromDALObject(this.userDALRepo.Select(ID));
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
        
    }
}
