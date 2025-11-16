namespace Serjbal.App
{
    public class OnSettingsLoadedEvent : AppEvent
    {
        public AppSettingsModel Model { get; private set; }

        public OnSettingsLoadedEvent(AppSettingsModel model)
        {
            Model = model;
        }

    }
}