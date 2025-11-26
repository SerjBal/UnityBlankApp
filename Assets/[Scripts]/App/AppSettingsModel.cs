using System;
using UnityEngine;

namespace Serjbal.App
{
    [Serializable]
    public class AppSettingsModel
    {
        [Header("App Settings")]
        public string version = "1.0.0";
        public string language = "en";

        [Header("Audio Settings")]
        [Range(0f, 1f)] public float masterVolume = 1f;
        [Range(0f, 1f)] public float musicVolume = 0.8f;
        [Range(0f, 1f)] public float sfxVolume = 0.8f;
        public bool muteAudio = false;

        [Header("Graphics Settings")]
        //public int qualityLevel = 2;
        //public Resolution resolution = new Resolution { width = 1920, height = 1080 };
        public bool fullscreen = true;
        //public float brightness = 1f;

        [Header("Input Settings")]
        public float mouseSensitivity = 1f;
        public bool invertYAxis = false;

    }
}