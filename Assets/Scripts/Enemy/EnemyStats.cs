using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyStats:MonoBehaviour
    {
        [SerializeField] private EnemyData _enemyData;

        private void Awake()
        {
            _enemyData.ResetStats();
        }

        public int maxHealth
        {
            get => _enemyData.maxHealth;
            private set => _enemyData.maxHealth = value;
        }

        public int currentHealth
        {
            get => _enemyData.health;
            private set => _enemyData.health = value;
        }

        public int damage
        {
            get => _enemyData.damage;
            private set => _enemyData.damage = value;
        }
        public bool IsAlive => currentHealth > 0;
        public int TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth < 0)
            {
                currentHealth = 0;
                Debug.Log("Enemy has been defeated!");
            }
            GameEventsManager.Instance.StatsEvents.ChangeHealthEnemy(this);
            return currentHealth;
        }

        public void Heal(int amount)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
        
    }
}