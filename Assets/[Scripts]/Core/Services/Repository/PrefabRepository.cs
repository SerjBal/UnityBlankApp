using UnityEngine;

namespace Serjbal
{
    [CreateAssetMenu]
    public class PrefabRepository : ScriptableObject
    {
        [SerializeField] private GameObject[] _prefabs;
        public GameObject[] Prefabs => _prefabs;
    }
}