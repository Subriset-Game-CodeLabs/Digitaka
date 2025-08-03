using System;
using Input;
using UIController.Stats;
using UnityEngine;

namespace UIController
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        [SerializeField] private CooldownUI _dashCooldownUI;
        [SerializeField] private CooldownUI _ultimateCooldownUI;
        [SerializeField] private CooldownUI _healthPotionCooldownUI;
        [SerializeField] private CooldownUI _manaPotionCooldownUI;
        [SerializeField] private GameObject _pauseMenu;
        private bool isPaused = false;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void StartCooldownUltimate(float duration)
        {
            if (_ultimateCooldownUI != null)
            {
                _ultimateCooldownUI.StartCooldown(duration);
            }
        }
        public void StartCooldownHealthPotion(float duration)
        {
            if (_healthPotionCooldownUI != null)
            {
                _healthPotionCooldownUI.StartCooldown(duration);
            }
        }
        public void StartCooldownManaPotion(float duration)
        {
            if (_manaPotionCooldownUI != null)
            {
                _manaPotionCooldownUI.StartCooldown(duration);
            }
        }
        public void StartCooldownDash(float duration)
        {
            if (_dashCooldownUI != null)
            {
                _dashCooldownUI.StartCooldown(duration);
            }
        }

        void OnEnable()
        {
            InputManager.Instance.PlayerInput.Pause.OnDown += Pause;
        }

        public void Pause()
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                _pauseMenu.SetActive(false);
                isPaused = false;
                InputManager.Instance.UIMode();
            }
            else
            {
                Time.timeScale = 0;
                _pauseMenu.SetActive(true);
                isPaused = true;
                InputManager.Instance.PlayerMode();
            }
        }
    }
}