using UnityEngine;

namespace Serjbal.App.MVVM
{
    [CreateAssetMenu]
    public class BaseViewModelCopy : MonoViewModel, IViewModel
    {
        [Data("Text")]
        private TMPro.TextMeshProUGUI _text;

        private void Start()
        {
            if (_text)
            {
                _text.text = "start";
            } 
        }
    }
}
