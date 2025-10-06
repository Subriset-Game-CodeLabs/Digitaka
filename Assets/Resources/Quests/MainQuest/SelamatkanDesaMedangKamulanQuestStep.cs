using QuestSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelamatkanDesaMedangKamulanQuestStep : QuestStep
{
    void Start()
    {
        GameEventsManager.Instance.QuestEvents.QuestInfoChange(
                "Main Quest: Selamatkan Desa Medang Kamulan",
                $"- Masuk ke desa "
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
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "C2")
        {
            FinishQuestStepAndDestroy();
        }
    }

    protected override void SetQuestStepState(string state)
    {

    }
}
