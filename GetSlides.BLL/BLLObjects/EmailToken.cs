using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSlides.BLL
{
    public class EmailToken :  IBLLObject
    {
        private string id;
        public string ID { get { return this.id; } }
        public string UserID { get; set; }
        public System.DateTime StartDateTime { get; set; }
        public string Token { get; set; }

        public EmailToken() { }
        public EmailToken(string id, string userId, System.DateTime startDateTime, string token)
        {
            this.id = id;
            this.UserID = userId;
            this.StartDateTime = startDateTime;
            this.Token = token;
        }

        public static EmailToken FromDALObject(DAL.EmailToken emailToken)
        {
            return new EmailToken(emailToken.ID, emailToken.UserID, emailToken.StartDateTime);
        }
        public DAL.EmailToken ToDALObject()
        {
            return new DAL.EmailToken { ID = this.id, StartDateTime = this.StartDateTime, UserID = this.UserID};
        }
    }
}
