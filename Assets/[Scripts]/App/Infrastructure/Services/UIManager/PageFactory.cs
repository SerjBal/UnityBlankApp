using UnityEngine;
using Serjbal.App.MVP;
using System;

namespace Serjbal.App
{
    public sealed class PageFactory : IFactory<PageName, IPresenter>
    {
        private Canvases _canvases;
        private PrefabsLoader _prefabsLoader;
        private FieldsInjector _viewInjector = new FieldsInjector();

        public PageFactory(PrefabsLoader viewPrefabsLoader, Canvases canvases)
        {
            _canvases = canvases;
            _prefabsLoader = viewPrefabsLoader;
        }

        public IPresenter Create(PageName pageName)
        {
            var strName = pageName.ToString();
            GameObject prefab = _prefabsLoader.Load(strName).gameObject;
            GameObject instance = GameObject.Instantiate(prefab, _canvases.DynamicCanvas.transform);
            instance.name = strName;

            var view = instance.GetComponent<IView>();
            var presenter = instance.GetComponent<IPresenter>();
            if (view == null || presenter == null)
            {
                Debug.LogError($"The page {strName} is have no View or Presenter component");
                return null;
            }
            _viewInjector.Inject(view, presenter);

            return presenter;
        }
    }
}