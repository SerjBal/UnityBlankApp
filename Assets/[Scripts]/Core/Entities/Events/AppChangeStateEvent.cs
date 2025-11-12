namespace Serjbal.App
{
    public class AppChangeStateEvent : AppEvent
    {
        private MonoApp monoApp;
        private IState state;

        public AppChangeStateEvent(MonoApp monoApp, IState state)
        {
            this.monoApp = monoApp;
            this.state = state;
        }
    }
}