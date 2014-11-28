using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSlides.BLL
{
    public class EmailToken :  IBLLObject
    {
        private int id;
        public int ID { get { return this.id; } }
        public int UserID { get; set; }
        public System.DateTime StartDateTime { get; set; }

        public EmailToken() { }
        public EmailToken(int id, int userId, System.DateTime startDateTime)
        {
            this.id = id;
            this.UserID = userId;
            this.StartDateTime = startDateTime;
            
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
