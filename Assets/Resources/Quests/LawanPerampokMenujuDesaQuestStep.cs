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

    void Start()
    {
        UpdateState();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GameEventsManager.Instance.StatsEvents.OnEnemyDeath += OnEnemyDeath;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        GameEventsManager.Instance.QuestEvents.OnFinishQuest += OnFinishQuest;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameEventsManager.Instance.StatsEvents.OnEnemyDeath -= OnEnemyDeath;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        GameEventsManager.Instance.QuestEvents.OnFinishQuest -= OnFinishQuest;
    }

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        base.OnSceneLoaded(scene, mode);
        if (scene.name == "B6")
        {
            _enemyStats = FindObjectsByType<EnemyStats>(FindObjectsSortMode.None).ToList();
            UpdateState();
        }
    }

    void OnFinishQuest(string id)
    {
        if (id == "LawanPerampokMenujuDesa")
        {
            Destroy(gameObject);
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
            GameEventsManager.Instance.QuestEvents.QuestInfoChange(
                "Main Quest: Menuju ke desa",
                $"- Kembali ke wanita tua"
            );
        }
    }

    private void UpdateState()
    {
        GameEventsManager.Instance.QuestEvents.QuestInfoChange(
                "Main Quest: Menuju ke desa",
                $"- Lawan perampok yang menghadang desa {_enemyKilled} / {_killedEnemyToComplete}"
            );

        string status = $"Killed {_enemyKilled} / {_killedEnemyToComplete} enemies";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {

    }
}
