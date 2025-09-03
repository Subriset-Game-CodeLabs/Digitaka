using System;
using Unity.Cinemachine; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportManager : PersistentSingleton<TeleportManager>
{
    [SerializeField] private GameObject _cinemachineCameraPrefab;
    private TeleportPoint[] _teleportPoints;
    private GameManager _gameManager;
    [SerializeField] private string _firstSceneName = "A1New";
    [SerializeField] private string _currentSceneName = "A1New";
    private CinemachineCamera _cinemachineCamera;

    private void Start()
    {
        _currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
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
        _teleportPoints = FindObjectsByType<TeleportPoint>(FindObjectsSortMode.None);
        InitializePlayer();
    }

    public void InitializePlayer()
    {
        foreach (var teleportPoint in _teleportPoints)
        {
            Debug.Log("Checking teleport point: " + teleportPoint.TargetScene + " for scene: " + _currentSceneName);
            if (teleportPoint.TargetScene == _currentSceneName)
            {
                teleportPoint.StarterPoint = true;
                _currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
                SpawnCharacterAtTeleportPoint(teleportPoint);
                Debug.Log($"Player spawned at {teleportPoint.name}");
                break;
            }
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
        _cinemachineCamera = null;
        Debug.Log($"Teleporting to scene: {sceneName} at point: {pointName}");
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName); 
        
    }
    public void ResetCheckpoint()
    {
        _currentSceneName = _firstSceneName;
    }
}
