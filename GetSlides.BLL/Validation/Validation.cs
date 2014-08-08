using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace GetSlides.BLL
{
    public class Validation
    {       
        public static bool ValidateInput(object item)
        {
            // Input strings should have a defined max lenght. 20 is just for non error purposes
            if (((string)item).Length > 20)
                return false;

            return false;
        }

        public static bool ValidateInputEmail(object email)
        {
            // Regular Expression for testing whether the input is an valid e-mail pattern
            if (System.Text.RegularExpressions.Regex.IsMatch(((string)email), @"/^(\w+|(\w+\.\w+)+)@\w+\.\w+$/",RegexOptions.ECMAScript))
                if(!EmailExists(email))
                    return true;
           
            return false;
        }
        public static bool ValidateInputUsername(object username)
        {
            // Regular Expression for testing whether the input contains only alphanumeric characters and the underscore
            if (System.Text.RegularExpressions.Regex.IsMatch(((string)username), @"/^\w+$", RegexOptions.ECMAScript))
                if (!UsernameExists(username))
                    return true;
            return false;
        }

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
    }
}