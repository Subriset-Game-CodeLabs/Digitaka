using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UIController.Pause
{
    public class PauseController: MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private GameObject _settingsMenu;

        private void Start()
        {
            _startButton.onClick.AddListener(OnStartButton);
            _settingsButton.onClick.AddListener(() =>
            {
                _settingsMenu.SetActive(true);
            });
            _exitButton.onClick.AddListener(OnExitButton);
        }

        private void OnStartButton()
        {
            UIManager.Instance.Pause();
        }

        private void OnExitButton()
        {
            _pauseMenu.SetActive(false);
            UIManager.Instance.MainMenuGame();
            UIManager.Instance.Pause();
        }
    }
}