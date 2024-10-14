using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode_Chomper : Chomper,IExplodeAble
{
    [Header("Explode Info")]
    [SerializeField] ParticleSystem explode;

    [SerializeField] TriggerDamageComponent explodeDMGCOMP;

    ITeamInterface teamInterface;
    
    
    
    public void Explode()
    {
        Vector3 spawnPos = transform.position;
        Instantiate(explode, transform.position + Vector3.up, transform.rotation);
        teamInterface = GetComponent<ITeamInterface>();
        if (teamInterface != null)
        {
            explodeDMGCOMP.SetTeamInterface(teamInterface);
        }
        explodeDMGCOMP.SetDamageEnable(true);
        Debug.Log("DealDamage");
    }

    protected override void Dead()
    {
        Explode();
    }

}
