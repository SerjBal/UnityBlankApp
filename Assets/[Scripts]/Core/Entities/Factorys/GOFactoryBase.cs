using UnityEngine;
using System;

namespace Serjbal.App.Factorys
{
    public abstract class GOFactoryBase<Key, Value> where Value : class
    {
        public abstract Value Create(Key objectType);

        public static Value Create(string name, GameObject prefab, Transform container)
        {
            var pageInstance = UnityEngine.Object.Instantiate(prefab, container);
            pageInstance.name = name;
            if (pageInstance.TryGetComponent<Value>(out var newPage))
            {
                return newPage;
            }

            throw new InvalidOperationException($"{prefab.name} is missing component {typeof(Value)}");
        }
    }
}