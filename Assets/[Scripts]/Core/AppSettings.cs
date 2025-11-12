using UnityEngine;

namespace Serjbal.App
{
    [CreateAssetMenu]
    public class AppSettings : ScriptableObject
    {
        public PrefabRepository pagePrefabs;
        public AppSettingsModel config;
    }
}