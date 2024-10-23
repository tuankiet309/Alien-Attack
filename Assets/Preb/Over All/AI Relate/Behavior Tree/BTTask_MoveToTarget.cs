using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTTask_MoveToTarget : BT_Node
{
    NavMeshAgent agent;
    string targetKey;
    GameObject target;
    float acceptableDistance = 1f;
    Behavior_Tree Tree;
    public BTTask_MoveToTarget(Behavior_Tree tree, string targetKey,float acceptableDistance)
    {
        this.Tree = tree;
        this.targetKey = targetKey;
        this.acceptableDistance = acceptableDistance;

    }

    protected override NodeResult Execute()
    {
        BlackBoard blackBoard = Tree.Board;
        blackBoard.onBlackBoardValueChange += BlackBoardValueChanged;
        if (blackBoard == null || !blackBoard.GetData(targetKey,out target) ) 
        {
            return NodeResult.Failure;
        }
        agent = Tree.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            return NodeResult.Failure;
        }
        if(IsTargetInAcceptableDistance())
        {
            return NodeResult.Success;
        }
        agent.SetDestination(target.transform.position);
        agent.isStopped = false;
        return NodeResult.Inprogress;
    }

    private void BlackBoardValueChanged(string key, object value)
    {
        if(key == targetKey)
        {
            target = (GameObject)value;
        }
    }

    protected override NodeResult Update()
    {
        if(target ==null)
        {
            agent.isStopped = true;
            return NodeResult.Failure;
        }
        agent.SetDestination(target.transform.position);
        if (IsTargetInAcceptableDistance())
        {
            agent.isStopped = true;
            return NodeResult.Success;
        }
        return NodeResult.Inprogress;
    }
    bool IsTargetInAcceptableDistance()
    { 
        return Vector3.Distance(target.transform.position,Tree.transform.position) <= acceptableDistance; 
    }

    protected override void End()
    {
        agent.isStopped = true;
        Tree.Board.onBlackBoardValueChange -= BlackBoardValueChanged;
        base.End();
    }
}
