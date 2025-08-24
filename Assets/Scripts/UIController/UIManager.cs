using System;
using Audio;
using Input;
using TMPro;
using TwoDotFiveDimension;
using UIController.Stats;
using UnityEngine;

namespace UIController
{
    public class UIManager : PersistentSingleton<UIManager>
    {
        [SerializeField] private CooldownUI _dashCooldownUI;
        [SerializeField] private CooldownUI _ultimateCooldownUI;
        [SerializeField] private CooldownUI _healthPotionCooldownUI;
        [SerializeField] private CooldownUI _manaPotionCooldownUI;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _tutorialPanel;
        [SerializeField] private GameObject _defeatedPanel;
        [SerializeField] private GameObject _completeQuestPanel;
        [SerializeField] private bool isPaused = false;
        private PlayerStats _playerStats;
        public void SetPause(bool value)
        {
            isPaused = value;
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
            GameEventsManager.Instance.StatsEvents.OnPlayerDeath += ShowDefeatedPanel;
        }
        void OnDisable()
        {
            InputManager.Instance.PlayerInput.Pause.OnDown -= Pause;
            GameEventsManager.Instance.PlayerActionsEvents.OnHealthPotionUsed -= StartCooldownHealthPotion;
            GameEventsManager.Instance.PlayerActionsEvents.OnManaPotionUsed -= StartCooldownManaPotion;
            GameEventsManager.Instance.PlayerActionsEvents.OnDashPerformed -= StartCooldownDash;
            GameEventsManager.Instance.PlayerActionsEvents.OnUltimatePerformed -= StartCooldownUltimate;
            GameEventsManager.Instance.StatsEvents.OnPlayerDeath -= ShowDefeatedPanel;
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
        public void ShowTutorialPanel()
        {
            if (_tutorialPanel != null)
            {
                _tutorialPanel.SetActive(true);
                InputManager.Instance.UIMode();
            }
        }
        
        public void HideTutorialPanel()
        {
            if (_tutorialPanel != null)
            {
                _tutorialPanel.SetActive(false);
                InputManager.Instance.PlayerMode();
            }
        }
        public void ShowDefeatedPanel()
        {
            if (_defeatedPanel != null)
            {
                AudioManager.Instance.PlaySound(SoundType.SFX_Lose);
                _defeatedPanel.SetActive(true);
                InputManager.Instance.UIMode();
            }
        } 
    
        public void Pause()
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                _pauseMenu.SetActive(false);
                isPaused = false;
                InputManager.Instance.PlayerMode();
            }
            else
            {
                Time.timeScale = 0;
                _pauseMenu.SetActive(true);
                isPaused = true;
                InputManager.Instance.UIMode();
            }
        }

        public void MainMenuGame()
        {
            SceneManager.Instance.ChangeScene("MainMenu");
        }
    }
}