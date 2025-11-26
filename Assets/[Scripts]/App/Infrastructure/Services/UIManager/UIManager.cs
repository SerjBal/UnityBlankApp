using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Serjbal.App
{
    public sealed class UIManager : MonoBehaviour, IService
    {
        [SerializeField] private Canvases _canvases;
        [SerializeField] private PrefabsLoader _viewPrefabsLoader;
        private readonly Dictionary<PageName, GameObject> _openedPages = new Dictionary<PageName, GameObject>();
        private PageFactory _pagesFactory;
        //  AppSettingsModel _settingsModel;

        [InjectDependency] private App _app;


        public void Init()
        {
            _pagesFactory = new PageFactory(_viewPrefabsLoader, _canvases);
          //  _app.EventBus.Subscribe<OnSettingsLoadedEvent>(UpdateSettingsModel);
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

        //private void UpdateSettingsModel(OnSettingsLoadedEvent eventValue)
        //{
        //    _settingsModel = eventValue.Model;
        //}
    }
}
