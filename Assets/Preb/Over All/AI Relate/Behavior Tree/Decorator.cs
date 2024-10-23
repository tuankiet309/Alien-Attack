using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorator : BT_Node
{
    private BT_Node child;

    protected BT_Node GetChild()
    {
        return child;
    }
    public Decorator(BT_Node child)
    {
        this.child = child;
    }
    public override void SortPiority(ref int piorityCounter)
    {
        base.SortPiority(ref piorityCounter);
        child.SortPiority(ref piorityCounter);
    }

    public override BT_Node Get()
    {
        return child.Get();
    }
    public override void Init()
    {
        base.Init();
        child.Init();
    }

}
