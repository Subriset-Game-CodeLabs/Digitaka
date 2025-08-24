using System;
using Enemy;

namespace Events
{
    public class StatsEvents
    {
        public event Action OnChangeHealthPlayer;
        public void ChangeHealthPlayer( )
        {
            OnChangeHealthPlayer?.Invoke();
        }
        
        public event Action<EnemyStats> OnChangeHealthEnemy;
        public void ChangeHealthEnemy(EnemyStats stats)
        {
            OnChangeHealthEnemy?.Invoke(stats);
        }
        public event Action<EnemyStats> OnEnemyDeath;
        public void EnemyDeath(EnemyStats stats)
        {
            OnEnemyDeath?.Invoke(stats);
        }
        public event Action OnChangeManaPlayer;
        public void ChangeManaPlayer()
        {
            OnChangeManaPlayer?.Invoke();
        }
        public event Action OnPlayerDeath;
        public void PlayerDeath()
        {
            OnPlayerDeath?.Invoke();
        }

        public event Action OnChangePlayerCoin;
        public void ChangePlayerCoin()
        {
            OnChangePlayerCoin?.Invoke();
        }
    }
}