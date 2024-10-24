
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] TextMeshProUGUI Title;
    [SerializeField] TextMeshProUGUI Price ;
    [SerializeField] TextMeshProUGUI Description;

    [SerializeField] Button button;
    [SerializeField] Image GrayOutCover;

    ShopItem item;

    [SerializeField] Color unableToBuyColor;
    [SerializeField] Color ableToBuyColor;

    public delegate void OnItemSelected(ShopItemUI item);
    public event OnItemSelected onItemSelected;



    public ShopItem GetItem()
    {
        return item;
    }
    private void Start()
    {
        button.onClick.AddListener(ItemSelected);
    }

    private void ItemSelected()
    {
        onItemSelected?.Invoke(this);
    }

    public void Init(ShopItem item, int AvaliableCredits)
    {
        this.item = item;
        
        Icon.sprite = item.ItemIcon;
        Title.text = item.Title;
        Price.text = "$" + item.Price.ToString();
        Description.text = item.Description;

        Refresh(AvaliableCredits);
    }

    public void Refresh(int avaliableCredits)
    {
        if(avaliableCredits < item.Price)
        {
            GrayOutCover.enabled = true;
            Price.color = unableToBuyColor;
        }
        else
        {
            GrayOutCover.enabled = false;
            Price.color =  ableToBuyColor;
        }
    }
   
}
