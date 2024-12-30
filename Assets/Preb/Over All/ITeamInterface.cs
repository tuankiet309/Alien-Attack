using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETeamRalation
{
    Friendly,
    Enemy,
    Neutral
}
public interface ITeamInterface
{
    public int GetTeamID()
    {
        return -1;
    }
    public ETeamRalation GetRalationToward(GameObject other)
    {
        ITeamInterface otherTeamInterface = other.GetComponent<ITeamInterface>();
        if (otherTeamInterface == null)
            return ETeamRalation.Neutral;
        if(otherTeamInterface.GetTeamID() == GetTeamID())
        {
            return ETeamRalation.Friendly;
        }
        else if(otherTeamInterface.GetTeamID()==-1)
            return ETeamRalation.Neutral;
        return ETeamRalation.Enemy;
    }

}
