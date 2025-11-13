using UnityEngine;
using System.Collections.Generic;
using Serjbal.App.UI;
using System;

namespace Serjbal.App.Factorys
{
    public class PagesFactory : GOFactoryBase<PageType, IViewable>
    {
        Dictionary<PageType, GameObject> _prefabsDict = new Dictionary<PageType, GameObject>();
        private Canvases _canvases;

        public PagesFactory(GameObject[] prefabs, Canvases canvases)
        {
            _canvases = canvases;
            foreach (var item in prefabs)
            {
                var itemType = (PageType)Enum.Parse(typeof(PageType), item.name);
                _prefabsDict.Add(itemType, item);
            }
        }

        public override IViewable Create(PageType pageType)
        {
            var prefab = GetPrefab(pageType);
            bool isDynamic = prefab.TryGetComponent<DynamicTeg>(out var teg);
            var canvas = isDynamic ? _canvases.DynamicCanvas : _canvases.StaticCanvas;

            IViewable newPage = Create(pageType.ToString(), prefab, canvas.transform);
            return newPage;
        }

        GameObject GetPrefab(PageType pageType)
        {
            if (_prefabsDict.ContainsKey(pageType))
                return _prefabsDict[pageType];

            Debug.LogError($"No page prefab with name {pageType}");
            return null;
        }
    }
}