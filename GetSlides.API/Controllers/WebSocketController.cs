using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Microsoft.Web.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Web.WebSockets;
using System.Net.WebSockets;
using MVC = System.Web.Mvc;

namespace GetSlides.API.Controllers
{
    public class WebSocketController : ApiController
    { 
        [HttpGet]
        [MVC.RequireHttps]
        [Route("~/api/Present")]
        public HttpResponseMessage GetSubject(string user, string data)
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                var subject = (WebSocketSubject)WebSocketFactory.CreateSubject();
                HttpContext.Current.AcceptWebSocketRequest(subject); 
                return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);  
            }
            else
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        [MVC.RequireHttps]
        [Route("~/api/WatchPresentation")]
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
            return 5;
        }
    }
}
