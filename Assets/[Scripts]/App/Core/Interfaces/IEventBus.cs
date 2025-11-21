using System;

namespace Serjbal.App
{
    public interface IEventBus<TEvent> where TEvent : class
    {
        void Subscribe<T>(Action<T> handler) where T : TEvent;
        void Unsubscribe<T>(Action<T> handler) where T : TEvent;
        void Raise<T>(T @event) where T : TEvent;
    }
}

