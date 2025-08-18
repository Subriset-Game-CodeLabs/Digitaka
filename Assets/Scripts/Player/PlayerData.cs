using UnityEngine;

namespace TwoDotFiveDimension
{
    [CreateAssetMenu(fileName = "PlayerStatsData", menuName = "Data/PlayerStatsData")]
    public class PlayerData: StatsData
    {
        [field:SerializeField] public int maxMana { get; set; } = 3;
        [field:SerializeField] public int mana { get; set; }
        [field:SerializeField] public int healPotion { get; set; }
        [field:SerializeField] public int manaPotion { get; set; } = 1;
        [field:SerializeField] public int coin { get; set; } = 1;
        [field:SerializeField] public int UltimateDamage = 2;
        [field:SerializeField] public int UltimateManaCost = 2;
        
        [Header("Dash Settings")]
        [field:SerializeField] public float DashDuration  { get; set; }
        [field:SerializeField] public int DashSpeed  { get; set; }
        [field:SerializeField] public float DashCooldown  { get; set; } = 1f;
       
        [Header("Ultimate Settings")]
        [field:SerializeField] public float UltimateCooldown  { get; set; } = 5f;
        
        [Header("Potion Settings")]
        [field:SerializeField] public float HealthPotionCooldown  { get; set; } = 2f;
        [field:SerializeField] public float ManaPotionCooldown  { get; set; } = 2f;
        public override void ResetStats()
        {
            health = maxHealth;
            mana = maxMana;
            healPotion = 5;
            manaPotion = 5;
            coin = 5;
        }
    }
}