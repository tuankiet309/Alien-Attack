using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BehaviorTreeInterface 
{
    public void RotateToward(GameObject target, bool verticleAim = false);
    public  void AttackTarget(GameObject target);
}
