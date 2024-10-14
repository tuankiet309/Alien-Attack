using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownDecorator : Decorator
{
    float coolDownTime;
    float lastExecutionTime = -1;
    bool failedOnCoolDown;
    public CoolDownDecorator( Behavior_Tree tree, BT_Node child, float coolDownTime, bool failedOnCoolDown = false) : base(child)
    {

        this.coolDownTime = coolDownTime;
        this.failedOnCoolDown = failedOnCoolDown;
    }
    protected override NodeResult Execute()
    {
        if (coolDownTime==0)
        {
            return NodeResult.Inprogress;
        }
        //first execute
        if (lastExecutionTime ==-1) 
        {
            lastExecutionTime = Time.timeSinceLevelLoad;
            return NodeResult.Inprogress;
        }
        //cooldown not finished
        if (Time.timeSinceLevelLoad - lastExecutionTime < coolDownTime)
        {
            if (failedOnCoolDown)
            {
                return NodeResult.Failure;
            }
            else
                return NodeResult.Success;
        }
        //cooldown is finished since lasttime
        lastExecutionTime = Time.timeSinceLevelLoad;
        return NodeResult.Inprogress;
    }
    protected override NodeResult Update()
    {
        return GetChild().UpdateNode();
    }
}
