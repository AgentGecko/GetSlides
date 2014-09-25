using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;

namespace GetSlides.API
{
    public class WebSocketSubject : WebSocketHandler, ISubject
    {
        private List<IObserver> observers;

        public WebSocketSubject() 
        {
            this.observers = new List<IObserver>();
        }

        public void Subscribe(IObserver observer)
        {
            this.observers.Add(observer);
        }
        public void Unsubscribe(IObserver observer)
        {
            this.observers.Remove(observer);
        }
        public void Notify(string msg) 
        {
            this.observers.ForEach(x => x.Update(msg));
        }
        public override void OnOpen()
        {
            this.Send("Hello! Let's do the full-duplex communication again!!");
        }
        public override void OnMessage(string message)
        {
            var msg = "You sent: " + message + " at " + DateTime.Now;
            this.Notify(message);
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