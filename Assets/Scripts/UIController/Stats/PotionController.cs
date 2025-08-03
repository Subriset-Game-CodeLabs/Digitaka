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
            _qtyHealthPotion.text = "x " +_playerStats.healPotion;
        }
        private void UpdateManaPotion()
        {
            _qtyManaPotion.text = "x " +_playerStats.manaPotion;

        }
    }
}