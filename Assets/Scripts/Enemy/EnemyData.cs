using DefaultNamespace;
using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData")]
    public class EnemyData: StatsData
    {
        public override void ResetStats()
        {
            maxHealth = 3;
            health = 3;
            damage = 1;
        }
    }
}