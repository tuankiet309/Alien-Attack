using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/ShopSystem")]

public class ShopSystemClass : ScriptableObject
{
    [SerializeField] ShopItem[] shopItems;
    
    public ShopItem[] GetShopItems()
    {
        return shopItems;
    }
    public bool TryPurchase(ShopItem selectedItem, CreditComponent purchaser)
    {
        return purchaser.Purchased(selectedItem.Price, selectedItem.Item);
    }
}
