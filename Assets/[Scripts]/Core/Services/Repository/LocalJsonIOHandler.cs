using System;
using System.IO;
using Serjbal.App;
using UnityEngine;

namespace Serjbal
{
    public class JsonLocalStorage<TData> : IFileIO<TData>
    {
        private readonly TData _defaultValue;

        public JsonLocalStorage(TData defaultValue) =>
            _defaultValue = defaultValue;

        public Action OnSaved { get; set; }
        public Action<TData> OnLoaded { get; set; }
        public Action<Exception> OnLoadFiled { get; set; }
        public Action<Exception> OnSaveFiled { get; set; }

        public void Save(string folderPath, string fileName, TData data)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            try
            {
                var json = JsonUtility.ToJson(data);
                var fullPath = Path.Combine(folderPath, fileName);
                File.WriteAllText(fullPath, json);
                OnSaved?.Invoke();
            } catch (Exception ex)
            {
                Debug.LogError($"Saving error: {ex.Message}");
                OnSaveFiled?.Invoke(ex);
            }
        }

        public void Load(string folderPath, string fileName, Action<TData> onLoadedAction)
        {
            var item = Load(folderPath, fileName);
            onLoadedAction?.Invoke(item);
        }

        public TData Load(string folderPath, string fileName)
        {
            try
            {
                var fullPath = Path.Combine(folderPath, fileName);
                var json = File.ReadAllText(fullPath);
                var item = JsonUtility.FromJson<TData>(json);
                OnLoaded?.Invoke(item);
                return item;
            } catch (Exception ex)
            {
                Debug.LogError($"Loading error: {ex.Message}");
                OnLoadFiled?.Invoke(ex);
            }
            return _defaultValue;
        }
    }

    public interface IFileIO<TData>
    {
        Action OnSaved { get; set; }
        Action<TData> OnLoaded { get; set; }
        Action<Exception> OnLoadFiled { get; set; }
        Action<Exception> OnSaveFiled { get; set; }

        void Save(string folderPath, string fileName, TData data);
        void Load(string folderPath, string fileName, Action<TData> onLoadedAction);
        TData Load(string folderPath, string fileName);
    }
}