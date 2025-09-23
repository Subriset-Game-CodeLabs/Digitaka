using System;
using System.Collections.Generic;
using Audio;
using Input;
using TwoDotFiveDimension;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

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

    [SerializeField]
    private List<ItemBaseSO> shopItems;

    public void ResetItem()
    {
        Debug.Log("Reset Item");
        foreach (ItemBaseSO item in shopItems)
        {
            if (item.itemType == ItemType.Equipment)
            {
                ItemEquipmentSO itemEquipment = (ItemEquipmentSO)item;
                itemEquipment.isBought = false;
                Debug.Log("false");
            }
        }
    }

    void OnEnable()
    {
        GameEventsManager.Instance.ShopEvents.OnShopOpen += OpenShop;
        GameEventsManager.Instance.ShopEvents.OnBuyItem += BuyItem;
    }

    void OnDisable()
    {
        GameEventsManager.Instance.ShopEvents.OnShopOpen -= OpenShop;
        GameEventsManager.Instance.ShopEvents.OnBuyItem -= BuyItem;
    }

    public void OpenShop()
    {
        InputManager.Instance.UIMode();
        GameEventsManager.Instance.ShopEvents.InitializeShop(CalculatePriceFromMoral());
    }

    public void BuyItem(ItemBaseSO item)
    {
        // check for money
        int itemPrice;
        if (item.ItemPrice != item.ItemDiscountPrice)
        {
            itemPrice = item.ItemDiscountPrice;
        }
        else
        {
            itemPrice = item.ItemPrice;
        }

        bool hasMoney = HasMoney(itemPrice);

        if (hasMoney)
        {
            if (item.itemType == ItemType.Equipment)
            {
                ItemEquipmentSO itemEquipment = (ItemEquipmentSO)item;
                itemEquipment.isBought = true;
                shopItems.Remove(item);
                switch (itemEquipment.statEffect)
                {
                    case StatEffect.Damage:
                        PlayerStats.Instance.ChangeDamage(itemEquipment.amount);
                        break;
                    case StatEffect.UltimateManaCost:
                        PlayerStats.Instance.ChangeUltimateCost((int)itemEquipment.amount);
                        break;
                }
            }

            GameEventsManager.Instance.ShopEvents.BuyItemSuccess(item);
            AudioManager.Instance.PlaySound(SoundType.SFX_PurchaseItem);
            PlayerStats.Instance.UseCoin(itemPrice);
            if (item.itemType == ItemType.Consumable)
            {
                ItemConsumableSO itemConsumable = (ItemConsumableSO)item;
                switch (itemConsumable.statEffect)
                {
                    case StatEffect.Health:
                        PlayerStats.Instance.AddHealthPotion(1);
                        break;
                    case StatEffect.Mana:
                        PlayerStats.Instance.AddManaPotion(1);
                        break;
                }
            }
        }
        else
        {
            GameEventsManager.Instance.ShopEvents.BuyItemFailed(item);
        }
    }

    private bool HasMoney(int ItemPrice)
    {
        return ItemPrice <= PlayerStats.Instance.coin;
    }

    public List<ItemBaseSO> CalculatePriceFromMoral()
    {
        int newPrice;
        for (var i = 0; i < shopItems.Count; i++)
        {
            if (PlayerStats.Instance.moralePoint > 50)
            {
                newPrice = (int)Math.Round(shopItems[i].ItemPrice * 0.8);
            }
            else if (PlayerStats.Instance.moralePoint < -50)
            {
                newPrice = (int)Math.Round(shopItems[i].ItemPrice * 1.2);
            }
            else
            {
                newPrice = shopItems[i].ItemPrice;
            }
            shopItems[i].ItemDiscountPrice = newPrice;
        }
        return shopItems;
    }
}
