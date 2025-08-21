using QuestSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissingBrotherQuestStep : QuestStep
{
    [SerializeField]
    private string _sceneName;

    private void Start()
    {
        string status = "Find the missing brother";
        ChangeState("", status);
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        if (currentScene.name == _sceneName)
            Enable();
        else
            Disable();
    }

    void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == _sceneName)
            Enable();
        else
            Disable();
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            string status = "You found the missing brother!";
            ChangeState("", status);
            FinishQuestStep();
        }
    }

    private void Disable()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        GetComponent<SphereCollider>().enabled = false;
    }

    private void Enable()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        GetComponent<SphereCollider>().enabled = true;
    }

    protected override void SetQuestStepState(string state)
    {
    }

}
