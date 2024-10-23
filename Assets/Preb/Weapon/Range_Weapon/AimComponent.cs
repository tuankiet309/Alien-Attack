 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour
{
    [SerializeField] Transform Muzzle; //Lấy vị trí hướng ngắm aka lòng súng
    [SerializeField] float aimRange = 1000f; //Tầm xa
    [SerializeField] LayerMask aimLayerMasked; //filter ác vật có thể đc ngắm
    
    

    public GameObject GetAimTargetBaseOnAimDir(out Vector3 aimDir) //Lấy vật đc ngắm// aimDir dể trả ra hướng ngắm
    {
        Vector3 aimPos = Muzzle.position;  //Lấy vị trí ngắm
        aimDir = GetAimDir(); //Lấy hướng ngắm

        if(Physics.Raycast(aimPos,aimDir, out RaycastHit hitInfo,aimRange, aimLayerMasked)) //Cast tại vị trí ngắm 1 tia theo hướng ngắm với tầm xa và filter vật thể
        {
            return hitInfo.collider.gameObject; //Nếu có vật như vậy trả về gameObject của vật đó
        }
        return null; //Còn không thì trả null
    }

    public GameObject GetAimTargetBasedOnDifferentDir(Vector3 inDir)
    {
        Vector3 aimPos = Muzzle.position;  

        if (Physics.Raycast(aimPos, inDir, out RaycastHit hitInfo, aimRange, aimLayerMasked)) 
        {
            return hitInfo.collider.gameObject; 
        }
        return null; 
    }
    public Vector3 GetAimDir() //Lấy hướng ngắm
    {
        Vector3 muzzleDir = Muzzle.forward; //lấy hướng của lòng súng
        return new Vector3(muzzleDir.x,0,muzzleDir.z); //lấy hướng ngắm theo mặt phẳng xz, không lấy y để tránh súng bắt giật lên trên trời do animation
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Muzzle.position, Muzzle.position + GetAimDir() * aimRange); //Dễ nhìn
    }
}
