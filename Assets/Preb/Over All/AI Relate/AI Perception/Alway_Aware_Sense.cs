using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alway_Aware_Sense : Sense_Component
{
    [SerializeField] float awareDistance = 2f;
    protected override bool IsStimuliSensable(Perception_Stimuli perception_Stimuli) //Luôn kiểm tra xem có ai xung quanh ko
    {
        return Vector3.Distance(transform.position,perception_Stimuli.transform.position) <= awareDistance;
    }

    protected override void DrawDebug()
    {
        base.DrawDebug();
        Gizmos.DrawWireSphere(transform.position + Vector3.up,awareDistance);
    }
   
}
