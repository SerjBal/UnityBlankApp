using UnityEngine;

namespace Serjbal.App.MVVM
{
    [CreateAssetMenu]
    public class MonoViewModel : MonoBehaviour, IViewModel
    {
        [InjectDependency] protected App _app;


    }
}
