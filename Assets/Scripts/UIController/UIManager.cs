using System;
using Audio;
using Input;
using TMPro;
using TwoDotFiveDimension;
using UIController.Stats;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private GameObject _tutorialPanel;
        [SerializeField] private GameObject _defeatedPanel;
        [SerializeField] private GameObject _completeQuestPanel;
        [SerializeField] private GameObject _mapPanel;
        [SerializeField] private GameObject _mobileUIPanel;
        [SerializeField] private GameObject _statsPanel;
        [SerializeField] private GameObject _coinPanel;
        [SerializeField] private bool isPaused = false;
        private PlayerStats _playerStats;
        private bool _mapPanelActive;
        private Transform _mapTiles;
        private bool _canvasActive = true;
        
        [Header("Mobile UI")]
        [SerializeField] private CooldownUI _dashCooldownUIMobile;
        [SerializeField] private CooldownUI _ultimateCooldownUIMobile;
        [SerializeField] private CooldownUI _healthPotionCooldownUIMobile;
        [SerializeField] private CooldownUI _manaPotionCooldownUIMobile;
        public void SetPause(bool value)
        {
            isPaused = value;
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        private void Start()
        {
            _playerStats = PlayerStats.Instance;
            //InitializeTutorial();
        }

        public void HideCanvas()
        {
            if (_canvasActive)
            {
                _mobileUIPanel.SetActive(false);
                _statsPanel.SetActive(false);
                _coinPanel.SetActive(false);
                _canvasActive = false;
            }
            else
            {
                _mobileUIPanel.SetActive(true);
                _statsPanel.SetActive(true);
                _coinPanel.SetActive(true);
                _canvasActive = true;
            }
        }

        private void InitializeTutorial()
        {
            if (!GameManager.Instance.TutorialCompleted)
            {
                ShowTutorialPanel();
            }
            else
            {
                HideTutorialPanel();
            }
        }
        void OnEnable()
        {
            InputManager.Instance.PlayerInput.Pause.OnDown += Pause;
            GameEventsManager.Instance.PlayerActionsEvents.OnHealthPotionUsed += StartCooldownHealthPotion;
            GameEventsManager.Instance.PlayerActionsEvents.OnManaPotionUsed += StartCooldownManaPotion;
            GameEventsManager.Instance.PlayerActionsEvents.OnDashPerformed += StartCooldownDash;
            GameEventsManager.Instance.PlayerActionsEvents.OnUltimatePerformed += StartCooldownUltimate;
            GameEventsManager.Instance.StatsEvents.OnPlayerDeath += ShowDefeatedPanel;
            InputManager.Instance.PlayerInput.OpenMap.OnDown += ShowMap;

        }
        void OnDisable()
        {
            InputManager.Instance.PlayerInput.Pause.OnDown -= Pause;
            GameEventsManager.Instance.PlayerActionsEvents.OnHealthPotionUsed -= StartCooldownHealthPotion;
            GameEventsManager.Instance.PlayerActionsEvents.OnManaPotionUsed -= StartCooldownManaPotion;
            GameEventsManager.Instance.PlayerActionsEvents.OnDashPerformed -= StartCooldownDash;
            GameEventsManager.Instance.PlayerActionsEvents.OnUltimatePerformed -= StartCooldownUltimate;
            GameEventsManager.Instance.StatsEvents.OnPlayerDeath -= ShowDefeatedPanel;
            InputManager.Instance.PlayerInput.OpenMap.OnDown -= ShowMap;

        }

        public void ShowMap()
        {
            InputManager.Instance.UIMode();
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu")
                return;
            
            _mapTiles = _mapPanel.transform.Find("MapBackground/MapTiles");
            Button button = _mapPanel.transform.Find("MapBackground/CloseBtn").GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                _mapPanel.gameObject.SetActive(false);
                _mapPanelActive = false;
                InputManager.Instance.PlayerMode();
            });

            if (_mapPanelActive)
            {
                _mapPanel.gameObject.SetActive(false);
                _mapPanelActive = false;
            }
            else
            {
                _mapPanel.gameObject.SetActive(true);
                _mapPanelActive = true;
                foreach (Transform item in _mapTiles)
                {
                    MapTile mapTile = item.gameObject.GetComponent<MapTile>();
                    // Debug.Log("checking map name : "  + mapTile.MapName);
                    // Debug.Log("Current Scene : " + SceneManager.GetActiveScene().name);
                    if (mapTile.MapName == UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
                    {
                        mapTile.ActivePointer();
                    }
                    else
                    {
                        mapTile.DeactivePointer();
                    }
                }
            }
        }
        public void StartCooldownUltimate()
        {
            if (_ultimateCooldownUI != null)
            {
                _ultimateCooldownUI.StartCooldown(_playerStats.ultimateCooldown);
                _ultimateCooldownUIMobile.StartCooldown(_playerStats.ultimateCooldown);
            }
        }
        public void StartCooldownHealthPotion()
        {
            if (_healthPotionCooldownUI != null)
            {
                _healthPotionCooldownUI.StartCooldown(_playerStats.healthPotionCooldown);
                _healthPotionCooldownUIMobile.StartCooldown(_playerStats.healthPotionCooldown);}
        }
        public void StartCooldownManaPotion()
        {
            if (_manaPotionCooldownUI != null)
            {
                _manaPotionCooldownUI.StartCooldown(_playerStats.manaPotionCooldown);
                _manaPotionCooldownUIMobile.StartCooldown(_playerStats.manaPotionCooldown);
            }
        }
        public void StartCooldownDash()
        {
            if (_dashCooldownUI != null)
            {
                _dashCooldownUI.StartCooldown(_playerStats.dashCooldown);
                _dashCooldownUIMobile.StartCooldown(_playerStats.dashCooldown);
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
            PlayerStats.Instance.ResetStats();
            _defeatedPanel.SetActive(false);
            TeleportManager.Instance.ResetCheckpoint();
        }
    }
}