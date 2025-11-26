using Serjbal.Utils;
using UnityEngine;

namespace Serjbal.App.MVVM
{
    public class MainPage : Page, IView
    {
        [SerializeField] TMPro.TextMeshProUGUI _text;

        [Data("Title")]
        private ReactiveProperty<string> Text = new ReactiveProperty<string>();

        private void Awake()
        {
            Text.OnChanged += (value) => _text.text = value;
        }
    }
}
