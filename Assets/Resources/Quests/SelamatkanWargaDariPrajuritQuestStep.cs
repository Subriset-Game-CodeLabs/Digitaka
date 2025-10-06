using System.Collections.Generic;
using System.Linq;
using Enemy;
using QuestSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelamatkanWargaDariPrajuritQuestStep : QuestStep
{
    private int _enemyKilled = 0;
    [SerializeField]
    private int _killedEnemyToComplete = 2;
    private List<EnemyStats> _enemyStats = new List<EnemyStats>();
    
    private void Start()
    {
        _enemyStats = FindObjectsByType<EnemyStats>(FindObjectsSortMode.None).ToList();
        UpdateState();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        GameEventsManager.Instance.StatsEvents.OnEnemyDeath += OnEnemyDeath;
        // GameEventsManager.Instance.QuestEvents.OnFinishQuest += OnFinishQuest;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameEventsManager.Instance.StatsEvents.OnEnemyDeath -= OnEnemyDeath;
        // GameEventsManager.Instance.QuestEvents.OnFinishQuest -= OnFinishQuest;
    }


    void OnEnemyDeath(EnemyStats stats)
    {
        if (!_enemyStats.Contains(stats)) return;
        if (_enemyKilled < _killedEnemyToComplete)
        {
            _enemyKilled++;
            UpdateState();
        }
        if(_enemyKilled >= _killedEnemyToComplete)
        {
            GameEventsManager.Instance.QuestEvents.QuestInfoChange(
                "Main Quest: Menuju Ke Keraton",
                $"- Bicara ke Nyai Sengkeran Untuk Melanjutkan Perjalanan"
            );
            FinishQuestStepAndDestroy();
        }
    }
    private void UpdateState()
    {
        GameEventsManager.Instance.QuestEvents.QuestInfoChange(
                "Main Quest: Selamatkan Warga Desa",
                $"- Lawan prajurit {_enemyKilled} / {_killedEnemyToComplete}"
            );
        string status = $"Killed {_enemyKilled} / {_killedEnemyToComplete} enemies";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {

    }
}
