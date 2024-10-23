using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] ShopSystemClass shopSystemClass;
    [SerializeField] ShopItemUI shopItemUIPrefab;
    [SerializeField] RectTransform shopList;
    [SerializeField] CreditComponent creditComponent;
    List<ShopItemUI> shopItemUIs = new List<ShopItemUI>();
    [SerializeField] UIManager UImanager;
    [SerializeField] TextMeshProUGUI creditText;
    [SerializeField] Button backBtn;
    [SerializeField] Button buyBtn;
    ShopItemUI selectedItem;
    void Start()
    {
        Init();
        backBtn.onClick.AddListener(UImanager.SwitchToGameplay);
        buyBtn.onClick.AddListener(TryPurchaseItem);
        creditComponent.onCreditChanged += UpdateCredit;
        UpdateCredit(creditComponent.Credit);
    }

    private void TryPurchaseItem()
    {
        if (!selectedItem || !shopSystemClass.TryPurchase(selectedItem.GetItem(), creditComponent))
            return;
        shopItemUIs.Remove(selectedItem);
        Destroy(selectedItem.gameObject);
    }

    private void UpdateCredit(int newCredit)
    {
        creditText.text = newCredit.ToString();
        RefreshItem();
    }
    private void Init()
    {
        ShopItem[] shopItems = shopSystemClass.GetShopItems();
        foreach (ShopItem shopItem in shopItems)
        {
            AddShopItem(shopItem);
        }
    }

    private void AddShopItem(ShopItem shopItem)
    {
        ShopItemUI newShopItemUI = Instantiate(shopItemUIPrefab, shopList);
        newShopItemUI.Init(shopItem, creditComponent.Credit);
        newShopItemUI.onItemSelected += ItemSelected;
        shopItemUIs.Add(newShopItemUI);
    }
    private void RemoveShopItem(ShopItemUI shopItem)
    {
        shopItemUIs.Remove(shopItem);
    }
    private void ItemSelected(ShopItemUI item)
    {
        selectedItem = item;
    }
    private void RefreshItem()
    {
        foreach(var shopItemUI in shopItemUIs)
        {
            shopItemUI.Refresh(creditComponent.Credit);
        }
    }
}
