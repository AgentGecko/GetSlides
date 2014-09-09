using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Microsoft.Web.WebSockets;
using System.Threading;

namespace GetSlides.API.Controllers
{
    public class WebSocketController : ApiController
    {
        MyWebSocketHandler webSocketHandler;

        public HttpResponseMessage Get()
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(new _WebSocketHandler());
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }

    }
}
