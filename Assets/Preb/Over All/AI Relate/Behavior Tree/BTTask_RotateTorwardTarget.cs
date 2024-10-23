using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_RotateTorwardTarget : BT_Node
{
    Behavior_Tree tree;
    string targetKey;
    float acceptableRadius;
    GameObject target;
    BehaviorTreeInterface treeInterface;
    
    public BTTask_RotateTorwardTarget(Behavior_Tree tree, string targetKey, float acceptableRadius)
    {
        this.tree = tree;
        this.targetKey = targetKey;
        this.acceptableRadius = acceptableRadius;
        this.treeInterface = this.tree.GetBehaviorTreeInterface();
    }
    protected override NodeResult Execute()
    {
        if (tree == null || tree.Board == null)
        {
            return NodeResult.Failure;
        }
        if (treeInterface == null) 
        { 
            return NodeResult.Failure;
        }
        if (!tree.Board.GetData(targetKey, out target))
        {
            return NodeResult.Failure;
        }
        if(IsInAcceptableRadius())
        {
            return NodeResult.Success;
        }

        tree.Board.onBlackBoardValueChange += BlackBoardValueChanged;
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
        if (IsInAcceptableRadius())
            return NodeResult.Success;
        treeInterface.RotateToward(target);
        return NodeResult.Inprogress;

    }

    protected override void End()
    {
        tree.Board.onBlackBoardValueChange -= BlackBoardValueChanged;
    }
    bool IsInAcceptableRadius()
    {
        if (target == null)
            return false;
        Vector3 targetDir = (target.transform.position - tree.transform.position).normalized;
        Vector3 dir = tree.transform.forward;
        float radius = Vector3.Angle(targetDir, dir);
        return radius < acceptableRadius;
    }
}
