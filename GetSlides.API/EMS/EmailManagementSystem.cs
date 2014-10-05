using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using RestSharp;

namespace GetSlides.API
{
    /// <summary>
    /// EmailManagementSystem is a static class for sending e-mail on behalf of GetSlides app.
    /// It uses the Mailgun e-mail service for developers. 
    /// </summary>
    public static class EmailManagementSystem
    {
        /// <summary>
        /// SendConfirmationLink is a method in the EmailManagementSystem class used to send a registration confirmation
        /// link to the user specified e-mail in order to complete the registration process.
        /// </summary>
        public static IRestResponse SendConfirmationLink(string mail, string tokID) 
        {
            RestClient client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator = new HttpBasicAuthenticator("api", "key_not_for_github_:D");

            RestRequest request = new RestRequest();
            request.AddParameter("domain", "samples.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "sandboxb5fea17d73ac4aa6b7ef752a19c5f5e4.mailgun.org/messages";
            request.AddParameter("from", "GetSlides <do_not_reply@getslides.com>");
            request.AddParameter("to", mail);
            request.AddParameter("subject", "Confirm Your E-mail");
            request.AddParameter("html", @"<html>
                                                <h1>GetSlides</h1>
                                                <p>Thank you for signing up for a GetSlides account.
                                                   You're just a second away from uploading and sharing your
                                                   presentations with other users! The last thing that we need
                                                   to do is to confirm this e-mail that was handed during the
                                                   registration process.</p>
                                                <p>Please click on the link below to confirm your e-mail address:
                                                    <a href=""https://localhost:44301/api/register?email="+ mail +"&token="
                                                     + tokID + @"></a></p>
                                                <p>If you didn't sign up, please ignore this message and have a nice day :)<p>
                                          </html>");
            request.Method = Method.POST;
            return client.Execute(request);
        }
        /// <summary>
        /// SendPasswordReset is a method in the EmailManagementSystem class called on a user's request in order to activate
        /// the process of reseting the caller's password.
        /// </summary>
        public static IRestResponse SendPasswordReset(string email, string tokID) 
        {
            RestClient client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator = new HttpBasicAuthenticator("api", "key_not_for_github_:D");

            RestRequest request = new RestRequest();
            request.AddParameter("domain", "samples.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "sandboxb5fea17d73ac4aa6b7ef752a19c5f5e4.mailgun.org/messages";
            request.AddParameter("from", "GetSlides <do_not_reply@getslides.com>");
            request.AddParameter("to", email);
            request.AddParameter("subject", "Password reset.");
            request.AddParameter("html", @"<html>
                                                <p>There has been a password change request from the GetSlides account linked
                                                with this e-mail address.</p>
                                                <p>Please click on the link below to confirm the request and change the password:
                                                    <a href=""https://localhost:44301/api/account?email=" + email +"&token="
                                                     + tokID + @"></a>
                                                </p>
                                                <p>If you didn't ask for a password reset, please ignore this message.<p>
                                          </html>");
            request.Method = Method.POST;
            return client.Execute(request);
        }

        #region SendingCustomContent
        /// <summary>
        /// SendTextMessage is a method in the EmailManagementSystem class intended for custom e-mail messages to users.
        /// </summary>
        public static IRestResponse SendTextMessage()
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest();
            return client.Execute(request);
        }
        public static IRestResponse SendTextMessage(string mail) 
        {
            RestClient client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator = new HttpBasicAuthenticator("api", "key_not_for_github_:D");

            RestRequest request = new RestRequest();
            request.AddParameter("domain","samples.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "sandboxb5fea17d73ac4aa6b7ef752a19c5f5e4.mailgun.org/messages";
            request.AddParameter("from", "GetSlides <do_not_reply@getslides.com>");
            request.AddParameter("to", mail );
            request.AddParameter("subject", "Hello from GetSlides!");
            request.AddParameter("text", "This is a default GetSlides e-mail message.");
            request.Method = Method.POST;
            return client.Execute(request);
        }
        public static IRestResponse SendTextMessage(string mail, string subject, string messageText) 
        {
            RestClient client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator = new HttpBasicAuthenticator("api", "key_not_for_github_:D");

            RestRequest request = new RestRequest();
            request.AddParameter("domain", "samples.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "sandboxb5fea17d73ac4aa6b7ef752a19c5f5e4.mailgun.org/messages";
            request.AddParameter("from", "GetSlides <do_not_reply@getslides.com>");
            request.AddParameter("to", mail);
            request.AddParameter("subject", subject);
            request.AddParameter("text", messageText);
            request.Method = Method.POST;
            return client.Execute(request);
        }
        /// <summary>
        /// SendHTMLMessage is a method in the EmailManagementSystem class intended for custom e-mail messages to users.
        /// </summary>
        public static IRestResponse SendHTMLMessage() 
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest();
            return client.Execute(request);
        }
        public static IRestResponse SendHTMLMessage(string mail)
        {
            RestClient client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator = new HttpBasicAuthenticator("api", "key_not_for_github_:D");
            
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "samples.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "sandboxb5fea17d73ac4aa6b7ef752a19c5f5e4.mailgun.org/messages";
            request.AddParameter("from", "GetSlides <do_not_reply@getslides.com>");
            request.AddParameter("to", mail);
            request.AddParameter("subject", "Hello from GetSlides!");
            request.AddParameter("hmtl", "<html>This is a default GetSlides e-mail message.</html>");
            request.Method = Method.POST;
            return client.Execute(request);
        }
        public static IRestResponse SendHTMLMessage(string mail, string subject, string HTML) 
        {
            RestClient client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator = new HttpBasicAuthenticator("api", "key_not_for_github_:D"); 
            
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "samples.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "sandboxb5fea17d73ac4aa6b7ef752a19c5f5e4.mailgun.org/messages";
            request.AddParameter("from", "GetSlides <do_not_reply@getslides.com>");
            request.AddParameter("to", mail);
            request.AddParameter("subject", subject);
            request.AddParameter("hmtl", HTML);
            request.Method = Method.POST;
            return client.Execute(request);
        }
        #endregion

    }
}