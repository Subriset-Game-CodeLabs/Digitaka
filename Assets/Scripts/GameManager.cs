using System;
using Input;
using QuestSystem;

public class GameManager: PersistentSingleton<GameManager>
{
    private void Start()
    {
        QuestManager.Instance.InitializeQuest();
    }

    public void StartGame()
    {
        InputManager.Instance.PlayerMode();
    }
}
