using UnityEngine;
using Serjbal.Utils;

namespace Serjbal.App.MVVM
{
    public class ViewModel : MonoBehaviour, IViewModel
    {
        private App _app;

        [Data("Title")]
        public ReactiveProperty<string> text = new ReactiveProperty<string>();

        public void Init(App app)
        {
            _app = app;
        }

        private void Start()
        {
            text.Value = "Hello world!";
        }
    }
}
