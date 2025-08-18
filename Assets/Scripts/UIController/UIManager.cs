using System;
using Input;
using TwoDotFiveDimension;
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
        [SerializeField] private bool isPaused = false;
        private PlayerStats _playerStats;
        public void SetPause(bool value)
        {
            isPaused = value;
        }
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
        private void Start()
        {
            _playerStats = PlayerStats.Instance;
        }

        void OnEnable()
        {
            InputManager.Instance.PlayerInput.Pause.OnDown += Pause;
            GameEventsManager.Instance.PlayerActionsEvents.OnHealthPotionUsed += StartCooldownHealthPotion;
            GameEventsManager.Instance.PlayerActionsEvents.OnManaPotionUsed += StartCooldownManaPotion;
            GameEventsManager.Instance.PlayerActionsEvents.OnDashPerformed += StartCooldownDash;
            GameEventsManager.Instance.PlayerActionsEvents.OnUltimatePerformed += StartCooldownUltimate;
        }
        void OnDisable()
        {
            InputManager.Instance.PlayerInput.Pause.OnDown -= Pause;
            GameEventsManager.Instance.PlayerActionsEvents.OnHealthPotionUsed -= StartCooldownHealthPotion;
            GameEventsManager.Instance.PlayerActionsEvents.OnManaPotionUsed -= StartCooldownManaPotion;
            GameEventsManager.Instance.PlayerActionsEvents.OnDashPerformed -= StartCooldownDash;
            GameEventsManager.Instance.PlayerActionsEvents.OnUltimatePerformed -= StartCooldownUltimate;
        }
  
        public void StartCooldownUltimate()
        {
            if (_ultimateCooldownUI != null)
            {
                _ultimateCooldownUI.StartCooldown(_playerStats.ultimateCooldown);
            }
        }
        public void StartCooldownHealthPotion()
        {
            if (_healthPotionCooldownUI != null)
            {
                _healthPotionCooldownUI.StartCooldown(_playerStats.healthPotionCooldown);
            }
        }
        public void StartCooldownManaPotion()
        {
            if (_manaPotionCooldownUI != null)
            {
                _manaPotionCooldownUI.StartCooldown(_playerStats.manaPotionCooldown);
            }
        }
        public void StartCooldownDash()
        {
            if (_dashCooldownUI != null)
            {
                _dashCooldownUI.StartCooldown(_playerStats.dashCooldown);
                Debug.Log("Dash cooldown started: " + _playerStats.dashCooldown);
            }
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