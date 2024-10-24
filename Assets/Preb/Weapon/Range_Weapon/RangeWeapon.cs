using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
    protected AimComponent aimComponent; //Lấy aimCompoent
    [SerializeField] protected  ParticleSystem bulletFX; //Hiệu ứng bắn súng
    protected override void Start()
    {
        base.Start();
        aimComponent = GetComponent<AimComponent>(); //Gán aimCompent
    }
    public override void Attack() //Hàm tấn công
    {
        GameObject target = aimComponent.GetAimTargetBaseOnAimDir(out Vector3 aimDir); //Lấy ra vật đang bị ngắm tới
        DamageOnGameObject(target, amount); //Gây sát thương lên vật bị ngắm tới
        bulletFX.transform.rotation = Quaternion.LookRotation(aimDir); //Cài đặt hiệu ứng
        bulletFX.Emit(bulletFX.emission.GetBurst(0).maxCount); //Bắt đầu hiệu ứng
        PlayWeaponAudio();
    }


}
