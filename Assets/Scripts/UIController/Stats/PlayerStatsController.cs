using System;
using System.Collections.Generic;
using TMPro;
using TwoDotFiveDimension;
using UnityEngine;

namespace UIController.Stats
{
    public class PlayerStatsController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinText;
        [SerializeField] private TMP_Text _moraleText;
        [SerializeField] private List<StatsItem> _healthItems;
        [SerializeField] private List<StatsItem> _manaItems;

        [SerializeField] private GameObject _healthPrefab;
        [SerializeField] private GameObject _manaPrefab;
        [SerializeField]
        private PlayerStats _playerStats;
        private void Start()
        {
            _playerStats = PlayerStats.Instance;
            UpdateHealthStats();
            UpdateManaStats();
            UpdateCoinText();
            UpdateMoraleText();
        }

        public void OnEnable()
        {
            GameEventsManager.Instance.StatsEvents.OnChangeHealthPlayer += UpdateHealthStats;
            GameEventsManager.Instance.StatsEvents.OnChangeManaPlayer += UpdateManaStats;
            GameEventsManager.Instance.StatsEvents.OnChangePlayerCoin += UpdateCoinText;
            GameEventsManager.Instance.StatsEvents.OnChangePlayermorale += UpdateMoraleText;
        }
        public void OnDisable()
        {
            GameEventsManager.Instance.StatsEvents.OnChangeHealthPlayer -= UpdateHealthStats;
            GameEventsManager.Instance.StatsEvents.OnChangeManaPlayer -= UpdateManaStats;
            GameEventsManager.Instance.StatsEvents.OnChangePlayerCoin -= UpdateCoinText;
            GameEventsManager.Instance.StatsEvents.OnChangePlayermorale -= UpdateMoraleText;
        }

        public void UpdateCoinText()
        {
            if (_playerStats is null)
            {
                return;
            }
            if (_coinText != null)
            {
                Debug.Log("Update coin text");
                _coinText.text = _playerStats.coin.ToString();
            }
            else
            {
                Debug.LogWarning("Coin Text is not assigned in PlayerStatsController.");
            }
        }

        public void UpdateMoraleText()
        {
            if (_moraleText != null)
            {
                _moraleText.text = _playerStats.moralePoint.ToString();
            }
            else
            {
                Debug.LogWarning("Morale Text is not assigned in PlayerStatsController.");
            }
        }

        public void UpdateHealthStats()
        {
            if (_playerStats is null)
            {
                return;
            }
            var currentHealth = _playerStats.currentHealth;
            var maxHealth = _playerStats.maxHealth;
            if (_healthItems.Count <= maxHealth)
            {
                int itemsToAdd = (int)(maxHealth - _healthItems.Count);
                for (int i = 0; i < itemsToAdd; i++)
                {
                    var newHealthItem = Instantiate(_healthPrefab, _healthItems[0].transform.parent);
                    _healthItems.Add(newHealthItem.GetComponent<StatsItem>());
                }
            }
            for (int i = 0; i < _healthItems.Count; i++)
            {
                float heartValue = currentHealth - i;
                Debug.Log(heartValue);
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
        }

        public void UpdateManaStats()
        {
            if (_playerStats is null)
            {
                return;
            }
            var currentMana = _playerStats.currentMana;
            var maxMana = _playerStats.maxMana;
            if (_manaItems.Count <= maxMana)
            {
                int itemsToAdd = (int)(maxMana - _manaItems.Count);
                for (int i = 0; i < itemsToAdd; i++)
                {
                    var newManaItem = Instantiate(_manaPrefab, _manaItems[0].transform.parent);
                    _manaItems.Add(newManaItem.GetComponent<StatsItem>());
                }
            }

            for (int i = 0; i < _manaItems.Count; i++)
            {
                float manaValue = currentMana - i;
                if (manaValue >= 1f)
                {
                    _manaItems[i].SetFill();
                }
                else if (manaValue >= 0.5f)
                {
                    _manaItems[i].SetHalfFill();
                }
                else
                {
                    _manaItems[i].SetEmpty();
                }
            }
        }
    }
}