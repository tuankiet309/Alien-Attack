using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIComponent : MonoBehaviour //Để xử lí ui thanh máu: cài đặt vị trí blah blah
{
    [SerializeField] HealthBar healthBarToSpawn;
    [SerializeField] Transform attachedPoin;
    [SerializeField] HealthComponent healthComponent;
    private void Start()
    {
        InGameUI inGameUI = FindObjectOfType<InGameUI>();   
        HealthBar healthBar = Instantiate(healthBarToSpawn,inGameUI.transform);
        healthBar.Init(attachedPoin);
        healthComponent.onHealthChange += healthBar.SetHealthSliderValue;
        healthComponent.onHealthEmpty += healthBar.OnOwnerDead;

    }
}
