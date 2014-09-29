using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace GetSlides.Utility
{
    public class MD5Hash
    {
        public static string CreateHash(string input)
        {
            using(MD5 md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                    sBuilder.Append(data[i].ToString("x2"));

                return sBuilder.ToString();
            }
        }

        public static bool ValidateContent(string input, string hash) 
        {
            using(MD5 md5 = MD5.Create())
            {
                string inputHash = CreateHash(input);

                if (StringComparer.OrdinalIgnoreCase.Compare(inputHash, hash) == 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
