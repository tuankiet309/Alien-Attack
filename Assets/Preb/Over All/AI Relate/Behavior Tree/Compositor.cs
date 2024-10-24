using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compositor : BT_Node
{
    LinkedList<BT_Node> children = new LinkedList<BT_Node>();
    LinkedListNode<BT_Node> currentChild = null;

    public void AddChild(BT_Node newChild)
    {
        children.AddLast(newChild);
    }
    protected override NodeResult Execute()
    {
        if(children.Count == 0)
            return NodeResult.Success;
        currentChild = children.First;
        return NodeResult.Inprogress;
    }

    protected BT_Node GetCurrentChild()
    {
        return currentChild.Value;
    }
    protected bool Next()
    {
        if(currentChild != children.Last)
        {
            currentChild = currentChild.Next;
            return true;
        }
        return false;
    }

    protected override void End()
    {
        if(currentChild == null)
            return;
        currentChild.Value.Abort();
        currentChild = null;
    }
    public override void SortPiority(ref int piorityCounter)
    {
        base.SortPiority(ref piorityCounter);
        foreach (BT_Node child in children) 
        {
            child.SortPiority(ref piorityCounter);
        }
    }
    public override BT_Node Get()
    {
        if (currentChild == null)
        {
            if (children.Count != 0)
            {
                return children.First.Value;
            }
            else
                return this; 
        }
        return currentChild.Value.Get();
    }
    public override void Init()
    {
        base.Init();
        foreach (BT_Node child in children)
        {
            child.Init();
        }
    }

}
