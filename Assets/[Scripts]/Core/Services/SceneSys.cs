using System;
using Serjbal.App.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Serjbal.App
{
    public class SceneSys : IService
    {
        public Action OnSceneLoadStart;
        public Action OnSceneLoadFinish;

        Scene _currentScene;
        int _currentSceneIndex;

        [InjectDependency] UIService _uiService;

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

            _uiService.ShowPage(PageType.LoaderPage);
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

                _uiService.ClosePage(PageType.LoaderPage);
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