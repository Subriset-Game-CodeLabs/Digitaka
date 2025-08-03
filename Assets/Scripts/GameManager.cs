using Input;

namespace DefaultNamespace
{
    public class GameManager: PersistentSingleton<GameManager>
    {
        public void StartGame()
        {
            InputManager.Instance.PlayerMode();
        }
    }
}