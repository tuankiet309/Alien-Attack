using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveProjectile : Projectile
{
    [SerializeField] float flyHight = 10f;
    public override void Launch(GameObject Instigator, Vector3 Destination)
    {
        
        base.Launch(Instigator, Destination);


        float gravity = Physics2D.gravity.magnitude;
        float halfFlightTime = Mathf.Sqrt( (flyHight * 2) / gravity );
        Vector3 DestinationVec = Destination - transform.position;
        DestinationVec.y = 0;
        float horizontalDistance = DestinationVec.magnitude;

        float upSpeed = halfFlightTime * gravity;
        float fwdSpeed = horizontalDistance / (2*halfFlightTime);

        Vector3 flightVel = Vector3.up * upSpeed + DestinationVec.normalized * fwdSpeed;
        rb.AddForce( flightVel,ForceMode.VelocityChange );

    }
    public override void Explode()
    {
        base.Explode();
    }

    protected override void OnTriggerEnter(Collider other)
    {
       base.OnTriggerEnter(other);
    }
}
