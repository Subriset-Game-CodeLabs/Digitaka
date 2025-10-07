using System;
using UnityEngine;

namespace UIController
{
    public class MobileUIController: MonoBehaviour
    {
        [SerializeField] private GameObject _pauseButton;
        [SerializeField] private GameObject _mapButton;
        [SerializeField] private GameObject _joystick;
        [SerializeField] private GameObject _actionsButton;

        private void Awake()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneChanged;
        }

        private void OnDestroy()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneChanged;
        }
        private void OnSceneChanged(UnityEngine.SceneManagement.Scene  scene, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode)
        {
            if (scene.name == "Cutscene_Prologue")
            {
                _mapButton.SetActive(false);
                _pauseButton.SetActive(false);
                return;
            }
            _mapButton.SetActive(true);
            _pauseButton.SetActive(true);
            
        }
    }
}