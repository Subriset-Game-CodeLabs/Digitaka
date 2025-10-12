using UnityEngine;

public class ItemBaseSO : DigitakaData
{
    public string ItemName;
    public Sprite ItemSprite;
    public ItemType itemType;
    public int ItemPrice;
    public int ItemDiscountPrice = 0;
    public string ItemDescription;
    public int moraleToUnlock;
}

public enum ItemType
{
    Consumable,
    Equipment
}
