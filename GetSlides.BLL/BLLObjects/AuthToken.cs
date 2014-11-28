using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetSlides.Utility;

namespace GetSlides.BLL
{
    public class AuthToken : IBLLObject
    {
        private int id;
        public int ID { get { return this.id; } }
        public int UserID { get; set; }
        public string Token { get; set; }
        public System.DateTime StartDateTime { get; set; }
        public long Timespan { get; set; }

        public AuthToken() { }
        public AuthToken(int id, int userID, string token, System.DateTime startDateTime, long timespan)
        {
            this.id = id;
            this.UserID = userID;
            this.Token = token;
            this.StartDateTime = startDateTime;
            this.Timespan = timespan;
        }

        public static AuthToken FromDALObject(DAL.AuthToken authToken)
        {
            return new AuthToken(authToken.ID,authToken.UserID, authToken.Token, authToken.StartDateTime, authToken.Timespan);
        }
        public DAL.AuthToken ToDALObject()
        {
            return new DAL.AuthToken { ID = this.id, Timespan = this.Timespan, StartDateTime = this.StartDateTime, Token = this.Token, UserID = this.UserID };
        }

        public bool ValidateToken(string browserToken) 
        {
            bool isExpired = this.IsExpired();
            bool isLastToken = this.IsLastToken();
            bool checkSum = this.CheckSum(browserToken);
            if (!isExpired && isLastToken && checkSum)
                return true;
            else
                return false;
        }

        private bool IsExpired() 
        {
            var time = this.StartDateTime.AddDays((double)this.Timespan);
            if(time < DateTime.Now)
                return true;
            else
                return false;
        }
        private bool IsLastToken() 
        {
            UserRepository userRepo = new UserRepository();
            AuthToken lastToken = userRepo.GetLatestToken(this.UserID);
            if (this.ID == lastToken.ID)
                return true;
            else
                return false;
        }
        private bool CheckSum(string bToken) 
        {
            string generatedHash = MD5Hash.CreateHash(this.Token + this.ID + this.UserID);
            return MD5Hash.ValidateContent(generatedHash, bToken.Split(';')[2]);
        }
    }
}
