using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingComponent: MonoBehaviour
{
    [SerializeField] Transform PatrolParent;
    int currentPatrolIndex = -1;

    public bool GetNextPatrolPoint(out Vector3 point)
    {
        point = Vector3.zero;
        if(PatrolParent.childCount == 0) 
            return false;
        currentPatrolIndex = (currentPatrolIndex + 1) % PatrolParent.childCount;
        point = PatrolParent.GetChild(currentPatrolIndex).transform.position;
        return true;
    }
}
