using System.Collections.Generic;
using System.Linq;
using Enemy;
using QuestSystem;
using UnityEngine;

public class MasukKeKerajaanQuestStep : QuestStep
{
    private int _enemyKilled = 0;
    [SerializeField]
    private int _killedEnemyToComplete = 3;
    private List<EnemyStats> _enemyStats = new List<EnemyStats>();

    private void Start()
    {
        _enemyStats = FindObjectsByType<EnemyStats>(FindObjectsSortMode.None).ToList();
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
        if (!_enemyStats.Contains(stats)) return;
        if (_enemyKilled < _killedEnemyToComplete)
        {
            _enemyKilled++;
            UpdateState();
        }
        if (_enemyKilled >= _killedEnemyToComplete)
        {
            FinishQuestStepAndDestroy();
        }
    }
    private void UpdateState()
    {
        string status = $"Killed {_enemyKilled} / {_killedEnemyToComplete} enemies";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {

    }
}
