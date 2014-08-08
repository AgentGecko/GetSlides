using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace GetSlides.BLL
{
    public abstract class Validation : IValidation
    {
        public abstract bool Validate();

        #region ProtectedMethods
        protected bool ValidateInput(object item)
        {
            // Input strings should have a defined max lenght. 20 is just for non error purposes
            if (((string)item).Length > 20)
                return false;

            return false;
        }

        #region UserValidation
        protected bool ValidateInputEmail(string email)
        {
            UserRepository userRepo = new UserRepository();

            // Regular Expression for testing whether the input is an valid e-mail pattern.
            if (System.Text.RegularExpressions.Regex.IsMatch(email, @"/^(\w+|(\w+\.\w+)+)@\w+\.\w+$/",RegexOptions.ECMAScript))
               if(!userRepo.EmailExists(email))
                    return true;
           
            return false;
        }
        protected bool ValidateInputUsername(string username)
        {
            UserRepository userRepo = new UserRepository(); 

            // Regular Expression for testing whether the input contains only alphanumeric characters and the underscore.
            if (System.Text.RegularExpressions.Regex.IsMatch(username, @"/^\w+$", RegexOptions.ECMAScript))
               if(!userRepo.UsernameExists(username))
                    return true;
            return false;
        }
        protected bool ValidateInputPassword(string password) 
        {
            // Regular Expession for testing whether the input contains at least one alphanumeric characters.
            if (System.Text.RegularExpressions.Regex.IsMatch(password, "/[a-z]+/", RegexOptions.ECMAScript)
            &&  System.Text.RegularExpressions.Regex.IsMatch(password, @"/\d+/", RegexOptions.ECMAScript))
                return true;
            return false;
        }
        #endregion

        /*
        public static bool UsernameExists(object username) 
        {
            UserRepository bllRepo = new UserRepository();
            return bllRepo.Select().Select(t => t.Username == username.ToString()).FirstOrDefault();
        }
        public static bool EmailExists(object email) 
        {
            UserRepository bllRepo = new UserRepository();
            return bllRepo.Select().Select(t => t.Email == email.ToString()).FirstOrDefault();
        }
       */
        #endregion
    }
}