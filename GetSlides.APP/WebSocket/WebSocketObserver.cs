using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;

namespace GetSlides.APP.WebSocket
{
    public class WebSocketObserver : WebSocketHandler, IObserver
    {
        public WebSocketObserver() { }

        public void Update(string msg) 
        {
            this.Send("Subject updated some content.");
        }

        #region WSHandler
        public override void OnOpen()
        {
            this.Send("Hello! Let's do the full-duplex communication again!!");
        }
        public override void OnMessage(string message)
        {
            var msg = "Subject sent: " + message + " at " + DateTime.Now;
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
        #endregion
    }
}