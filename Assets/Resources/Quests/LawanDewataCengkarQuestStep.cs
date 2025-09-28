using System.Collections.Generic;
using System.Linq;
using Enemy;
using QuestSystem;
using UnityEngine;

public class LawanDewataCengkarQuestStep : QuestStep
{
    private int _enemyKilled = 0;
    private int _killedEnemyToComplete = 1;

    void OnEnable()
    {
        GameEventsManager.Instance.StatsEvents.OnEnemyDeath += OnEnemyDeath;

    }

    void OnDisable()
    {
        GameEventsManager.Instance.StatsEvents.OnEnemyDeath -= OnEnemyDeath;
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
        string status = "Bunuh Prabu Dewata Cengkar";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {

    }
}
