using UnityEngine;

namespace Serjbal.App.UI
{
    public class Page : MonoBehaviour, IViewable
    {
        public App App { private get; set; }

        public virtual void Show(bool isTrue) => gameObject.SetActive(isTrue);
        public virtual void Show() => Show(true);
        public virtual void Show(bool isTrue, float delay)
        {
            gameObject.SetActive(!isTrue);
            AddTimer(isTrue, delay);
        }

        public virtual void Hide() => Show(false);
        public virtual void Close() =>
            App.ServiceContainer.GetService<UIService>().DestroyPage(this);

        private void AddTimer(bool isTrue, float delay)
        {
            var newTimer = MyTimer.Create();
            newTimer.OnStopAction = () => Show(isTrue);
            newTimer.Init(delay);
        }
    }

    public interface IViewable
    {
        App App { set; }
        GameObject gameObject { get; }
        //void Show(bool isTrue);
        //void Show(bool isTrue, float delay);
        void Show();
        void Hide();
        void Close();
    }
   
}