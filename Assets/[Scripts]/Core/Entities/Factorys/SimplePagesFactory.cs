using UnityEngine;
using System.Collections.Generic;
using Serjbal.App.UI;

namespace Serjbal.App.Factorys
{
    public class SimplePagesFactory : GOFactoryBase<PageType, IViewable>
    {
        readonly Dictionary<string, GameObject> _prefabsDict = new Dictionary<string, GameObject>();
        readonly private Canvases _canvases;
        private App _app;

        public SimplePagesFactory(App app, Canvases canvases)
        {
            _app = app;
            foreach (var item in app.Settings.pagePrefabs.Prefabs)
                _prefabsDict.Add(item.name, item);
            _canvases = canvases;
        }

        public override IViewable Create(PageType pageType)
        {
            var prefab = GetPrefab(pageType);
            bool isDynamic = prefab.TryGetComponent<DynamicTeg>(out var teg);
            var canvas = isDynamic ? _canvases.DynamicCanvas : _canvases.StaticCanvas;

            IViewable newPage = Create(pageType.ToString(), prefab, canvas.transform);
            newPage.App = _app;
            return newPage;
        }


        GameObject GetPrefab(PageType pageType)
        {
            var name = pageType.ToString();
            if (_prefabsDict.ContainsKey(name))
                return _prefabsDict[name];

            Debug.LogError($"No page prefab with name {name}");
            return null;
        }
    }
}