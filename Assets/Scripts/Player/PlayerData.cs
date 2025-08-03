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