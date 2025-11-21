using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using Serjbal.App.MVVM;

namespace Serjbal.App
{
    public class PageFactory : IFactory<PageName, IViewable>
    {
        Dictionary<PageName, PageConfig> _prefabs = new Dictionary<PageName, PageConfig>();
        private Canvases _canvases;

        public PageFactory(PageConfig[] prefabs, Canvases canvases)
        {
            _canvases = canvases;
            _prefabs = prefabs.ToDictionary(x => x.Name, x => x);
        }

        public IViewable Create(PageName pageName)
        {
            var config = _prefabs[pageName];
            IViewable viewInstance = CreateViewInstance(config);
            IViewModel viewModelInstance = CreateViewModelInstance(config, viewInstance.GameObject);
            FieldsInjector.Inject(viewInstance, viewModelInstance);

            return viewInstance;
        }

        private IViewModel CreateViewModelInstance(PageConfig config, GameObject gameObject)
        {
            var classType = config.ViewModel.GetClass();

            if (typeof(MonoBehaviour).IsAssignableFrom(classType))
            {
                return gameObject.AddComponent(classType) as IViewModel;
            } else
            {
                return Activator.CreateInstance(classType) as IViewModel;
            }
        }

        private IViewable CreateViewInstance(PageConfig config)
        {
            var canvas = config.Type == PageType.Dynamic
                            ? _canvases.DynamicCanvas
                            : _canvases.StaticCanvas;

            IViewable newPage = Factorys.Create<IViewable>(config.Name.ToString(), config.View, canvas.transform);
            return newPage;
        }
    }
}