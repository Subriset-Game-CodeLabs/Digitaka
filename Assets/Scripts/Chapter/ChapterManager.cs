using UnityEngine;

public class ChapterManager: MonoBehaviour
{
    [SerializeField] private ChapterController[] _chapterControllers;
    
    private void Start()
    {
        Initialize();
    }
    public void BackToMainMenu()
    {
        SceneManager.Instance.ChangeScene("MainMenu");
    }
    private void Initialize()
    {
        var currentChapter = GameManager.Instance.GetCurrentChapter;
        Debug.Log(currentChapter);
        for (int i = 0; i < _chapterControllers.Length; i++)
        {
            if (i <= currentChapter - 1)
            {
                _chapterControllers[i].SetLockChapter(false);
            }
            else
            {
                _chapterControllers[i].SetLockChapter(true);
            }
        }
    }
    
}
