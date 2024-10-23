
using System;
using UnityEngine;

public class BlackBoard_Decorator : Decorator
{
    public enum Condiction
    {
        KeyExist,
        KeyNotExist
    }

    public enum NotifyRules
    {
        RunCondictionChanged,
        KeyValueChanged
    }

    public enum NotifyAbort
    {
        None,
        Self,
        Lower,
        Both
    }
    string key;
    Behavior_Tree tree;
    UnityEngine.Object value;
    Condiction condiction;
    NotifyRules notifyRules;
    NotifyAbort notifyAbort;
    public BlackBoard_Decorator(Behavior_Tree tree, BT_Node child, string key, Condiction codittion, NotifyRules notifyRules, NotifyAbort abort) : base(child)
    {
        this.tree = tree;
        this.condiction = codittion;
        this.notifyRules = notifyRules;
        this.notifyAbort = abort;
        this.key = key;
    }
    protected override NodeResult Execute()
    {
        BlackBoard blackBoard = tree.Board;
        if (blackBoard == null)
            return NodeResult.Failure;
        tree.Board.onBlackBoardValueChange -= CheckNotify;
        blackBoard.onBlackBoardValueChange += CheckNotify;
        if(CheckRunCondition())
        {
            return NodeResult.Inprogress;
        }
        else
            return NodeResult.Failure;
    }

    protected override NodeResult Update()
    {
        return GetChild().UpdateNode();
    }
    private bool CheckRunCondition()
    {
        bool exist = tree.Board.GetData(key, out value);
        switch(condiction)
        {
            case Condiction.KeyExist:
                return exist;
            case Condiction.KeyNotExist: 
                return !exist;
        }
        return false;
           
    }

    private void CheckNotify(string key, object value)
    {
        if(this.key != key)
        {
            return;
        }
        if (notifyRules == NotifyRules.RunCondictionChanged)
        {
            bool preExist = this.value != null;
            bool curExist = value != null;
            if (preExist != curExist)
            {
                Notify();
            }
        }
        else if (notifyRules == NotifyRules.KeyValueChanged) 
        {
            if(this.value != value)
            {
                Notify();
            }
        }
    }
    private void Notify()
    {
        switch(notifyAbort)
        {
            case NotifyAbort.None:
                break;
            case NotifyAbort.Self:
                AbortSelf();
                break;
            case NotifyAbort.Lower:
                AbortLower();
                break;
            case NotifyAbort.Both:
                AbortBoth();
                break;
        }
    }

    private void AbortBoth()
    {
        AbortSelf();
        AbortLower();
    }

    private void AbortLower()
    {
        tree.AbortLowerThan(GetPiority());
    }

    private void AbortSelf()
    {
        Abort();
    }
    protected override void End()
    {
        
        GetChild().Abort();
        base.End();
    }
}
