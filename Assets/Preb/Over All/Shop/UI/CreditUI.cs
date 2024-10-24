using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CreditUI : MonoBehaviour
{
    [SerializeField] Button shopBtn;
    [SerializeField] UIManager UImanager;
    [SerializeField] TextMeshProUGUI creditText;
    [SerializeField] CreditComponent creditComponent;
    private void Start()
    {
        shopBtn.onClick.AddListener(PullOutShop);
        creditComponent.onCreditChanged += UpdateCredit;
        UpdateCredit(creditComponent.Credit);
    }

    private void UpdateCredit(int newCredit)
    {
       creditText.text = newCredit.ToString();
    }

    private void PullOutShop()
    {
        UImanager.SwitchToShop();
    }

}
