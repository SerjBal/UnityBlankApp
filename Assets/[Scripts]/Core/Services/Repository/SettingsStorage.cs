using System;
using UnityEngine;

namespace Serjbal.App
{
    public class SettingsStorage : IService
    {
        private string _path;
        private string _name;
        private readonly IFileIO<AppSettingsModel> _storage;

        public Action OnSaved => _storage.OnSaved;
        public Action<AppSettingsModel> OnLoaded => _storage.OnLoaded;
        public Action<Exception> OnLoadFiled => _storage.OnLoadFiled;
        public Action<Exception> OnSaveFiled => _storage.OnSaveFiled;

        public SettingsStorage(IFileIO<AppSettingsModel> storage) =>
            _storage = storage;

        public void Init()
        {
            _path = Application.streamingAssetsPath;
            _name = "Settings.json";
        }

        public void Save(AppSettingsModel data)
        {
            _storage.Save(_path, _name, data);
        }

        public void Load()
        {
            _storage.Load(_path, _name);
        }
    }
}