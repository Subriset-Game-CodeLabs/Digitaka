using TwoDotFiveDimension;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour, ISelectHandler
{
    [SerializeField]
    private Image _itemSprite;

    [SerializeField]
    private GameObject _lockSprite;
    [SerializeField]
    private ItemBaseSO _itemData;
    [SerializeField]
    private Button button;

    public void SetItemSprite(Sprite ItemSprite)
    {
        _itemSprite.sprite = ItemSprite;
    }

    public void SetItemData(ItemBaseSO ItemData)
    {
        _itemData = ItemData;
        if (ItemData.moraleToUnlock == 0)
        {
            return;
        }
        else if (ItemData.moraleToUnlock > PlayerStats.Instance.moralePoint)
        {
            _lockSprite.SetActive(true);
        }

    }
    void OnEnable()
    {
        GameEventsManager.Instance.ShopEvents.OnBuyItemSuccess += BuyItemSuccess;
    }

    void OnDisable()
    {
        GameEventsManager.Instance.ShopEvents.OnBuyItemSuccess -= BuyItemSuccess;
    }

    private void BuyItemSuccess(ItemBaseSO item)
    {
        if (item == _itemData && item is ItemEquipmentSO itemEquipment && itemEquipment.isBought)
        {
            Destroy(gameObject);
        }
    }
    public Button GetButton()
    {
        return button;
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameEventsManager.Instance.ShopEvents.UpdateChoiceItem(_itemData, button);
    }
}
