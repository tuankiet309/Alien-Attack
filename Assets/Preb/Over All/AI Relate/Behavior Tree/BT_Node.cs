
using UnityEngine;
public enum NodeResult
{
    Success,
    Failure,
    Inprogress
}
public abstract class BT_Node 
{
    public NodeResult UpdateNode()
    {
        //One off
        if(!started)
        {
            started = true;
            NodeResult executeResult = Execute();
            if(executeResult !=NodeResult.Inprogress )
            {
                EndNode();
                return executeResult;
            }
        }
        //time base
        NodeResult updateResult = Update();
        if(updateResult !=NodeResult.Inprogress)
        {
            EndNode();
        }
        return updateResult;
    }
    protected virtual NodeResult Execute()
    {
        // one time function, use to implement in child class, return bellow is not important
        return NodeResult.Success;
    }

    protected virtual NodeResult Update()
    {
        //time based function, use to implement in child class, return bellow is not important
        return NodeResult.Success;
    }

    private void EndNode()
    {
        started= false;
        End();
    }

    protected virtual void End()
    {
        // clean up  
    }
    public void Abort()
    {
        EndNode();
    }
    bool started = false;
    int piority;
    public int GetPiority()
    {
        return piority;
    }

    public virtual void Init()
    { }
    public virtual BT_Node Get()
    {
        return this;
    }

    public virtual void SortPiority(ref int piorityCounter)
    {
       piority = piorityCounter++;
        Debug.Log($"{this} has piority of{piority}");
    }
}
