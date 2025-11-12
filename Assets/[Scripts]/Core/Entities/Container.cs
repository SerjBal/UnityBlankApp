using System.Collections.Generic;
using UnityEngine;

namespace Serjbal
{
    public class Container : MonoBehaviour, IFindable
    {
        [SerializeField] IFindable[] _components;
        Dictionary<string, GameObject> _componentsDictionary;

        private void Awake()
        {
            FindAllComponents();
            DictionaryInit();
        }

        public virtual T GetGOComponent<T>(string goName) where T : MonoBehaviour
        {
            if (_componentsDictionary.ContainsKey(goName))
            {
                var gameObject = _componentsDictionary[goName];
                return gameObject as T;
            }
            Debug.LogWarning($"No component found with name {goName}");
            return null;
        }

        public GameObject GemeObject => gameObject;

        private void DictionaryInit()
        {
            _componentsDictionary = new Dictionary<string, GameObject>();
            foreach (var item in _components)
            {
                var obj = item.GemeObject;
                _componentsDictionary.Add(obj.name, obj);
            }
        }

        private void FindAllComponents()
        {
            if (_components == null)
            {
                _components = transform.GetComponentsInChildren<IFindable>(true);
            }
        }
    }

    public interface IFindable
    {
        GameObject GemeObject { get; }
    }
}
