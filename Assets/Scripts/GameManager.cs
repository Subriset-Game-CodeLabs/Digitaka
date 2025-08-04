using System;
using Input;
using QuestSystem;
using UIController;

public class GameManager: PersistentSingleton<GameManager>
{

    public void StartGame()
    {
        InputManager.Instance.PlayerMode();
        UIManager.Instance.SetPause(false);
    }
}
