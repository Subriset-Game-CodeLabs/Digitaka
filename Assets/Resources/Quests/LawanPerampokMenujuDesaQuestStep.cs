using System.Collections.Generic;
using System.Linq;
using Enemy;
using QuestSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LawanPerampokMenujuDesaQuestStep : QuestStep
{
    private int _enemyKilled = 0;
    [SerializeField]
    private int _killedEnemyToComplete = 2;
    private List<EnemyStats> _enemyStats = new List<EnemyStats>();

    void OnEnable()
    {
        GameEventsManager.Instance.StatsEvents.OnEnemyDeath += OnEnemyDeath;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        GameEventsManager.Instance.StatsEvents.OnEnemyDeath -= OnEnemyDeath;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "B6")
        {
            _enemyStats = FindObjectsByType<EnemyStats>(FindObjectsSortMode.None).ToList();
            UpdateState();
        }
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
