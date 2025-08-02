using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    [SerializeField]
    private List<ItemBaseSO> shopItems;

    [SerializeField] int _moneyPlaceholder;
    [SerializeField] private int _moralePlaceholder;
    [SerializeField] private int _healthPotionPlaceholder;
    [SerializeField] private int _manaPotionPlaceholder;

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
        GameEventsManager.Instance.ShopEvents.InitializeShop(CalculatePriceFromMoral());
    }

    public void BuyItem(ItemBaseSO item)
    {
        Debug.Log(item.ItemName + " Buyed");
        // check for money
        bool hasMoney = false;
        int itemPrice;
        if (item.ItemPrice != item.ItemDiscountPrice)
        {
            itemPrice = item.ItemDiscountPrice;
        }
        else
        {
            itemPrice = item.ItemPrice;
        }

        hasMoney = HasMoney(itemPrice);

        if (hasMoney)
        {
            GameEventsManager.Instance.ShopEvents.BuyItemSuccess(item);
            _moneyPlaceholder -= itemPrice;
            if (item.itemType == ItemType.Consumable)
            {
                ItemConsumableSO itemConsumable = (ItemConsumableSO)item;
                switch (itemConsumable.statEffect)
                {
                    case StatEffect.Health:
                        _healthPotionPlaceholder += 1;
                        break;
                    case StatEffect.Mana:
                        _manaPotionPlaceholder += 1;
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
        return ItemPrice <= _moneyPlaceholder;
    }

    public List<ItemBaseSO> CalculatePriceFromMoral()
    {
        int newPrice;
        for (var i = 0; i < shopItems.Count; i++)
        {
            if (_moralePlaceholder == 0)
            {
                newPrice = shopItems[i].ItemPrice;
            }
            else if (_moralePlaceholder > 30)
            {
                newPrice = (int)Math.Round(shopItems[i].ItemPrice * 0.7);
            }
            else
            {
                newPrice = (int)Math.Round(shopItems[i].ItemPrice * 1.3);
            }
            shopItems[i].ItemDiscountPrice = newPrice;
        }
        Debug.Log(shopItems);
        return shopItems;
    }

}