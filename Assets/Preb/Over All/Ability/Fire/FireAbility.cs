using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Ability/Fire")]
public class FireAbility : Ability
{
    [SerializeField] Scanner scannerPreb;
    [SerializeField] float fireRadius;
    [SerializeField] float fireDuration;
    [SerializeField] GameObject scanVFX;
    [SerializeField] GameObject damageVFX;
    [SerializeField] float damageDuration;
    [SerializeField] float damage;

    public override void Active()
    {
        if (!CommitAbility())
            return;
        Scanner fireScanner = Instantiate(scannerPreb,AbilityComponent.transform);
        fireScanner.SetScanRange(fireRadius);
        fireScanner.SetScanDuration(fireDuration);
        fireScanner.AddChildAttached(Instantiate(scanVFX).transform);
        fireScanner.onScanDetectionUpdate += DetectionUpdate;
        fireScanner.StartScanning();
    }

    private void DetectionUpdate(GameObject newDetection)
    {
        ITeamInterface detectedTeamInterface = newDetection.GetComponent<ITeamInterface>();
        if (detectedTeamInterface == null || detectedTeamInterface.GetRalationToward(AbilityComponent.gameObject) != ETeamRalation.Enemy)
            return;

        HealthComponent health = newDetection.GetComponent<HealthComponent>();
        if (health == null) return;
        AbilityComponent.StartCoroutine(ApplyDamageTo(health));
    }

    private IEnumerator ApplyDamageTo(HealthComponent health)
    {
        GameObject damageVFX = Instantiate(this.damageVFX,health.transform);
        float damageRate = damage / damageDuration;
        float startTime = 0;
        while(startTime < damageDuration && health!=null)
        {
           startTime += Time.deltaTime;
            health.ChangeHealth(-damageRate * Time.deltaTime,AbilityComponent.gameObject);
            yield return new WaitForEndOfFrame();
        }
        if (damageVFX != null) 
        {
            Destroy(damageVFX);
        }
    }
}
