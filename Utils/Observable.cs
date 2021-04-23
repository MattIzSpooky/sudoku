using System;
using System.Collections.Generic;

namespace Utils
{
    /// <summary>
    /// See https://refactoring.guru/design-patterns/observer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Observable<T>
    {
        private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();
    
        public void Register(IObserver<T> observer)
        {
            _observers.Add(observer);
        }

        public void Unregister(IObserver<T> observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(T subject)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(subject);
            }
        }
    }
}