using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamageComponent : DamageComponent
{
    [SerializeField] float damage = 0f;
    [SerializeField] Collider trigger;
    [SerializeField] bool startedEnable = false;


    public void SetDamageEnable(bool enable)
    {

        trigger.enabled = enable; 
    }
    private void Start()
    {
        SetDamageEnable(startedEnable);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!shouldDamageThis(other.gameObject))
        {
            return;
        }
        HealthComponent health = other.GetComponent<HealthComponent>();
        if(health != null)
        {
            health.ChangeHealth(-damage, this.gameObject);
        }
    }

}
