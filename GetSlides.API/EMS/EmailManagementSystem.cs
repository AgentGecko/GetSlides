using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace GetSlides.API
{
    public class EmailManagementSystem
    {
        MailMessage message;
        SmtpClient client;

        public EmailManagementSystem() { }

        public void SendEmail(string userEmail, string emailMessage, string messageSubject) 
        {
            this.message = new MailMessage("do_not_reply@getslides.notcom", userEmail, messageSubject, emailMessage);
            this.client = new SmtpClient(); 
            this.client.EnableSsl = true;
            this.client.UseDefaultCredentials = true;
            client.Send(this.message);
            // After sending an email to a user, an EmailToken must be created in the DB
        }
        
    }
}