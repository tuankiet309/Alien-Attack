using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BTTask_Group : BT_Node
{
    BT_Node root;
    protected Behavior_Tree tree;
    
    public BTTask_Group(Behavior_Tree tree)
    {
        this.tree = tree;
    }
    protected override NodeResult Execute()
    {
        return NodeResult.Inprogress;
    }
    protected override NodeResult Update()
    {
        return root.UpdateNode();
    }
    protected override void End()
    {
        root.Abort();
        base.End();
    }

    public override void SortPiority(ref int piorityCounter)
    {
        base.SortPiority(ref piorityCounter);
        root.SortPiority(ref piorityCounter);
    }

    protected abstract void ConstructTree(out BT_Node root);
    
    public virtual BT_Node GetNode()
    { return this; }

    public override void Init()
    {
        base.Init();
        ConstructTree(out root);
    }
    public override BT_Node Get()
    {
        return root.Get();
    }


}
