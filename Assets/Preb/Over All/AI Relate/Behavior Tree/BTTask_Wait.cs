using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_Wait : BT_Node
{
    float waitTime = 2f;

    float timeCounter = 0f;

    public BTTask_Wait(float waitTime)
    {
        this.waitTime = waitTime;
    }
    protected override NodeResult Execute()
    {
        if (waitTime <= 0)
        {
            return NodeResult.Success;
        }
        Debug.Log($"Wait started w duration {waitTime}");
        timeCounter = 0;
        return NodeResult.Inprogress;
    }

    protected override NodeResult Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= waitTime)
        {
            Debug.Log("Wait Time Complete");
            return NodeResult.Success;
        }

        //Debug.Log($"Waiting for finish {timeCounter}");
        return NodeResult.Inprogress;
    }

}
