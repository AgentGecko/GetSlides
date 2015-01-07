using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;

namespace GetSlides.APP.WebSocket
{
    public class WebSocketSubject : WebSocketHandler, ISubject
    {
        private List<IObserver> observers;
        private string pin;
        public string userName;
        public string lastPage;
        public string presentationUri;
        public int presentationId;

        public WebSocketSubject() { }

        public WebSocketSubject(string pin, string _userName, string _presentationUri) 
        {
            this.observers = new List<IObserver>();
            this.pin = pin;
            this.userName = _userName;
            this.lastPage = "0";
            this.presentationUri = _presentationUri;
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
            this.lastPage = msg;
            this.observers.ForEach(x => x.Update(msg));
        }

        #region WSHandler
        public override void OnOpen()
        {
            this.Send("OPEN");
        }
        public override void OnMessage(string message)
        {
            this.Notify(message);
        }
        public override void OnClose()
        {
            this.DisposeEntry();
            base.OnClose();
        }
        public override void OnError()
        {
            base.OnError();
        }
        #endregion

        public string GetPin()
        {
            return this.pin;
        }
        public int GetPresentationId() 
        {
            return this.presentationId;
        }
        public void DisposeEntry() 
        {
            this.observers.ForEach(x => x.Update("CLOSE"));
            this.observers.ForEach(x => x.Close());
            WebSocketFactory._subjects.TryUpdate(pin, null, this);
        }

    }
}