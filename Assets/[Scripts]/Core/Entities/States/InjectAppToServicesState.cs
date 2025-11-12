using System;

namespace Serjbal.App
{
    public class InjectAppToServicesState : AppState
    {
        private App _app;
        private AppInjector _injector;

        public InjectAppToServicesState(App app, AppInjector injector)
        {
            _app = app;
            _injector = injector;
        }

        public override bool Execute()
        {
            foreach (Type serviceType in _app.ServiceContainer.GetAllServices())
            {
                _injector.Inject(_app, _app.ServiceContainer.GetService(serviceType));
            }

            return true;
        }

        public override bool Enter() => true;
        public override bool Exit() => true;
    }
}