using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelUI : MonoBehaviour
{
    [SerializeField]
    private GameObject contentParent;

    [SerializeField]
    private TextMeshProUGUI _itemNameText;

    [SerializeField]
    private TextMeshProUGUI _itemDescText;

    [SerializeField]
    private TextMeshProUGUI _itemPriceText;

    [SerializeField]
    private TextMeshProUGUI _itemDiscountPriceText;

    [SerializeField]
    private GameObject _itemPrefab;

    [SerializeField]
    private RectTransform _itemShopParent;

    [SerializeField]
    private Button _buyButton;

    [SerializeField]
    private Button _closeButton;

    [SerializeField]
    private GameObject _discountEffect;

    private ItemBaseSO _selectedItem;

    void Start()
    {
        _buyButton.onClick.AddListener(() =>
        {
            GameEventsManager.Instance.ShopEvents.BuyItem(_selectedItem);
        });

        _closeButton.onClick.AddListener(() =>
        {
            contentParent.SetActive(false);
        });

        _discountEffect.SetActive(false);
        _itemPriceText.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        GameEventsManager.Instance.ShopEvents.OnUpdateChoiceItem += UpdateChoiceItem;
        GameEventsManager.Instance.ShopEvents.OnInitializeShop += InitializeShop;
    }

    void OnDisable()
    {
        GameEventsManager.Instance.ShopEvents.OnUpdateChoiceItem -= UpdateChoiceItem;
        GameEventsManager.Instance.ShopEvents.OnInitializeShop -= InitializeShop;
    }

    public void UpdateChoiceItem(ItemBaseSO selectedItem)
    {
        _selectedItem = selectedItem;
        _itemNameText.text = selectedItem.ItemName;
        _itemDescText.text = selectedItem.ItemDescription;
        _itemDiscountPriceText.text = selectedItem.ItemDiscountPrice.ToString();
        _itemPriceText.text = selectedItem.ItemPrice.ToString();
        if (selectedItem.ItemDiscountPrice != selectedItem.ItemPrice)
        {
            _itemPriceText.gameObject.SetActive(true);
            _discountEffect.SetActive(true);
        }
        else
        {
            _itemPriceText.gameObject.SetActive(false);
            _discountEffect.SetActive(false);
        }
    }

    public void InitializeShop(List<ItemBaseSO> items)
    {
        contentParent.SetActive(true);
        ClearParent();
        bool first = true;
        foreach (ItemBaseSO item in items)
        {
            GameObject gameObject = Instantiate(_itemPrefab, _itemShopParent);
            ShopItemUI shopItemUI = gameObject.GetComponent<ShopItemUI>();
            shopItemUI.SetItemData(item);
            shopItemUI.SetItemSprite(item.ItemSprite);
            if (first)
            {
                shopItemUI.SelectButton();
                first = false;
            }
        }
    }

    private void ClearParent()
    {
        foreach (Transform child in _itemShopParent)
        {
            Destroy(child.gameObject);
        }
    }
}
