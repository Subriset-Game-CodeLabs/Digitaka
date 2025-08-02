using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour, ISelectHandler
{
    [SerializeField]
    private Image _itemSprite;
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
    }

    public void SelectButton()
    {
        button.Select();
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameEventsManager.Instance.ShopEvents.UpdateChoiceItem(_itemData);
    }
}
