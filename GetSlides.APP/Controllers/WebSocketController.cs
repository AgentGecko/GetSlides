using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Web.WebSockets;
using GetSlides.APP.WebSocket;

namespace GetSlides.APP.Controllers
{
    [RoutePrefix("api/ws")]
    public class WebSocketController : ApiController
    {
        [HttpGet]
        //[Authorize]
        [Route("present")]
        public dynamic GetSubject()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                try
                {
                    int UserPin;
                    var subject = (WebSocketSubject) WebSocketFactory.CreateSubject(User.Identity.Name, out UserPin);
                    HttpContext.Current.AcceptWebSocketRequest(subject);
                    HttpContext.Current.Response.StatusCode = (int) HttpStatusCode.SwitchingProtocols;
                    return UserPin;
                }
                catch (Exception ex) 
                {
                    throw ex;
                }
                
            }
            else
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
            
        

        [HttpGet]
        [Route("watch/{presentationPin}")]
        public HttpResponseMessage GetObserver(string presentationPin)
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                var observer = (WebSocketObserver)WebSocketFactory.CreateObserver(GetPin(presentationPin));
                HttpContext.Current.AcceptWebSocketRequest(observer);
                return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        private int GetPin(string pin) 
        {
            return Int32.Parse(pin);
        }
    }
    
}
