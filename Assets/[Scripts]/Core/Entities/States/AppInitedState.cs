namespace Serjbal.App
{
    public class AppInitedState : AppState
    {
        private App _app;

        public AppInitedState(App app)
        {
            _app = app;
        }

        public override bool Execute()
        {
            var ui = (UIService)_app.ServiceContainer.GetService(typeof(UIService));
            ui.ShowPage(UI.PageType.MainPage);
            return true;
        }
    }
}