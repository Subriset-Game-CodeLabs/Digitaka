using QuestSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MencariTempatIstirahatQuestStep : QuestStep
{

    void Start()
    {
        GameEventsManager.Instance.QuestEvents.QuestInfoChange(
                "Main Quest: Cari Tempat Istirahat",
                $"Lanjutkan Perjalanan "
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
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "A7New")
        {
            FinishQuestStepAndDestroy();
        }
    }

    protected override void SetQuestStepState(string state)
    {
        
    }
}
