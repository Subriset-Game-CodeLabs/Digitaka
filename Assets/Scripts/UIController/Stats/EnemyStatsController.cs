using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace UIController.Stats
{
    public class EnemyStatsController:MonoBehaviour
    {
        [SerializeField] private List<StatsItem> _healthItems;
        [SerializeField] private EnemyStats _enemyStats;

        private void Start()
        {
            UpdateStats(_enemyStats);
        }
        public void OnEnable()
        {
            GameEventsManager.Instance.StatsEvents.OnChangeHealthEnemy += UpdateStats;
        }
        public void OnDisable()
        {
            GameEventsManager.Instance.StatsEvents.OnChangeHealthEnemy -= UpdateStats;
        }
        public void UpdateStats(EnemyStats enemyStats)
        {
            if (enemyStats != _enemyStats) return;
            var currentHealth = _enemyStats.currentHealth;
    
            for (int i = 0; i < _healthItems.Count; i++)
            {
                float heartValue = currentHealth - i;
                if (heartValue >= 1f)
                {
                    _healthItems[i].SetFill();
                }
                else if (heartValue >= 0.5f)
                {
                    _healthItems[i].SetHalfFill();
                }
                else
                {
                    _healthItems[i].SetEmpty();
                }
            }
            if(currentHealth == 0) gameObject.SetActive(false);
        }
    }
}