using UnityEngine;
using Serjbal.App.MVVM;

namespace Serjbal.App
{
    public sealed class PageFactory : IFactory<PageName, IViewModel>
    {
        private Canvases _canvases;
        private PrefabsLoader _prefabsLoader;

        public PageFactory(PrefabsLoader viewPrefabsLoader, Canvases canvases)
        {
            _canvases = canvases;
            _prefabsLoader = viewPrefabsLoader;
        }

        public IViewModel Create(PageName pageName)
        {
            var strName = pageName.ToString();
            GameObject prefab = _prefabsLoader.Load(strName).gameObject;
            GameObject instance = GameObject.Instantiate(prefab, _canvases.DynamicCanvas.transform);
            instance.name = strName;

            var view = instance.GetComponent<IView>();
            var presenter = instance.GetComponent<IViewModel>();
            if (view == null || presenter == null)
            {
                Debug.LogError($"The page {strName} is have no View or Presenter component");
                return null;
            }

            var binder = instance.AddComponent<Binder>();
            binder.Bind(view, presenter);

            return presenter;
        }
    }
}