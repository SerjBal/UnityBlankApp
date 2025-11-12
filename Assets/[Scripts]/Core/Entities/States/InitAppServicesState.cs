using System;

namespace Serjbal.App
{
    public class InitAppServicesState : AppState
    {
        protected App _app;

        public InitAppServicesState(App app)
        {
            _app = app;
        }

        public override bool Execute()
        {
            foreach (Type serviceType in _app.ServiceContainer.GetAllServices())
            {
                (_app.ServiceContainer.GetService(serviceType) as IInitializable)?.Init();
            }

            return true;
        }

        public override bool Enter() => true;
        public override bool Exit() => true;
    }
}