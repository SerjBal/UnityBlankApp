namespace Serjbal.App
{
    internal class AppDisposedEvent : AppEvent
    {
        private MonoApp monoApp;

        public AppDisposedEvent(MonoApp monoApp)
        {
            this.monoApp = monoApp;
        }
    }
}