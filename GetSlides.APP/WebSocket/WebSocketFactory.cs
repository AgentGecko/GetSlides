using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;

namespace GetSlides.APP.WebSocket
{
    public static class WebSocketFactory
    {
        public static ConcurrentDictionary<int, ISubject> _subjects = new ConcurrentDictionary<int, ISubject>();

        public static void Initialize() 
        {
            for (int i = 0; i < 10000; i++)
            {
                _subjects.TryAdd(i, null);
            }
        }
        public static int GeneratePin() 
        {
            return _subjects.First(t => t.Value == null).Key;
        }
        public static ISubject CreateSubject(string _userID, out int subjectPin, string _presentationURI) 
        {
            int value;
            subjectPin = value = WebSocketFactory.GeneratePin();
            var Pair = _subjects.First(t => t.Key == value);
            WebSocketSubject subject = new WebSocketSubject(Pair.Key, _userID, _presentationURI);
            if (_subjects.TryUpdate(Pair.Key, subject, Pair.Value))
                return subject;
            else
                return null;
        }
        public static IObserver CreateObserver(int pin)
        {
            if (IsActive(pin))
            {
                WebSocketObserver observer = new WebSocketObserver();
                ISubject subject;
                if (_subjects.TryGetValue(pin, out subject))
                {
                    subject.Subscribe(observer);
                    return observer;
                }
                else
                    return null;
            }
            else
            {
                // Ask for another pin because the one supplied isn't active or valid.
                return null;

            }
        }
        public static bool IsActive(int pin)
        {
            ISubject outSubject;
            bool isActive = _subjects.TryGetValue(pin, out outSubject);
            if (outSubject == null) 
                return false;
            if (isActive == false) 
                return false;
            return true;

        }
    }
}