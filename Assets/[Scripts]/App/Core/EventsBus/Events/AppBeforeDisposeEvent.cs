namespace Serjbal.App
{
    internal class AppBeforeDisposeEvent : AppEvent
    {
        private MonoApp monoApp;

        public AppBeforeDisposeEvent(MonoApp monoApp)
        {
            this.monoApp = monoApp;
        }
    }
}