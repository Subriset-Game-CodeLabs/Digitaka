using UIController.Stats;
using UnityEngine;

namespace DefaultNamespace.UIController
{
    public class UIManager: MonoBehaviour
    {
        public static  UIManager Instance { get; private set; }
        [SerializeField] private CooldownUI _dashCooldownUI;
        [SerializeField] private CooldownUI _ultimateCooldownUI;
        [SerializeField] private CooldownUI _healthPotionCooldownUI;
        [SerializeField] private CooldownUI _manaPotionCooldownUI;
        [SerializeField] private GameObject _pauseMenu;
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
        public void StartCooldownUltimate(float duration)
        {
            if (_ultimateCooldownUI != null)
            {
                _ultimateCooldownUI.StartCooldown(duration);
            }
        }
        public void StartCooldownHealthPotion(float duration)
        {
            if (_healthPotionCooldownUI != null)
            {
                _healthPotionCooldownUI.StartCooldown(duration);
            }
        }
        public void StartCooldownManaPotion(float duration)
        {
            if (_manaPotionCooldownUI != null)
            {
                _manaPotionCooldownUI.StartCooldown(duration);
            }
        }
        public void StartCooldownDash(float duration)
        {
            if (_dashCooldownUI != null)
            {
                _dashCooldownUI.StartCooldown(duration);
            }
        }
    }
}