using System.Collections.Generic;
using Input;
using TMPro;
using TwoDotFiveDimension;
using UIController;
using UnityEditor;
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
    private Button _informationCloseButton;

    [SerializeField]
    private GameObject _informationPanel;
    [SerializeField]
    private TextMeshProUGUI _informationTextTitle;
    [SerializeField]
    private TextMeshProUGUI _informationTextDesc;

    [SerializeField]
    private GameObject _discountEffect;

    private ItemBaseSO _selectedItem;
    private ShopItemUI _firstItem;
    private Button _selectedButton;

    void Start()
    {
        _buyButton.onClick.AddListener(() =>
        {
            GameEventsManager.Instance.ShopEvents.BuyItem(_selectedItem);
        });

        _closeButton.onClick.AddListener(() =>
        {
            UIManager.Instance.HideShopPanel();
            _informationPanel.SetActive(false);
            InputManager.Instance.UIMode();
            GameEventsManager.Instance.DialogueEvents.DialogueResumed();
        });

        _informationCloseButton.onClick.AddListener(() =>
        {
            _informationPanel.SetActive(false);
            _selectedButton.Select();
        });

        _discountEffect.SetActive(false);
        _itemPriceText.gameObject.SetActive(false);
        _informationPanel.SetActive(false);
    }

    void OnEnable()
    {
        GameEventsManager.Instance.ShopEvents.OnUpdateChoiceItem += UpdateChoiceItem;
        GameEventsManager.Instance.ShopEvents.OnInitializeShop += InitializeShop;
        GameEventsManager.Instance.ShopEvents.OnBuyItemFailed += BuyItemFailed;
        GameEventsManager.Instance.ShopEvents.OnBuyItemSuccess += BuyitemSucces;
    }

    void OnDisable()
    {
        GameEventsManager.Instance.ShopEvents.OnUpdateChoiceItem -= UpdateChoiceItem;
        GameEventsManager.Instance.ShopEvents.OnInitializeShop -= InitializeShop;
        GameEventsManager.Instance.ShopEvents.OnBuyItemFailed -= BuyItemFailed;
        GameEventsManager.Instance.ShopEvents.OnBuyItemSuccess -= BuyitemSucces;
    }

    public void UpdateChoiceItem(ItemBaseSO selectedItem, Button selectedButton)
    {
        if (selectedItem.moraleToUnlock > PlayerStats.Instance.moralePoint && selectedItem.moraleToUnlock != 0)
        {
            _informationPanel.SetActive(true);
            _informationTextTitle.text = "GAGAL";
            _informationTextDesc.text =
                $"Kamu perlu {selectedItem.moraleToUnlock} moral untuk membuka item ini";
            return;
        }
        _selectedButton = selectedButton;
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
        UIManager.Instance.ShowShopPanel();
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
                _selectedButton = shopItemUI.GetButton();
                _selectedButton.Select();
                _firstItem = shopItemUI;
                first = false;
            }
        }
        InputManager.Instance.ShopMode();
    }

    public void BuyItemFailed(ItemBaseSO item)
    {
        _informationPanel.SetActive(true);
        _informationTextTitle.text = "FAILED";
        _informationTextDesc.text = "You Dont Have Enough Money";
    }

    public void BuyitemSucces(ItemBaseSO item)
    {
        _informationPanel.SetActive(true);
        _informationTextTitle.text = "SUCCESS";
        _informationTextDesc.text = "+1 " + item.ItemName;

        if (item.itemType == ItemType.Equipment)
        {
            _selectedButton = _firstItem.GetButton();
            _selectedButton.Select();
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
