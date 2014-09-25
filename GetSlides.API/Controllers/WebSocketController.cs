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
        private static ConcurrentDictionary<int, ISubject> _subjects = new ConcurrentDictionary<int, ISubject>();
        public HttpResponseMessage Get(string user, string data)
        {
            if (HttpContext.Current.IsWebSocketRequest)
            {
                if(data.Substring(data.IndexOf('-')-1,1) == "1")
                {
                    var subject = new WebSocketSubject();
                    _subjects.GetOrAdd( Int32.Parse(data.Substring(data.LastIndexOf('-') + 1)), subject);
                    HttpContext.Current.AcceptWebSocketRequest(subject);
                }
                else
                {
                    var observer = new WebSocketObserver();
                        _subjects.First(t => t.Key == Int32.Parse(data.Substring(data.LastIndexOf('-') + 1))).Value.Subscribe(observer);    
                        HttpContext.Current.AcceptWebSocketRequest(observer);
                }
                
                
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }

        
    }
}
