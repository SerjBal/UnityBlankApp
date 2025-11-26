using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Serjbal.App
{
    public sealed class SceneSys : IService
    {
        public Action OnSceneLoadStart;
        public Action OnSceneLoadFinish;

        Scene _currentScene;
        int _currentSceneIndex;

        [InjectDependency] UIManager _uiManager;

        public void Init()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void LoadScene(int sceneIndex)
        {
            Debug.Log($"SceneSys.LoadScene({sceneIndex})");

            OnSceneLoadStart?.Invoke();
            if (_currentScene)
            {
                SceneManager.UnloadSceneAsync(_currentScene.BuildIndex);
            }
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

            _uiManager.ShowPage(PageName.LoaderPage);
        }


        void OnSceneLoaded(UnityEngine.SceneManagement.Scene arg0, LoadSceneMode loadMode)
        {
            if (arg0.buildIndex == 0) return;

            if (_currentSceneIndex != arg0.buildIndex)
            {
                Debug.Log($"SceneSys.Сцена загружена!");

                _currentSceneIndex = arg0.buildIndex;
                _currentScene = FindSceneObject();
                _currentScene.Init();
                OnSceneLoadFinish?.Invoke();

                _uiManager.ClosePage(PageName.LoaderPage);
            }
        }

        Scene FindSceneObject()
        {
            var scene = UnityEngine.Object.FindFirstObjectByType<Scene>();
            if (!scene)
                Debug.LogError($"SceneSys.Сцена не найдена!");
            return scene;
        }
    }
}