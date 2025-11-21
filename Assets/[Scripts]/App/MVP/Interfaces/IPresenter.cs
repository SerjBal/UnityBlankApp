using UnityEngine;

namespace Serjbal.App.MVP
{
    public interface IPresenter
    {
        GameObject gameObject { get; }

        void Init(App app);
    }
}