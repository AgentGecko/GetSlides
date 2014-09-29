using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;

namespace GetSlides.API
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
        public static ISubject CreateSubject() 
        {
            var Pair = _subjects.First(t => t.Value == null);
            if (_subjects.TryUpdate(Pair.Key, new WebSocketSubject(Pair.Key), Pair.Value))
                return Pair.Value;
            else
                // trebao bi bit custom exception
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
                return null;
        }
        public static bool IsActive(int pin)
        {
            ISubject outSubject;
            bool isActive = _subjects.TryGetValue(pin, out outSubject);
            if (outSubject == null) 
                return false;
            if (isActive == false) // the key is invalid
                return false;
            return true;

        }
    }
}