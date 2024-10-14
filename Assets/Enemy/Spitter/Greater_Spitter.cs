using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greater_Spitter : Enemy
{
    [SerializeField] GameObject projectilePrefab; // Use GameObject for serialization
    [SerializeField] Transform launchPoint;

    Vector3 originalDestination;
    float angle = 40f;

    public override void AttackTarget(GameObject target)
    {
        Animator.SetTrigger("attack");
        originalDestination = target.transform.position;
    }

    public void Shoot()
    {
        // Calculate the direction vector from launchPoint to originalDestination
        Vector3 direction = (originalDestination - launchPoint.position).normalized;

        // Calculate the destinations for C and D
        Vector3 cDestination = CalculatePointAtAngle(launchPoint.position, direction, angle);
        Vector3 dDestination = CalculatePointAtAngle(launchPoint.position, direction, -angle);

        // List of destinations
        List<Vector3> destinations = new List<Vector3> { cDestination, dDestination, originalDestination };

        // Create and launch projectiles
        foreach (Vector3 destination in destinations)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, launchPoint.position, Quaternion.LookRotation(destination - launchPoint.position));
            Projectile newProjectile = projectileInstance.GetComponent<Projectile>();

            if (newProjectile != null)
            {
                newProjectile.Launch(this.gameObject, destination);
            }
            else
            {
                Debug.LogError("The instantiated projectile does not implement IProjectile.");
            }
        }
    }

    private Vector3 CalculatePointAtAngle(Vector3 origin, Vector3 direction, float angle)
    {
        // Rotate the direction vector by the angle around the up axis
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        Vector3 rotatedDirection = rotation * direction;

        // Calculate the new point based on the distance to originalDestination
        float distance = Vector3.Distance(originalDestination, launchPoint.position);
        return origin + rotatedDirection * distance;
    }
}
