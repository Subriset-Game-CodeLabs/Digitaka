using System;
using Input;
using UnityEngine;
using UnityEngine.UI;

namespace UIController
{
    public class MainMenuController:MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private GameObject _settingsMenu;
        private void Start()
        {
            _startButton.onClick.AddListener(StartGame);
            _exitButton.onClick.AddListener(ExitGame);
            _settingsButton.onClick.AddListener(OpenSettings);
        }
        private void StartGame()
        {
            // Load the main game scene
            SceneManager.Instance.ChangeScene("ChapterSelector");
        }
        private void ExitGame()
        {
            Application.Quit();
        }
        private void OpenSettings()
        {
            _settingsMenu.SetActive(true);
        }
    }
}