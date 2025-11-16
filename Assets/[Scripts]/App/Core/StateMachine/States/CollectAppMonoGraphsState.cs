namespace Serjbal.App
{
    internal class CollectAppMonoGraphsState : AppState
    {
        private MonoApp monoApp;

        public CollectAppMonoGraphsState(MonoApp monoApp)
        {
            this.monoApp = monoApp;
        }
    }
}