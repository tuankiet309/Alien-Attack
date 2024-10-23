using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter_Behavior_Tree : Behavior_Tree
{
    [SerializeField] float acceptableDistance = 9;
    [SerializeField] float acceptableRadius = 10;
    [SerializeField] float coolDown = 4f;
    protected override void ConstructTree(out BT_Node root_Node)
    {
        Selector RootSelector = new Selector();
        RootSelector.AddChild(new BTTask_AttackTargetGroup(this, acceptableDistance, acceptableRadius, coolDown));
        RootSelector.AddChild(new BTTask_MoveToLastLocationGroup(this));
        RootSelector.AddChild(new BTTask_PatrollingGroup(this));
        root_Node = RootSelector;
    }
}
