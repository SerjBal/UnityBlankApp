using System.Collections.Generic;
using Serjbal.App.UI;

namespace Serjbal.App
{
    public class InjectAppToPrefabsState : AppState
    {
        private App _app;
        private AppInjector _injector;

        public InjectAppToPrefabsState(App app, AppInjector injector)
        {
            _app = app;
            _injector = injector;
        }

        public override bool Execute()
        {
            InjectAppToUIViewModels();
            return true;
        }

        private void InjectAppToUIViewModels()
        {
            var allViewModels = GetAllViewModels();
            InjectAppToViewModels(allViewModels);
        }

        private HashSet<IViewModel> GetAllViewModels()
        {
            HashSet<IViewModel> viewModels = new HashSet<IViewModel>();
            foreach (var item in _app.Settings.pageViewPrefabs.Prefabs)
            {
                var view = item.GetComponent<IViewable>();
                viewModels.Add(view.ViewModel);
            }
            return viewModels;
        }

        private void InjectAppToViewModels(HashSet<IViewModel> viewModels)
        {
            foreach (var viewModel in viewModels)
            {
                _injector.Inject(_app, viewModel);
            }
        }

    }
}