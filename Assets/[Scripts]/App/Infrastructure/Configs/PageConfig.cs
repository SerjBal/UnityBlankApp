
using UnityEditor;
using UnityEngine;

namespace Serjbal.App
{
    [CreateAssetMenu(fileName = "PageConfig", menuName = "Scriptable Objects/PageConfig")]
    public class PageConfig : ScriptableObject
    {
        [SerializeField] PageName _pageName;
        [SerializeField] PageType _pageType;
        [SerializeField] GameObject _view;
        [SerializeField] MonoScript _viewModel;

        public PageName Name => _pageName;
        public PageType Type => _pageType;
        public GameObject View => _view;
        public MonoScript ViewModel => _viewModel;

    }
}
