using UnityEngine;

namespace Serjbal.App
{
    public abstract class PrefabsLoader : MonoBehaviour
    {
        public abstract MonoBehaviour[] Load();
        public abstract MonoBehaviour Load(string name);
    }
}