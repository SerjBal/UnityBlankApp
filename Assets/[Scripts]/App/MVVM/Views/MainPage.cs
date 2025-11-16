using UnityEngine;

namespace Serjbal.App.MVVM
{
    public class MainPage : Page, IViewable
    {
        [Data("Text")]
        [SerializeField] TMPro.TextMeshProUGUI _text;

    }
}
