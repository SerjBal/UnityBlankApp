using System;
using UnityEngine;

namespace Serjbal.App
{
    public class InjectServicesToServicesState : AppState
    {
        private App _app;
        private AppInjector _injector;

        public InjectServicesToServicesState(App app, AppInjector injector)
        {
            _app = app;
            _injector = injector;
        }

        public override bool Execute()
        {
            var allServices = _app.ServiceContainer.GetAllServices();
            foreach (Type client in allServices)
            {
                foreach (Type dependency in allServices)
                {
                    _injector.Inject(_app.ServiceContainer.GetService(dependency), _app.ServiceContainer.GetService(client));
                }
            }

            return true;
        }

        public override bool Enter() => true;
        public override bool Exit() => true;
    }
}