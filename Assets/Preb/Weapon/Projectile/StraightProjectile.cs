using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : Projectile,  IExplodeAble
{
    [SerializeField] float flySpeed = 20f;
    [SerializeField] float lifeTime = 3f;
  
    private void Start()
    {
        rb.useGravity = false;
    }
    public override void Explode()
    {
        Vector3 spawnPos = transform.position;
        Instantiate(explosionVFX, spawnPos, Quaternion.identity);
        explodeTrigger.SetDamageEnable(true);
        StartCoroutine(StartDestroy()); // Make sure these logic can be aplied before destroy 
    }
    IEnumerator StartDestroy()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    public override void Launch(GameObject Instigator, Vector3 Destination)
    {
        base.Launch(Instigator, Destination);
        Vector3 flyingDirection = Destination - Instigator.transform.position ;
        rb.velocity = rb.velocity.normalized;
        rb.velocity = flySpeed * flyingDirection.normalized;
        StartCoroutine(ExplodeAfterAWhile());

    }
    IEnumerator ExplodeAfterAWhile()
    {
        yield return new WaitForSeconds(lifeTime);
        Explode();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

}
