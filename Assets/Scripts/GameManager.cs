using System;
using Input;
using QuestSystem;
using TwoDotFiveDimension;
using UIController;
using UnityEngine;

public class GameManager: PersistentSingleton<GameManager>
{
    
    [SerializeField] private GameObject _mainCharacterPrefab;
    public string CurrentMap { get; private set; }
    public bool TutorialCompleted { get; private set; } = false;
    public bool IsGamePaused { get; private set; } = false;
    private UIManager _uiManager;
   

    private void Start()
    {
        StartGame();   
        PlayerStats.Instance.ResetStats();
    }
    public void CompleteTutorial()
    {
        TutorialCompleted = true;
        UIManager.Instance.HideTutorialPanel();
    }
    public void StartGame()
    {
        if(!TutorialCompleted) 
        {
            UIManager.Instance.ShowTutorialPanel();
        }
        else
        {
            UIManager.Instance.HideTutorialPanel();
        }
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
