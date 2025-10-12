using System;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using QuestSystem;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillEnemyQuestStep : QuestStep
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
        GameEventsManager.Instance.QuestEvents.OnFinishQuest += OnFinishQuest;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameEventsManager.Instance.StatsEvents.OnEnemyDeath -= OnEnemyDeath;
        GameEventsManager.Instance.QuestEvents.OnFinishQuest -= OnFinishQuest;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        base.OnSceneLoaded(scene, mode);
        if (scene.name == "B1")
        {
            Destroy(gameObject);
            GameEventsManager.Instance.QuestEvents.QuestInfoChange("R", $"");
        }
    }

    void OnFinishQuest(string questId)
    {
        if (questId == "KillEnemy")
        {
            GameEventsManager.Instance.QuestEvents.QuestInfoChange("R", $"");
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
            // GameEventsManager.Instance.QuestEvents.QuestInfoChange(
            //     "Side Quest: Bantu Jaka",
            //     $"Bicara ke jaka"
            // );
            // FinishQuestStep();
            GameEventsManager.Instance.QuestEvents.FinishQuest("KillEnemy");
            Destroy(gameObject);
        }

    }
    private void UpdateState()
    {
        GameEventsManager.Instance.QuestEvents.QuestInfoChange(
        "Side Quest: Bantu Jaka",
        $"Lawan musuh di depan {_enemyKilled} / 3"
    );
        // string state = _enemyKilled.ToString();
        string status = $"Killed {_enemyKilled} / {_killedEnemyToComplete} enemies";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
        // this._enemyKilled = System.Int32.Parse(state);
        // UpdateState();
    }
}
