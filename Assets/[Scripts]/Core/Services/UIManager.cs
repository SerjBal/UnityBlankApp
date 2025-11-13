using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Serjbal.App.UI;
using Serjbal.App.Factorys;

namespace Serjbal.App
{
    public class UIManager : MonoBehaviour, IService
    {
        [SerializeField] Canvases _canvases;
        PagesFactory _pagesFactory;
        readonly Dictionary<string, IViewable> _openedPages = new Dictionary<string, IViewable>();

        [InjectDependency] App _app;


        public void Init()
        {
            _pagesFactory = new PagesFactory(_app.Settings.pageViewPrefabs.Prefabs, _canvases);
        }

        public void ShowPage(PageType pageType) => GetPage(pageType).Show();

        public void ClosePage(PageType pageType) => GetPage(pageType).Close();

        public IViewable GetPage(PageType pageType)
        {
            var pageName = pageType.ToString();

            if (_openedPages.ContainsKey(pageName))
                return _openedPages[pageName];

            var newPage = _pagesFactory.Create(pageType);
            _openedPages.Add(pageName, newPage);
                
            return newPage;
        }

        public void DestroyPage(IViewable page)
        {
            var pageGO = page.GameObject;
            var pageGO_name = pageGO.name;

            if (_openedPages.ContainsKey(pageGO_name))
                _openedPages.Remove(pageGO_name);

            Destroy(pageGO);
        }

        public bool IsPageExist(PageType pageType, out IViewable page)
        {
            var pageName = pageType.ToString();
            page = null;
            bool has = _openedPages.ContainsKey(pageName);
            if (has)
                page = _openedPages[pageName];

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
