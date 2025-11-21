using UnityEngine;

namespace Serjbal.App.MVP
{
    public class Page : MonoBehaviour, IView
    {
        public GameObject GameObject => gameObject;

        public virtual void Show(bool isTrue) => gameObject.SetActive(isTrue);
        public virtual void Show() => Show(true);
        public virtual void Hide() => Show(false);
        public virtual void Close() => Destroy(gameObject);
    }
}