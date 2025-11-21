using UnityEngine;

namespace Serjbal.App.MVP
{
    public class MainPage : Page, IView
    {
        [Data("Text")]
        [SerializeField] TMPro.TextMeshProUGUI _text;

    }
}
