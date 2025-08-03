using Enemy;
using UnityEngine;

namespace UIController.Stats
{
    public class EnemyStatsController:MonoBehaviour
    {
        [SerializeField] private StatsItem[] _healthItems;
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
            var maxHealth = _enemyStats.maxHealth;

            for (int i = 0; i < _healthItems.Length; i++)
            {
                if (i < maxHealth)
                {
                    _healthItems[i].SetFill(i < currentHealth);
                }
                else
                {
                    _healthItems[i].SetFill(false);
                }
            }
            if(currentHealth == 0) gameObject.SetActive(false);
        }
    }
}