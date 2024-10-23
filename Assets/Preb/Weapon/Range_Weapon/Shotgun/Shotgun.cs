using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : RangeWeapon
{
    [SerializeField] float spreadAngle = 10f;
    [SerializeField] int pellets = 10;
    [SerializeField] LayerMask layerMask;

    bool canShoot = true;

    public override void Attack()
    {
        ShootPellet();
        PlayWeaponAudio();
    }

    private void ShootPellet()
    {
        Vector3 aimDir = aimComponent.GetAimDir().normalized;
        float angleEach = spreadAngle * 2 / pellets;

        for (int i = 0; i < pellets; i++)
        {
            Quaternion calAngle = Quaternion.Euler(0, -spreadAngle + i * angleEach, 0);
            Vector3 pelletDir = calAngle * aimDir;
            Debug.DrawRay(aimComponent.transform.position, pelletDir * 1000, Color.red, 1f);
            GameObject Enemy = aimComponent.GetAimTargetBasedOnDifferentDir(pelletDir);
            bulletFX.transform.rotation = Quaternion.LookRotation(pelletDir); //Cài đặt hiệu ứng
            bulletFX.Emit(bulletFX.emission.GetBurst(0).maxCount); //Bắt đầu hiệu ứng
            if (Enemy != null)
                DamageOnGameObject(Enemy, amount);
        }
    }

    private void OnDrawGizmos()
    {
        

    }
}