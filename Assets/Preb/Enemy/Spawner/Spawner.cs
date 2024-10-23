using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VFXspecs
{
    public ParticleSystem VFX;
    public float size;
}
public class Spawner : Enemy
{
    [SerializeField] VFXspecs[] DeadVFX;
    protected override void Dead()
    {
        foreach (VFXspecs spec in DeadVFX)
        {
            ParticleSystem particleSystem = Instantiate(spec.VFX);
            particleSystem.transform.position = transform.position;
            particleSystem.transform.localScale = Vector3.one * spec.size;
        }
    }
}
