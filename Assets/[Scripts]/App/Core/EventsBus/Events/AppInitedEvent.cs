namespace Serjbal.App
{
    internal class AppInitedEvent : AppEvent
    {
        private MonoApp monoApp;

        public AppInitedEvent(MonoApp monoApp)
        {
            this.monoApp = monoApp;
        }
    }
}