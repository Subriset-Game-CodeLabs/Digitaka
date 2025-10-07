using System;
using Input;
using UnityEngine;
using UnityEngine.UI;

public class ChapterController:MonoBehaviour
{
    [SerializeField] private int _chapterIndex;
    [SerializeField] private GameObject _lockChapter;
    [SerializeField] private string _chapterScene;
    private Button _buttonChapter;

    private void Awake()
    {
        _buttonChapter = GetComponent<Button>();
        _buttonChapter.onClick.AddListener(() =>
        {
            SceneManager.Instance.ChangeScene(_chapterScene);
            GameManager.Instance.SetChapterScene(_chapterScene);
            GameManager.Instance.SetCurrentChapter(_chapterIndex);
            GameManager.Instance.StartGame();
        });
    }

    public void SetLockChapter(bool isLock)
    {
        _lockChapter.SetActive(isLock);
        _buttonChapter.interactable = !isLock;
    }
}
