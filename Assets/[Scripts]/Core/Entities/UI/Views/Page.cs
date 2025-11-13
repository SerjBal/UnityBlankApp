using Serjbal.App.Factorys;
using UnityEngine;

namespace Serjbal.App.UI
{
    public class Page : MonoBehaviour, IViewable
    {
        [SerializeField] BaseViewModel _viewModel;

        public IViewModel ViewModel => _viewModel;

        public GameObject GameObject => gameObject;

        public virtual void Show(bool isTrue) => gameObject.SetActive(isTrue);
        public virtual void Show() => Show(true);
        public virtual void Hide() => Show(false);
        public virtual void Close() => Destroy(gameObject);
    }

    public interface IViewable
    {
        GameObject GameObject { get; }
        IViewModel ViewModel { get; }
        void Show();
        void Hide();
        void Close();
    }
}