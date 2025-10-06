using System.Collections.Generic;
using System.Linq;
using Enemy;
using QuestSystem;
using UnityEngine;

public class MasukKeKerajaanQuestStep1 : QuestStep
{
    private int _enemyKilled = 0;
    [SerializeField]
    private int _killedEnemyToComplete = 1;
    private List<EnemyStats> _enemyStats = new List<EnemyStats>();

    void Start()
    {
        UpdateState();
    }

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
            GameEventsManager.Instance.QuestEvents.FinishQuest("MasukKeKerajaan");
            GameEventsManager.Instance.QuestEvents.QuestInfoChange(
                "Main Quest: Menuju Ke Keraton",
                $"- Masuk ke ruang takhta"
            );
        }
    }
    private void UpdateState()
    {
        GameEventsManager.Instance.QuestEvents.QuestInfoChange(
                "Main Quest: Menuju Ke Keraton",
                $"- Lawan Patih Jugul Muda"
            );
        string status = "Bunuh Patih";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {

    }
}
