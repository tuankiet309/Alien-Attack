using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class BTTask_MoveToLastLocationGroup : BTTask_Group
{
    [SerializeField] float acceptableDistance;
    public BTTask_MoveToLastLocationGroup(Behavior_Tree tree, float acceptableDistance = 2) : base(tree)
    {
        this.acceptableDistance = acceptableDistance;
    }

    protected override void ConstructTree(out BT_Node root)
    {
        Sequencer checlLastSeenLocation = new Sequencer();
        BTTask_MoveToALocation moveToLastSeenLocation = new BTTask_MoveToALocation(tree, "LastSeenLocation", acceptableDistance);
        BTTask_Wait waitAtLastSeenLocation = new BTTask_Wait(2f);
        BTTask_RemoveBlackBoardData bTTask_RemoveBlackBoardData = new BTTask_RemoveBlackBoardData(tree, "LastSeenLocation");
        checlLastSeenLocation.AddChild(moveToLastSeenLocation);
        checlLastSeenLocation.AddChild(waitAtLastSeenLocation);
        checlLastSeenLocation.AddChild(bTTask_RemoveBlackBoardData);

        BlackBoard_Decorator checkLastSeenLocationDecorator = new BlackBoard_Decorator(tree, checlLastSeenLocation, "LastSeenLocation",
                                                                                       BlackBoard_Decorator.Condiction.KeyExist,
                                                                                       BlackBoard_Decorator.NotifyRules.RunCondictionChanged,
                                                                                       BlackBoard_Decorator.NotifyAbort.None);
        root = checkLastSeenLocationDecorator;
    }
   
}
