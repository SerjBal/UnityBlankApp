using UnityEngine;

namespace Serjbal.App.MVVM
{
    public interface IViewModel
    {
        GameObject gameObject { get; }

        void Init(App app);
    }
}