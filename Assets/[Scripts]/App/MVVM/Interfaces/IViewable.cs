using UnityEngine;

namespace Serjbal.App.MVVM
{
    public interface IViewable
    {
        GameObject GameObject { get; }
        void Show();
        void Hide();
        void Close();
    }
}