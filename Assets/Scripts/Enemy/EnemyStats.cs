using System;
using Audio;
using TwoDotFiveDimension;
using UnityEngine;

namespace Enemy
{
    public class EnemyStats:MonoBehaviour
    {
        [SerializeField] private EnemyData _enemyBaseData;
        private int _currentHealth ;
        private void Awake()
        {
            _enemyBaseData.ResetStats();
            _currentHealth = _enemyBaseData.maxHealth;
        }

        public int maxHealth
        {
            get => _enemyBaseData.maxHealth;
            private set => _enemyBaseData.maxHealth = value;
        }

        public int currentHealth
        {
            get => _currentHealth;
            private set => _currentHealth = value;
        }

        public int damage
        {
            get => _enemyBaseData.damage;
            private set => _enemyBaseData.damage = value;
        }
        public bool IsAlive => currentHealth > 0;
        public int TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log("Enemy has been defeated!");
                PlayerStats.Instance.AddCoin(_enemyBaseData.dropCoin);
                // AudioManager.Instance.PlaySound();
                
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