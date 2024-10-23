using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_Attack : BT_Node
{
    Behavior_Tree tree;
    string targetKey;
    GameObject target;

    public BTTask_Attack(Behavior_Tree tree,  string targetKey)
    {
        this.tree = tree;
        this.targetKey = targetKey;
    }

    protected override NodeResult Execute()
    {
        if(!tree || tree.Board == null || !tree.Board.GetData(targetKey, out target) )
        {
            return NodeResult.Failure;
        }

        BehaviorTreeInterface behaviorTreeInterface = tree.GetBehaviorTreeInterface();
        if ((behaviorTreeInterface==null))
        {
            return NodeResult.Failure;
        }

        behaviorTreeInterface.AttackTarget(target);
        return NodeResult.Success;
    }
}
