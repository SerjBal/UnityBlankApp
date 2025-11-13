using UnityEngine;

namespace Serjbal.App
{
    [CreateAssetMenu]
    public class AppSettings : ScriptableObject
    {
        public PrefabRepository pageViewPrefabs;
        public AppSettingsModel config;
    }
}