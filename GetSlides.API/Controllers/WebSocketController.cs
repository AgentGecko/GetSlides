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

namespace GetSlides.API.Controllers
{
    public class WebSocketController : ApiController
    {
        private static ConcurrentDictionary<ISubject, int> _subjects;
        public HttpResponseMessage Get(string user, string data)
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                HttpContext.Current.AcceptWebSocketRequest(new _WebSocketHandler());
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }

        
    }
}
