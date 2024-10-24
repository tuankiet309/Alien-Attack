using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_GetNextPatrolPoint : BT_Node
{
    PatrollingComponent patrollingComponent;
    Behavior_Tree tree;
    string partrolKey;
   public BTTask_GetNextPatrolPoint(Behavior_Tree tree, string patrolkey)
    {
        patrollingComponent = tree.GetComponent<PatrollingComponent>();
        this.tree = tree;
        this.partrolKey = patrolkey;
    }
    protected override NodeResult Execute()
    {
        if (patrollingComponent != null && patrollingComponent.GetNextPatrolPoint(out Vector3 point))
        {
            tree.Board.SetOrAddData(partrolKey, point);
            return NodeResult.Success;
        }
        else
            return NodeResult.Failure;

    }
}
