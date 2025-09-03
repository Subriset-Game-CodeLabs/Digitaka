using System;
using Audio;
using TwoDotFiveDimension;
using UnityEngine;

namespace Enemy
{
    public class EnemyStats:MonoBehaviour
    {
        [SerializeField] private EnemyData _enemyBaseData;
        private float _currentHealth ;
        private void Awake()
        {
            // _enemyBaseData.ResetStats();
            _currentHealth = _enemyBaseData.maxHealth;
        }

        public float maxHealth
        {
            get => _enemyBaseData.maxHealth;
            private set => _enemyBaseData.maxHealth = value;
        }

        public float currentHealth
        {
            get => _currentHealth;
            private set => _currentHealth = value;
        }

        public float damage
        {
            get => _enemyBaseData.damage;
            private set => _enemyBaseData.damage = value;
        }
        public bool IsAlive => currentHealth > 0;
        public float TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Debug.Log("Enemy has been defeated!");
                PlayerStats.Instance.AddCoin(_enemyBaseData.dropCoin);
                GameEventsManager.Instance.StatsEvents.EnemyDeath(this);
                AudioManager.Instance.PlaySound(SoundType.SFX_Death);
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