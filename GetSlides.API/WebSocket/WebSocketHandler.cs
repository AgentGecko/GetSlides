using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;

namespace GetSlides.API
{
    public class _WebSocketHandler : WebSocketHandler
    {
        public override void OnOpen() 
        {
            this.Send("Hello! Let's do the full-duplex communication again!!");
        }
        public override void OnMessage(string message) 
        {
            var msg = "You sent: " + message + " at " + DateTime.Now;
            this.Send(msg);
        }
        public override void OnClose()
        {
            base.OnClose();
        }
        public override void OnError()
        {
            base.OnError();
        }
    }
}