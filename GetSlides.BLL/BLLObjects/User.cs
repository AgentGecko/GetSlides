using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSlides.BLL
{
    public class User : Validation, IBLLObject
    {
        private string id;
        public string ID { get { return this.id; } }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public User() { }
        public User(string id, string username, string email, string passwordHash) 
        {
            this.id = id;
            this.Username = username;
            this.Email = email;
            this.PasswordHash = passwordHash;
        }

        public override bool Validate()
        {
            if (!ValidateInputEmail(this.Email))
                return false; // +Signal faulty email
            if (!ValidateInputUsername(this.Username))
                return false; // +Signal faulty username
            return true;
        }
        public override bool Validate(string password)
        {
            if (!ValidateInputEmail(this.Email))
                return false; // +Signal faulty email
            if (!ValidateInputUsername(this.Username))
                return false; // +Signal faulty username
            if (!ValidateInputPassword(password))
                return false;
            return true;
        }


        public static User FromDALObject(DAL.User user) 
        {
            return new User(user.ID,user.Username,user.Email,user.PasswordHash);
        }
        public DAL.User ToDALObject() 
        {
            return new DAL.User { ID = this.id, PasswordHash = this.PasswordHash, Email = this.Email, Username = this.Username };
        }
    }
}
