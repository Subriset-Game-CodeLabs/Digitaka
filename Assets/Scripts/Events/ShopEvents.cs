using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Events
{
    public class ShopEvents
    {
        public event Action<ItemBaseSO> OnBuyItem;
        public event Action<ItemBaseSO> OnBuyItemSuccess;
        public event Action<ItemBaseSO> OnBuyItemFailed;
        public event Action OnShopOpen;
        public event Action OnShopClose;
        public event Action<List<ItemBaseSO>> OnInitializeShop;
        public event Action<ItemBaseSO, Button> OnUpdateChoiceItem;

        public void BuyItem(ItemBaseSO item)
        {
            OnBuyItem?.Invoke(item);
        }

        public void BuyItemSuccess(ItemBaseSO item)
        {
            OnBuyItemSuccess?.Invoke(item);
        }

        public void BuyItemFailed(ItemBaseSO item)
        {
            OnBuyItemFailed?.Invoke(item);
        }

        public void ShopOpen()
        {
            OnShopOpen?.Invoke();
        }

        public void ShopClose()
        {
            OnShopClose?.Invoke();
        }

        public void InitializeShop(List<ItemBaseSO> items)
        {
            OnInitializeShop?.Invoke(items);
        }

        public void UpdateChoiceItem(ItemBaseSO item, Button button)
        {
            OnUpdateChoiceItem?.Invoke(item, button);
        }
    }
}