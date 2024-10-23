using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : Enemy
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform lauchPoint;
    

    Vector3 Destination;
    public override void AttackTarget(GameObject target)
    {
        Animator.SetTrigger("attack");
        Destination = target.transform.position;
    }
    public void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab,lauchPoint.position,lauchPoint.rotation);
        Projectile newIProjectile = newProjectile.GetComponent<Projectile>();
        if (newIProjectile != null)
        {
            newIProjectile.Launch(gameObject, Destination);
        }
        else
        {
            Debug.Log("Cant find Iprojectile");
        }
    }
}
