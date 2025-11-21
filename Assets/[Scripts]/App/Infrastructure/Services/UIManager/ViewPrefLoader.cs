using System.Collections.Generic;
using UnityEngine;

namespace Serjbal.App
{
    public class ViewPrefLoader : PrefabsLoader
    {
        [SerializeField] MonoBehaviour[] _prefabs;
        Dictionary<string, MonoBehaviour> _prefabDict = new Dictionary<string, MonoBehaviour>();

        public override MonoBehaviour[] Load() => _prefabs;

        public override MonoBehaviour Load(string name)
        {
            if (_prefabDict.Count == 0)
            {
                foreach (var item in _prefabs)
                {
                    if (item)
                        _prefabDict.Add(item.name, item);
                }
            }

            if (_prefabDict.ContainsKey(name))
                return _prefabDict[name];

            Debug.Log($"PrefabsLoader: Prefab {name} is not exists");
            return null;
        }
    }
}
