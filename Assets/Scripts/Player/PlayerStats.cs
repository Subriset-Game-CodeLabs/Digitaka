using System;
using Audio;
using UnityEngine;
using UnityEngine.Serialization;

namespace TwoDotFiveDimension
{
    public class PlayerStats:MonoBehaviour
    {
        public static PlayerStats Instance { get; private set; }
        [SerializeField] private PlayerData _playerData;
        private Animator _playerAnimator;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public int maxHealth
        {
            get => _playerData.maxHealth;
            private set => _playerData.maxHealth = value;
        }
        public int currentHealth
        {
            get => _playerData.health;
            private set => _playerData.health = value;
        }
        public int maxMana
        {
            get => _playerData.maxMana;
            private set => _playerData.maxMana = value;
        }

        public int currentMana
        {
            get => _playerData.mana;
            private set => _playerData.mana = value;
        }
        
        public int healPotion
        {
            get => _playerData.healPotion;
            private set => _playerData.healPotion = value;
        }
        public int manaPotion
        {
            get => _playerData.manaPotion;
            private set => _playerData.manaPotion = value;
        }
        public int coin
        {
            get => _playerData.coin;
            private set => _playerData.coin = value;
        }
        public int ultimateDamage
        {
            get => _playerData.UltimateDamage;
            private set => _playerData.UltimateDamage = value;
        }
        public int ultimateCost
        {
            get => _playerData.UltimateManaCost;
            private set => _playerData.UltimateManaCost = value;
        }
        public float dashDuration
        {
            get => _playerData.DashDuration;
            private set => _playerData.DashDuration = value;
        }
        public int dashSpeed
        {
            get => _playerData.DashSpeed;
            private set => _playerData.DashSpeed = value;
        }
        public float dashCooldown
        {
            get => _playerData.DashCooldown;
            private set => _playerData.DashCooldown = value;
        }
        
        public float ultimateCooldown
        {
            get => _playerData.UltimateCooldown;
            private set => _playerData.UltimateCooldown = value;
        }
        public float healthPotionCooldown
        {
            get => _playerData.HealthPotionCooldown;
            private set => _playerData.HealthPotionCooldown = value;
        }
        public float manaPotionCooldown
        {
            get => _playerData.ManaPotionCooldown;
            private set => _playerData.ManaPotionCooldown = value;
        }
        
        public int moralePoint
        {
            get => _playerData.moralePoint;
            private set => _playerData.moralePoint = value;
        }
        
        public bool IsAlive => currentHealth > 0;
        public bool CanUltimate => currentMana >= ultimateCost;
        public int damage
        {
            get => _playerData.damage;
            private set => _playerData.damage = value;
        }
        
        public int TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (!IsAlive)
            {
                Debug.Log("Player has died");
                currentHealth = 0;
                GameEventsManager.Instance.StatsEvents.PlayerDeath();
                AudioManager.Instance.PlaySound(SoundType.SFX_Death);
                // _playerAnimator.SetTrigger("Die");
            }
            GameEventsManager.Instance.StatsEvents.ChangeHealthPlayer();
            return currentHealth;
        }
        public void Heal(int amount)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            GameEventsManager.Instance.StatsEvents.ChangeHealthPlayer();
        }
        public void AddMana(int amount)
        {
            currentMana += amount;
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
            GameEventsManager.Instance.StatsEvents.ChangeManaPlayer();
        }
        public void UseMana(int amount)
        {
            currentMana -= amount;
            if (currentMana < 0)
            {
                currentMana = 0;
            }
            GameEventsManager.Instance.StatsEvents.ChangeManaPlayer();
        }
        
        public void UseManaPotion(int amount)
        {
            if(manaPotion <= 0) return;
            manaPotion -= amount;
            AddMana(amount);
        }
        public  void UseHealthPotion(int amount)
        {
            if(healPotion <= 0) return;
            healPotion -= amount;
            Heal(amount);
        }

        public void AddManaPotion(int amount)
        {
            manaPotion += amount;
            GameEventsManager.Instance.StatsEvents.ChangeManaPlayer();
        }

        public void AddHealthPotion(int amount)
        {
            healPotion += amount;
            GameEventsManager.Instance.StatsEvents.ChangeHealthPlayer();
        }
        public void AddCoin(int amount)
        {
            coin += amount;
            AudioManager.Instance.PlaySound(SoundType.SFX_Reward);
            GameEventsManager.Instance.StatsEvents.ChangePlayerCoin();
        }
        public void UseCoin(int amount)
        {
            coin -= amount;
            GameEventsManager.Instance.StatsEvents.ChangePlayerCoin();
        }

        public void ChangeMoralePoint(int amount)
        {
            moralePoint += amount;
        }

        public void ResetStats()
        {
            _playerData.ResetStats();
        }
        
    }
}