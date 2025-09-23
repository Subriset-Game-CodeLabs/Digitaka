using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumable")]
public class ItemConsumableSO : ItemBaseSO
{
    public StatEffect statEffect;
    public int amount;
}

public enum StatEffect
{
    Health,
    Mana,
    UltimateManaCost,
    Damage
}