using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behavior_Tree : MonoBehaviour
{
    BehaviorTreeInterface treeInterface;
    BT_Node root_Node;
    BlackBoard board = new BlackBoard();
    public BlackBoard Board
    {

    get { return board; } 
    }

    void Start()
    {
        treeInterface = GetComponent<BehaviorTreeInterface>();
        ConstructTree(out root_Node);
        SortTree();
    }
    public BehaviorTreeInterface GetBehaviorTreeInterface() 
    {
        return treeInterface;
    }

    private void SortTree()
    {
        int piorityCounter = 0;
        root_Node.Init();
        root_Node.SortPiority(ref piorityCounter);
    }

    protected abstract void ConstructTree( out BT_Node root_Node);
    

    // Update is called once per frame
    void Update()
    {
        root_Node.UpdateNode();
       
    }

    public void AbortLowerThan(int piority)
    {
        BT_Node currentNode = root_Node.Get();
        if (currentNode.GetPiority() > piority)
        {
            root_Node.Abort();
        }
    }
}
