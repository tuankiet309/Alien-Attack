using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_Spawn : BT_Node
{
    SpawnComponent spawnComponent;
    public BTTask_Spawn(Behavior_Tree tree)
    {
        spawnComponent = tree.GetComponent<SpawnComponent>();
    }
    protected override NodeResult Execute()
    {
        if (spawnComponent == null || !spawnComponent.StartSpawn())
            return NodeResult.Failure;
        return NodeResult.Success;
    }
}
