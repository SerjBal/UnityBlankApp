namespace Serjbal.App
{
    internal class AppDisposedState : AppState
    {
        private App app;

        public AppDisposedState(App app)
        {
            this.app = app;
        }
    }
}