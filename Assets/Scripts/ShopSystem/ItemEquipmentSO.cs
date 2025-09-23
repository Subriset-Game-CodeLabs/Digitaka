using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipment")]
public class ItemEquipmentSO : ItemBaseSO
{
    public bool isBought;
    public StatEffect statEffect;
    public float amount;
}
