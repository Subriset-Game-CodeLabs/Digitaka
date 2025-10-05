using UnityEngine;

public abstract class StatsData:ScriptableObject
{
    [field:SerializeField] public string characterName { get; set; } 
    [field:SerializeField] public float maxHealth { get; set; } = 3;
    [field:SerializeField] public float health { get; set; } = 3;
    [field:SerializeField] public float damage { get; set; } = 1;
    public abstract void ResetStats(bool resetAll);
}
