using UnityEngine;

namespace Serjbal.App
{
    [CreateAssetMenu]
    public class UISettings : ScriptableObject
    {
        [SerializeField] private PageConfig[] _prefabs;
        public PageConfig[] Prefabs => _prefabs;
    }
}