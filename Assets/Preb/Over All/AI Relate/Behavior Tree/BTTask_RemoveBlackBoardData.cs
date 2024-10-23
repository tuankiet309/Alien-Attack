using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTask_RemoveBlackBoardData : BT_Node
{
    Behavior_Tree tree;
    string keyToRemove;
    public BTTask_RemoveBlackBoardData(Behavior_Tree tree,string keyToRemove)
    {
        this.tree = tree;
        this.keyToRemove = keyToRemove;
    }
    protected override NodeResult Execute()
    {
        if (tree != null && tree.Board!=null)
        {
            tree.Board.RemoveData(keyToRemove);
            return NodeResult.Success;
        }
        else
        {
            return NodeResult.Failure;
        }
    }
}
