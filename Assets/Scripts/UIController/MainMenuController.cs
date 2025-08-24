using System;
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
            SceneManager.Instance.ChangeScene("A1New");
        }
        private void ExitGame()
        {
            // Exit the application
            Application.Quit();
        }
        private void OpenSettings()
        {
            // Open settings menu (implementation depends on your settings menu setup)
            Debug.Log("Settings button clicked");
            _settingsMenu.SetActive(true);
            
        }
    }
}