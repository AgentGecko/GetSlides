using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSlides.DAL
{
    public class UserRepository : DALRepository<User>
    {
        #region CRUD
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
            // Ovo bi trebalo bit bool da vrati false ako je postojao. To treba nekako diferencirati.
            using (GetSlidesDBEntities context = new GetSlidesDBEntities()) 
            {
                if(!UsernameExists(user.Username) && !EmailExists(user.Email))
                {
                    user.ID = this.GenerateID();
                    context.Users.Add(user);
                    context.SaveChanges();
                } 
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
        #endregion

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
        public bool UsernameExists(string username) 
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities()) 
            {
                if (context.Users.Select(t => t.Username == username).FirstOrDefault())
                    return true;
                else
                    return false;
            }
        }
        public bool EmailExists(string email) 
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                if (context.Users.Select(t => t.Email == email).FirstOrDefault())
                    return true;
                else
                    return false;
            }
        }
        public bool PasswordExists(string passwordHash)
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                if (context.Users.Select(t => t.PasswordHash == passwordHash).FirstOrDefault())
                    return true;
                else 
                    return false;
            }
        }
        public bool LogInValidation(string email, string passwordHash) 
        {
            using (GetSlidesDBEntities context = new GetSlidesDBEntities())
            {
                var user = context.Users.FirstOrDefault(t => t.Email == email);
                if (passwordHash == user.PasswordHash)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
