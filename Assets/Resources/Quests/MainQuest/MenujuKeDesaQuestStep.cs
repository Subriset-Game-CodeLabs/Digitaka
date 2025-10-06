using QuestSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenujuKeDesaQuestStep : QuestStep
{
    void Start()
    {
        GameEventsManager.Instance.QuestEvents.QuestInfoChange(
                "Main Quest: Menuju ke desa",
                $"- Lanjutkan perjalanan "
            );    
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        base.OnSceneLoaded(scene, mode);
        if (scene.name == "B2")
        {
            FinishQuestStepAndDestroy();
        }
    }

    protected override void SetQuestStepState(string state)
    {

    }
}
