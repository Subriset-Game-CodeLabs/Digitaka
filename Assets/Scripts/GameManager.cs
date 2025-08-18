using System;
using Input;
using QuestSystem;
using UIController;
using UnityEngine;

public class GameManager: PersistentSingleton<GameManager>
{
    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        InputManager.Instance.PlayerMode();
        SceneManager.Instance.LoadSceneAdditive("MainCanvas");
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                  Application.Quit();
        #endif
    }
}
