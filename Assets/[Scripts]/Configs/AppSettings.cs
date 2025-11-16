using UnityEngine;

namespace Serjbal.App
{
    [CreateAssetMenu]
    public class AppSettings : ScriptableObject
    {
        public AppSettingsModel config;

        [Header("UI")]
        public UISettings uiSettings;
    }
}