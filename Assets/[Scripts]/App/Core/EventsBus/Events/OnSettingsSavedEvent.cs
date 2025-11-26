namespace Serjbal.App
{
    public class OnSettingsSavedEvent : AppEvent
    {
        public AppSettingsModel Model { get; private set; }

        public OnSettingsSavedEvent(AppSettingsModel model)
        {
            Model = model;
        }
    }
}