using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;

namespace GetSlides.API
{
    public class WebSocketObserver : _WebSocketHandler, IObserver
    {
        public void Update() 
        {
            this.Send("Subject updated some content.");
        }
    }
}