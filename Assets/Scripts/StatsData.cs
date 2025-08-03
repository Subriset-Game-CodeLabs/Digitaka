using UnityEngine;

public abstract class StatsData:ScriptableObject
{
    [field:SerializeField] public string characterName { get; set; } 
    [field:SerializeField] public int maxHealth { get; set; } = 3;
    [field:SerializeField] public int health { get; set; } = 3;
    [field:SerializeField] public int damage { get; set; } = 1;
    public abstract void ResetStats();
}
