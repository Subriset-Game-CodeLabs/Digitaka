using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Input;
using TwoDotFiveDimension;

public class TeleportManager : PersistentSingleton<TeleportManager>
{
    [Header("Scene Management")]
    [SerializeField] private GameObject _cinemachineCameraPrefab;
    [SerializeField] private string _firstSceneName = "A1New";
    [SerializeField] private string _currentSceneName = "A1New";

    [Header("Slide Animation")]
    [SerializeField] private GameObject _slideTransitionPrefab; // Prefab dengan UI Canvas dan Image
    [SerializeField] private float _slideAnimationDuration = 0.5f;
    [SerializeField] private Ease _slideEaseType = Ease.InOutQuad;

    private TeleportPoint[] _teleportPoints;
    private GameManager _gameManager;
    private CinemachineCamera _cinemachineCamera;
    private GameObject _slideTransition;
    private RectTransform _slidePanel;
    private bool _isTransitioning = false;
    private String _previousScene;

    public void InitializeTeleport(string startingScene)
    {
        _currentSceneName = startingScene;
        CreateSlideTransition();
        enabled = true;
    }

    private void CreateSlideTransition()
    {
        if (_slideTransitionPrefab != null)
        {
            _slideTransition = Instantiate(_slideTransitionPrefab);
            _slidePanel = _slideTransition.transform.GetChild(0).GetComponent<RectTransform>();

            DontDestroyOnLoad(_slideTransition);
            Debug.Log(_slidePanel.name);
            Debug.Log(_slidePanel.anchoredPosition);
            _slidePanel.anchoredPosition = new Vector2(-Screen.width, 0);

            Canvas canvas = _slideTransition.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.sortingOrder = 1000;
            }
        }
    }
    private void OnEnable()
    {
        _gameManager = GameManager.Instance;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ResetStats kalau respawn 
        if (_previousScene == UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)
        {
            PlayerStats.Instance.ResetStats(false);
        }
        
        _teleportPoints = FindObjectsByType<TeleportPoint>(FindObjectsSortMode.None);
        if (_isTransitioning)
        {
            StartCoroutine(SlideOutAfterLoad());
        }
        else
        {
            InitializePlayer();
        }

        if (scene.name == "A1New" || scene.name == "B1" || scene.name == "C1")
        {
            PlayerStats.Instance.ResetStats(false);
        }
    }

    public void InitializePlayer()
    {
        foreach (var teleportPoint in _teleportPoints)
        {
            Debug.Log("Checking teleport point: " + teleportPoint.TargetScene + " for scene: " + _currentSceneName);
            if (teleportPoint.TargetScene == _currentSceneName)
            {
                // teleportPoint.StarterPoint = true;
                _currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
                SpawnCharacterAtTeleportPoint(teleportPoint);
                Debug.Log($"Player spawned at {teleportPoint.name}");
                InputManager.Instance.PlayerMode();
                break;
            }
        }
    }
    private IEnumerator SlideOutAfterLoad()
    {
        yield return new WaitForSeconds(0.1f);

        InitializePlayer();

        if (_slidePanel != null)
        {
            _slidePanel.DOAnchorPosX(Screen.width, _slideAnimationDuration)
                .SetEase(_slideEaseType)
                .OnComplete(() =>
                {
                    // Reset posisi untuk transisi berikutnya
                    _slidePanel.anchoredPosition = new Vector2(-Screen.width, 0);
                    _isTransitioning = false;
                });
        }
    }
    private void SpawnCharacterAtTeleportPoint(TeleportPoint teleportPoint)
    {
        if (teleportPoint == null)
        {
            Debug.LogError("Teleport point is null.");
            return;
        }

        var character = _gameManager.SpawnMainCharacter(teleportPoint.transform);
        SetTargetCinemachine(character);
    }

    private void SetTargetCinemachine(GameObject target)
    {
        if (target == null)
        {
            Debug.LogError("Target is null for Cinemachine.");
            return;
        }
        Debug.Log($"Setting Cinemachine target to {_cinemachineCamera}");
        if (_cinemachineCamera == null)
        {
            _cinemachineCamera = Instantiate(_cinemachineCameraPrefab).GetComponent<CinemachineCamera>();
            _cinemachineCamera.Follow = target.transform;
            Debug.Log(_cinemachineCamera.Follow);
        }
    }

    public void TeleportToScene(string sceneName, string pointName)
    {
        if (_isTransitioning) return;
        _previousScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        StartCoroutine(TeleportWithSlideAnimation(sceneName, pointName));

    }
    private IEnumerator TeleportWithSlideAnimation(string sceneName, string pointName)
    {
        _isTransitioning = true;

        // Slide in animation (dari kiri ke tengah)
        if (_slidePanel != null)
        {
            _slidePanel.DOAnchorPosX(0, _slideAnimationDuration).SetEase(_slideEaseType);
            yield return new WaitForSeconds(_slideAnimationDuration);
        }

        _cinemachineCamera = null;
        Debug.Log($"Teleporting to scene: {sceneName} at point: {pointName}");
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    public void ResetCheckpoint()
    {
        _currentSceneName = _firstSceneName;
        enabled = false;
    }
}
