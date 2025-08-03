using System;
using Input;
using QuestSystem;

public class GameManager: PersistentSingleton<GameManager>
{

    public void StartGame()
    {
        InputManager.Instance.PlayerMode();
    }
}
