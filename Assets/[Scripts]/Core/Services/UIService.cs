using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Serjbal.App.UI;
using Serjbal.App.Factorys;

namespace Serjbal.App
{
    public class UIService : MonoBehaviour, IService
    {
        [SerializeField] Canvases _canvases;

        private readonly Dictionary<string, IViewable> _openedPages = new Dictionary<string, IViewable>();
        private SimplePagesFactory _factory;

        [InjectDependency] App _app;


        public void Init()
        {
            _factory = new SimplePagesFactory(_app, _canvases);
           // _app.EventBus.Subscribe<>():
        }

        public void ShowPage(PageType pageType) => GetPage(pageType).Show();

        public void ClosePage(PageType pageType) => GetPage(pageType).Close();

        public IViewable GetPage(PageType pageType)
        {
            var pageName = pageType.ToString();

            if (_openedPages.ContainsKey(pageName))
                return _openedPages[pageName];

            var newPage = _factory.Create(pageType);
            _openedPages.Add(pageName, newPage);

            return newPage;
        }

        public void DestroyPage(IViewable page)
        {
            var pageGO = page.gameObject;
            var pageGO_name = pageGO.name;

            if (_openedPages.ContainsKey(pageGO_name))
                _openedPages.Remove(pageGO_name);

            Destroy(pageGO);
        }

        public bool IsPageExist(string name, out IViewable page)
        {
            page = null;
            bool has = _openedPages.ContainsKey(name);
            if (has)
                page = _openedPages[name];

            return has;
        }

        public void ClearCanvas()
        {
            _openedPages.Clear();
            _canvases.Clear();
        }

        private void Update()
        {
            var currentKey = Keyboard.current;
            if (currentKey != null)
            {
                if (currentKey.escapeKey.wasPressedThisFrame)
                {
                    ShowPage(PageType.QuitGamePopup);
                }
            }
        }
    }
}
