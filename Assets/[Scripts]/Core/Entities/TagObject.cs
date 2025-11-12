using UnityEngine;

namespace Serjbal
{
    public class TagObject : UnityEngine.MonoBehaviour, IFindable
    {
        public GameObject GemeObject => gameObject;
    }
}
