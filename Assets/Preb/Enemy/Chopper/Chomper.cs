using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : Enemy
{
    [SerializeField]TriggerDamageComponent dc;
   
    public override void AttackTarget(GameObject target)
    {
        Animator.SetTrigger("attack");
    }
    public void AttackPoint()
    {
        if (dc)
        {
            dc.SetDamageEnable(true);
            Debug.Log("I attack");
        }
    }
    public void AttackEnd()
    {
        if (dc)
        {
            dc.SetDamageEnable(false);
            Debug.Log("I stop");
        }
    }

    protected  override void  Start()
    {
        base.Start();
        dc.SetTeamInterface(this);
    }
}
