using System;
using Input;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : PersistentSingleton<MapManager>
{
    private Canvas _canvas;
    private Transform _mapTiles;

    [SerializeField]
    private GameObject _mapPrefab;
    private GameObject _mapPanel;
    private bool isActive = false;

    protected override void Awake()
    {
        base.Awake();
        _canvas = FindFirstObjectByType<Canvas>();
    }

    void OnEnable()
    {
        InputManager.Instance.PlayerInput.OpenMap.OnDown += MapPressed;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        InputManager.Instance.PlayerInput.OpenMap.OnDown -= MapPressed;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Changescene(String scenename)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scenename);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _canvas = FindFirstObjectByType<Canvas>();
    }

    public void MapPressed()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu")
            return;

        if (_mapPanel == null)
        {
            _mapPanel = Instantiate(_mapPrefab, _canvas.transform);
            _mapTiles = _mapPanel.transform.Find("MapBackground/MapTiles");
            Button button = _mapPanel.transform.Find("MapBackground/CloseBtn").GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                _mapPanel.gameObject.SetActive(false);
                isActive = false;
            });
        }

        if (isActive)
        {
            _mapPanel.gameObject.SetActive(false);
            isActive = false;
        }
        else
        {
            _mapPanel.gameObject.SetActive(true);
            isActive = true;
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
}
