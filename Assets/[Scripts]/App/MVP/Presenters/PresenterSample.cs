using UnityEngine;

namespace Serjbal.App.MVP
{
    [CreateAssetMenu]
    public class PresenterSample : MonoBehaviour, IPresenter
    {
        [Data("Text")]
        private TMPro.TextMeshProUGUI _text;
        private App _app;

        public void Init(App app)
        {
            _app = app;
        }

        private void Start()
        {
            if (_text)
            {
                _text.text = "start";
            } 
        }
    }
}
