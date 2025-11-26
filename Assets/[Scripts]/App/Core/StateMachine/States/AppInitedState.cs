
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
            _app.ServiceContainer.GetService<UIManager>().ShowPage(PageName.MainPage);
            return true;
        }
    }
}