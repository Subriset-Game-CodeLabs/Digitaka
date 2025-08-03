using DefaultNamespace;
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
        public override void ResetStats()
        {
            maxHealth = 3;
            health = 3;
            maxMana = 3;
            mana = 3;
            healPotion = 1;
            manaPotion = 1;
            coin = 0;
        }
    }
}