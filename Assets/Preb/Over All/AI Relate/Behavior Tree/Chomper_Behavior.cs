using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper_Behavior : Behavior_Tree
{
    [SerializeField] float acceptableDistance = 2.5f;
    [SerializeField] float acceptableRadius = 10f;
    [SerializeField] float coolDown = .6f;
    protected override void ConstructTree(out BT_Node root_Node)
    {
        Selector root_Selector = new Selector();
        //Attack
        BTTask_AttackTargetGroup attackTargetGroup = new BTTask_AttackTargetGroup(this, acceptableDistance, acceptableRadius, coolDown);
        root_Selector.AddChild(attackTargetGroup);

        //Check Last Seen
        BTTask_MoveToLastLocationGroup bTTask_MoveToLastLocationGroup = new BTTask_MoveToLastLocationGroup(this);
        root_Selector.AddChild(bTTask_MoveToLastLocationGroup);

        //Patrol
        BTTask_PatrollingGroup bTTask_PatrollingGroup = new BTTask_PatrollingGroup(this);
        root_Selector.AddChild(bTTask_PatrollingGroup);
        root_Node = root_Selector;
    }

}
