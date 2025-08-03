using System;
using TwoDotFiveDimension;
using UnityEngine;

namespace UIController.Stats
{
    public class PlayerStatsController:MonoBehaviour
    {
        [SerializeField] private StatsItem[] _healthItems;
        [SerializeField] private StatsItem[] _manaItems;
        private PlayerStats _playerStats;
        private void Start()
        {
            _playerStats = PlayerStats.Instance;
            UpdateStats();
        }

        public void OnEnable()
        {
            GameEventsManager.Instance.StatsEvents.OnChangeHealthPlayer += UpdateStats;
        }
        public void OnDisable()
        {
            GameEventsManager.Instance.StatsEvents.OnChangeHealthPlayer -= UpdateStats;
        }

        public void UpdateStats()
        {
            var currentHealth = _playerStats.currentHealth;
            var maxHealth = _playerStats.maxHealth;
            var currentMana = _playerStats.currentMana;
            var maxMana = _playerStats.maxMana;
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
            for (int i = 0; i < _manaItems.Length; i++)
            {
                if (i < maxMana)
                {
                    _manaItems[i].SetFill(i < currentMana);
                }
                else
                {
                    _manaItems[i].SetFill(false);
                }
            }
        }
    }
}