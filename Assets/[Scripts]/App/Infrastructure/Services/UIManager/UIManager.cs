using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Serjbal.App.MVP;

namespace Serjbal.App
{
    public sealed class UIManager : MonoBehaviour, IService
    {
        [SerializeField] Canvases _canvases;
        [SerializeField] PrefabsLoader _viewPrefabsLoader;
        IFactory<PageName, IPresenter> _pagesFactory;
        AppSettingsModel _settingsModel;
        readonly Dictionary<PageName, GameObject> _openedPages = new Dictionary<PageName, GameObject>();

        [InjectDependency] App _app;


        public void Init()
        {
            _pagesFactory = Factorys.CreatePageFactory( _viewPrefabsLoader, _canvases);
            _app.EventBus.Subscribe<OnSettingsLoadedEvent>(UpdateSettingsModel);
        }

        private void UpdateSettingsModel(OnSettingsLoadedEvent eventValue)
        {
            _settingsModel = eventValue.Model;
        }

        public void ShowPage(PageName pageType)
        {
            GetPage(pageType).SetActive(true);
        }

        public void ClosePage(PageName pageType)
        {
            var page = GetPage(pageType);
            _openedPages[pageType] = null;
            Destroy(page);
        }

        public GameObject GetPage(PageName pageType)
        {
            if (!_openedPages.ContainsKey(pageType))
            {
                var newPage = _pagesFactory.Create(pageType);
                newPage.Init(_app);
                _openedPages.Add(pageType, newPage.gameObject);
                return newPage.gameObject;
            }

            if (_openedPages[pageType] == null)
            {
                var newPage = _pagesFactory.Create(pageType);
                newPage.Init(_app);
                _openedPages[pageType] = newPage.gameObject;
                return newPage.gameObject;
            }

            return _openedPages[pageType];
        }

        public bool IsPageExist(PageName pageType, out GameObject page)
        {
            page = null;
            bool has = _openedPages.ContainsKey(pageType);
            if (has)
                page = _openedPages[pageType];

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
                    ShowPage(PageName.QuitGamePopup);
                }
            }
        }
    }
}
