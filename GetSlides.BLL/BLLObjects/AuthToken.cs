using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSlides.BLL
{
    public class AuthToken : IBLLObject
    {
        private string id;
        public string ID { get { return this.id; } }
        public string UserID { get; set; }
        public string Token { get; set; }
        public System.DateTime StartDateTime { get; set; }
        public long Timespan { get; set; }

        public AuthToken() { }
        public AuthToken(string id, string userID, string token, System.DateTime startDateTime, long timespan)
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
    }
}
