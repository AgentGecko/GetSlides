using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSlides.DAL
{
    public class UserRepository : DALRepository<User>
    {
        public override ICollection<User> Select() 
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                return context.Users.ToList();
            }
        }
        public override User Select(string ID) 
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities()) 
            {
                return context.Users.FirstOrDefault(t => t.ID == ID);
            }
        }
        public override void Create(User user) 
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities()) 
            {
                user.ID = this.GenerateID();
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
        public override void Update(User user) 
        {
            using(GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                var DBUser = context.Users.FirstOrDefault(t => t.ID == user.ID);
                DBUser = user;
                context.SaveChanges();
            }
        }
        public override void Delete(User user) 
        {
            using(GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
        public AuthToken GetLatestToken(User user) 
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                return context.Users.FirstOrDefault(t => t.ID == user.ID).AuthTokens.Where(t => t.StartDateTime + TimeSpan.Parse(t.Timespan.ToString()) > DateTime.Now).OrderByDescending(t => t.StartDateTime).FirstOrDefault();
            }
        }
        public EmailToken GetLatestEmailToken(User user) 
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities()) 
            {
                return context.Users.FirstOrDefault(t => t.ID == user.ID).EmailTokens.OrderByDescending(t => t.StartDateTime).FirstOrDefault();
            }
        }
    
    }
}
