using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/HealthRegen")]
public class HealthRegenAbility : Ability
{
    [SerializeField] float healthAmt;
    [SerializeField] float healthRegenDuration;
    HealthComponent healthComponent;

    public override void Active()
    {
        if (!CommitAbility())
        {
            return;
        }
        healthComponent= AbilityComponent.GetComponent<HealthComponent>();
        if (healthComponent != null) 
        {
            if (healthRegenDuration == 0)
            {
                healthComponent.ChangeHealth(healthAmt, AbilityComponent.gameObject);
                return; 
            }
            AbilityComponent.StartCoroutine(StartHealthRegen(healthAmt, healthRegenDuration, healthComponent));
        }
    }

    IEnumerator StartHealthRegen(float amt, float duration, HealthComponent healthComponent)
    {
        
        float counter = duration;
        float regenRate = amt / duration;
        while (counter > 0) 
        {
            counter -= Time.deltaTime;
            healthComponent.ChangeHealth(regenRate*Time.deltaTime, AbilityComponent.gameObject);
            yield return new WaitForEndOfFrame();
        }
    }
}
