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
        [SerializeField] private Button _exitButton;

        private void Start()
        {
            _startButton.onClick.AddListener(OnStartButton);
            _exitButton.onClick.AddListener(OnExitButton);
        }

        private void OnStartButton()
        {
            InputManager.Instance.PlayerMode();
            _pauseMenu.SetActive(false);
        }

        private void OnExitButton()
        {
            _pauseMenu.SetActive(false);
            SceneManager.Instance.QuitGame();
        }
    }
}