using System;
using TMPro;
using TwoDotFiveDimension;
using UnityEngine;

namespace UIController.Stats
{
    public class PotionController:MonoBehaviour
    {
        [SerializeField] TMP_Text _qtyHealthPotion;
        [SerializeField] TMP_Text _qtyManaPotion;
        
        private PlayerStats _playerStats;

        private void Start()
        {
            _playerStats = PlayerStats.Instance;
            UpdateHealthPotion();
            UpdateManaPotion();
        }

        private void OnEnable()
        {
            GameEventsManager.Instance.StatsEvents.OnChangeHealthPlayer += UpdateHealthPotion;
            GameEventsManager.Instance.StatsEvents.OnChangeManaPlayer += UpdateManaPotion;
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.StatsEvents.OnChangeHealthPlayer -= UpdateHealthPotion;
            GameEventsManager.Instance.StatsEvents.OnChangeManaPlayer -= UpdateManaPotion;
        }
        
        private void UpdateHealthPotion()
        {
            if (_playerStats is null)
            {
                return;
            }
            _qtyHealthPotion.text = "x " +_playerStats.healPotion;
        }
        private void UpdateManaPotion()
        {
            if (_playerStats is null)
            {
                return;
            }
            _qtyManaPotion.text = "x " +_playerStats.manaPotion;

        }
    }
}