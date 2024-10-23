using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_PatrollingGroup : BTTask_Group
{
    [SerializeField] float acceptableDistance;
    public BTTask_PatrollingGroup(Behavior_Tree tree, float acceptableDistance = 2f) : base(tree)
    {
        this.acceptableDistance = acceptableDistance;
    }

    protected override void ConstructTree(out BT_Node root)
    {
        Sequencer patrollSQL = new Sequencer();
        BTTask_GetNextPatrolPoint bTTask_GetNextPatrolPoint = new BTTask_GetNextPatrolPoint(tree, "Patrol");
        BTTask_MoveToALocation bTTask_MoveToPatrolPoint = new BTTask_MoveToALocation(tree, "Patrol", acceptableDistance);
        BTTask_Wait BTTask_waitAtPatrolPoint = new BTTask_Wait(2);

        patrollSQL.AddChild(bTTask_GetNextPatrolPoint);
        patrollSQL.AddChild(bTTask_MoveToPatrolPoint);
        patrollSQL.AddChild(BTTask_waitAtPatrolPoint);

        root = patrollSQL;
    }
}
