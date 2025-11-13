using UnityEngine;

namespace Serjbal.App.UI
{
    [CreateAssetMenu]
    public class BaseViewModel : ScriptableObject, IViewModel
    {
        [InjectDependency] protected App _app;


    }
}
