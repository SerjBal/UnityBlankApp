using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Serjbal.App
{
    public class SettingsStorage : IService
    {
        private string _filePath;
        private readonly IFileIO<AppSettingsModel> _storage;

        public Action OnSaved => _storage.OnSaved;
        public Action<AppSettingsModel> OnLoaded => _storage.OnLoaded;
        public Action<Exception> OnLoadFiled => _storage.OnLoadFiled;
        public Action<Exception> OnSaveFiled => _storage.OnSaveFiled;

        [InjectDependency] App _app;

        public SettingsStorage(IFileIO<AppSettingsModel> storage)
        {
            _storage = storage;
        }

        public void Init()
        {
            _filePath = Path.Combine(Application.streamingAssetsPath, "Settings.json");
            _storage.OnLoaded += OnModelLoaded;
            _storage.OnLoaded += OnModelSaved;
        }

        public void Save(AppSettingsModel data)
        {
            _storage.Save(_filePath, data);
        }

        public void Load()
        {
            _storage.Load(_filePath);
        }

        private void OnModelSaved(AppSettingsModel model)
        {
            _app.EventBus.Raise(new OnSettingsSavedEvent(model));
        }

        private void OnModelLoaded(AppSettingsModel model)
        {
            _app.EventBus.Raise(new OnSettingsLoadedEvent(model));
        }
    }
}