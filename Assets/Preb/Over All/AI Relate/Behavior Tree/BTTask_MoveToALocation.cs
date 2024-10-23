using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTTask_MoveToALocation : BT_Node
{
    NavMeshAgent agent;
    string patrolKey;
    Vector3 patrolPoint;
    float acceptableDistance = 1f;
    Behavior_Tree tree;

    public BTTask_MoveToALocation( Behavior_Tree tree, string patrolKey, float acceptableDistance)
    {
        this.patrolKey = patrolKey;
        this.tree = tree;
        this.acceptableDistance = acceptableDistance;
    }
    protected override NodeResult Execute()
    {
        BlackBoard blackBoard = tree.Board;
        if (blackBoard == null || !blackBoard.GetData(patrolKey,out patrolPoint)) 
        {
            return NodeResult.Failure;
        }
        agent = tree.GetComponent<NavMeshAgent>();
        if (agent == null) 
        {
            return NodeResult.Failure;
        }
        if(IsAcceptableDistance())
        {
            return NodeResult.Success;
        }
        agent.SetDestination(patrolPoint);
        agent.isStopped = false;
        return NodeResult.Inprogress;


    }
    protected override NodeResult Update()
    {
        if(IsAcceptableDistance())
        {
            agent.isStopped=true;
            return NodeResult.Success;
        }
        return NodeResult.Inprogress;
    }
    bool IsAcceptableDistance()
    {
        return Vector3.Distance(tree.transform.position, patrolPoint) <= acceptableDistance;
    }
    protected override void End()
    {
        agent.isStopped = true;
        base.End();
    }
}
