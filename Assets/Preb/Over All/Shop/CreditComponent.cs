using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPurchaseListener
{
    public bool HandlePurchase(Object newPurchase);
}
public class CreditComponent : MonoBehaviour,IRewardListener
{
    [SerializeField] int credits;
    [SerializeField] Component[] PurchaseListener;
    List<IPurchaseListener> purchaseListenersInterface = new List<IPurchaseListener>();
    public int Credit {  get { return credits; } private set { } }

    public delegate void OnCreditChanged(int newCredit);
    public event OnCreditChanged onCreditChanged;

    private void Start()
    {
        CollectPurchaseListener();
    }

    private void CollectPurchaseListener()
    {
        foreach (var listener in PurchaseListener)
        {
            IPurchaseListener listenerInterface = listener as IPurchaseListener; ;
            if(listenerInterface != null)
            {
                purchaseListenersInterface.Add(listenerInterface);
            }
        }
    }

    private void BroadcastPurchase(Object item)
    {
        foreach(IPurchaseListener purchaseListener in purchaseListenersInterface)
        {
            if (purchaseListener.HandlePurchase(item))
                return;
        }
    }
    public bool Purchased(int price, Object item)
    {
        if(credits < price)
            return false;
        credits-=price;
        BroadcastPurchase(item);
        onCreditChanged?.Invoke(credits);
        return true;
    }

    public void Reward(Reward reward)
    {
        credits += reward.creditReward;
        onCreditChanged(credits);
    }
}
