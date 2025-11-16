using System;

namespace Serjbal.App
{
    public class DisposeAppServcesState : AppState
    {
        protected App _app;

        public DisposeAppServcesState(App app)
        {
            _app = app;
        }

        public override bool Execute()
        {
            foreach (Type serviceType in _app.ServiceContainer.GetAllServices())
            {
                (_app.ServiceContainer.GetService(serviceType) as IDisposable)?.Dispose();
            }

            return true;
        }

        public override bool Enter() => true;
        public override bool Exit() => true;
    }
}