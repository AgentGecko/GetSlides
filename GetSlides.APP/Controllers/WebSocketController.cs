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
        [Route("present")]
        public HttpResponseMessage GetSubject(string user, string data)
        {
            try{
            if (HttpContext.Current.IsWebSocketRequest)
            {
                try
                {
                    int pin;
                    var subject = (WebSocketSubject)WebSocketFactory.CreateSubject(user, out pin);
                    HttpContext.Current.AcceptWebSocketRequest(subject);
                    return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
                }
                catch 
                {
                    HttpContext.Current.AcceptWebSocketRequest(new WebSocketHandler());
                    return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
                }  
            }
            else
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (Exception ex) { throw ex; }
        }

        [HttpGet]
        [Route("watch")]
        public HttpResponseMessage GetObserver(string user, string data)
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                var observer = (WebSocketObserver)WebSocketFactory.CreateObserver(GetPin(data));
                HttpContext.Current.AcceptWebSocketRequest(observer);
                return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
            }
            else
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        private int GetPin(string data) 
        {
            return Int32.Parse(data);
        }
    }
    
}
