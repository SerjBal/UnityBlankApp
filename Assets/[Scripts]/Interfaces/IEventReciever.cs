namespace Serjbal.App
{
    public interface IEventReciever<T> : IBaseEventReciever where T : struct, IEvent
    {
        void OnEvent(T @event);
    }
}