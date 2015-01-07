using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;

namespace GetSlides.APP.WebSocket
{
    public class WebSocketObserver : WebSocketHandler, IObserver
    {
        public string lastpage;
        public WebSocketObserver() { }

        public void Update(string msg) 
        {
            this.Send(msg);
        }

        #region WSHandler
        public override void OnOpen()
        {
            this.Send(this.lastpage);
        }
        public override void OnMessage(string message)
        {
            var msg = "You sent: " + message + " at " + DateTime.Now + ". You shouldn't be doing that,you are an observer" ;
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

        public void Close()
        {
            base.Close();
        }
    }
}