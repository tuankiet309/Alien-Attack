using System.Collections;
using System.Collections.Generic;
using Preb.Over_All.AI_Relate.Behavior_Tree;
using UnityEngine;

public class BaseBossBehavior : Behavior_Tree
{
    [SerializeField] protected float acceptableDistance = 2.5f;
    [SerializeField] protected float acceptableRadius   = 10f;
    [SerializeField] protected float coolDown           = 1f;
    [SerializeField] protected float weakenedTime       = 5f;

    protected override void ConstructTree(out BT_Node root_Node)
    {
        Selector root_Selector = new Selector();

        // Attack Target
        BTTask_AttackTargetGroup attackTargetGroup = new BTTask_AttackTargetGroup(this, acceptableDistance, acceptableRadius, coolDown);
        root_Selector.AddChild(attackTargetGroup);

        // Evade (Placeholder, can be customized by child bosses)
        BTTask_Evade evadeTask = new BTTask_Evade(this);
        root_Selector.AddChild(evadeTask);

        // Weakened State
        BTTask_Weakened weakenedTask = new BTTask_Weakened(this);
        root_Selector.AddChild(weakenedTask);

        // Patrolling
        BTTask_PatrollingGroup patrolTask = new BTTask_PatrollingGroup(this);
        root_Selector.AddChild(patrolTask);

        root_Node = root_Selector;
    }

    public virtual void EnterWeakenedState()
    {
        Debug.Log("Boss entered weakened state.");
        // Disable boss movement and attacks
    }

    public virtual bool IsWeakenedComplete()
    {
        // Return true when the weakened time is over
        return false;
    }

    public virtual void StartEvade()
    {
        Debug.Log("Boss is evading.");
        // Logic for boss to evade (run around)
    }

    public virtual bool IsEvadeComplete()
    {
        // Return true when the evasion ends
        return false;
    }
}