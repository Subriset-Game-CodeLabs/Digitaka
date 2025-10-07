using System;
using Input;
using QuestSystem;
using TwoDotFiveDimension;
using UIController;
using UnityEngine;

public class GameManager: PersistentSingleton<GameManager>
{
    
    [SerializeField] private GameObject _mainCharacterPrefab;
    [SerializeField] private GameObject _uiHUDPrefab;
    public string CurrentMap { get; private set; }
    public bool TutorialCompleted { get; private set; } = false;
    public bool IsGamePaused { get; private set; } = false;
    [SerializeField] private int _currentChapter= 1;
    [SerializeField] private int _unlockChapter= 3;
    [SerializeField] private string _chapterScene;
    
    public int GetCurrentChapter => _currentChapter;
    public int GetUnlockChapter => _unlockChapter;
    public string GetChapterScene => _chapterScene;
    public void CompleteChapter()
    {
        _unlockChapter++;
    }
    
    public void SetChapterScene(string sceneName)
    {
        _chapterScene = sceneName;
    }
    public void SetCurrentChapter(int chapter)
    {
        _currentChapter = chapter;
    }

    public void CompleteTutorial()
    {
        TutorialCompleted = true;
        UIManager.Instance.HideTutorialPanel();
    }
    public void StartGame()
    {
        Debug.Log("Game Start");
        TeleportManager.Instance.InitializeTeleport(_chapterScene);
        var HUD = Instantiate(_uiHUDPrefab);
        UIManager uiManager = HUD.GetComponent<UIManager>();
        PlayerStats.Instance.ResetStats(true);
        ShopManager.Instance.ResetItem();
        InputManager.Instance.PlayerMode();
        CutsceneManager.Instance.ResetCutscene();
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                  Application.Quit();
        #endif
    }

    public GameObject SpawnMainCharacter(Transform position)
    {
        if (_mainCharacterPrefab == null)
        {
            Debug.LogError("Main Character Prefab is not assigned in GameManager.");
            return null;
        }
        GameObject mainCharacter = Instantiate(_mainCharacterPrefab, position.position, position.rotation);
        return mainCharacter;
    }

}
