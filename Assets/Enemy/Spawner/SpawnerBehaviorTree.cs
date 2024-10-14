using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviorTree : Behavior_Tree
{
    protected override void ConstructTree(out BT_Node root_Node)
    {
        BTTask_Spawn bTTask_Spawn = new BTTask_Spawn(this);
        CoolDownDecorator coolDownDecorator = new CoolDownDecorator(this, bTTask_Spawn, 5f);
        BlackBoard_Decorator spawnBBDecorator = new BlackBoard_Decorator(this, coolDownDecorator,
                                                                         "Target",
                                                                         BlackBoard_Decorator.Condiction.KeyExist,
                                                                         BlackBoard_Decorator.NotifyRules.RunCondictionChanged,
                                                                         BlackBoard_Decorator.NotifyAbort.Both
                                                                            );
        root_Node = spawnBBDecorator;
    }
}
