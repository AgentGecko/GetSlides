﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;

namespace GetSlides.API
{
    public class WebSocketSubject : _WebSocketHandler, ISubject
    {
        private List<IObserver> observers;

        public WebSocketSubject() 
        {
            this.observers = new List<IObserver>();
        }

        void Subscribe(IObserver observer)
        {
            this.observers.Add(observer);
        }
        void Unsubscribe(IObserver observer)
        {
            this.observers.Remove(observer);
        }
        void Notify() 
        {
            this.observers.ForEach(x => x.Update());
        }

    }
}