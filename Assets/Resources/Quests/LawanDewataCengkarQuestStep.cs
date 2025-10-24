using System.Collections.Generic;
using System.Linq;
using Enemy;
using QuestSystem;
using UnityEngine;

public class LawanDewataCengkarQuestStep : QuestStep
{
    private int _enemyKilled = 0;
    private int _killedEnemyToComplete = 1;

    void Start()
    {
        UpdateState();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GameEventsManager.Instance.StatsEvents.OnEnemyDeath += OnEnemyDeath;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameEventsManager.Instance.StatsEvents.OnEnemyDeath -= OnEnemyDeath;
    }

    protected override void OnQuestDelete()
    {
        base.OnQuestDelete();
        GameEventsManager.Instance.QuestEvents.QuestInfoChange(
            "Main Quest: Menuju Ke Keraton",
            $"- Masuk ke ruang takhta"
        );
    }


    void OnEnemyDeath(EnemyStats stats)
    {
        if (_enemyKilled < _killedEnemyToComplete)
        {
            _enemyKilled++;
            UpdateState();
        }
        if (_enemyKilled >= _killedEnemyToComplete)
        {
            GameEventsManager.Instance.QuestEvents.FinishQuest("LawanDewataCengkar");
        }
    }
    private void UpdateState()
    {
        GameEventsManager.Instance.QuestEvents.QuestInfoChange(
                "Main Quest: Menuju Ke Keraton",
                $"- Lawan Prabu Dewata Cengkar"
            );
        string status = "Bunuh";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {

    }
}
