using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] float turnSpeed = 8f;
    public float RotateTowards(Vector3 aimDir)
    {
        float currentTurnSpeed = 0f; //Tốc độ quay hiện tại
        if (aimDir.magnitude != 0) //Nêu aimDir khác không, thực hiện quay nhân vật 
        {
            //Các câu lệnh dưới phục vụ việc quay nhật vật 1 cách smooth mà không bị diễn ra tức khắc (đùng cái dừng animation)
            Quaternion preRotate = transform.rotation; //Hướng trước khi quay
            float rotationSpeedAlpha = turnSpeed * Time.deltaTime; // tính toán tốc độ quay ko phụ thuộc vào framerate
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDir, Vector3.up), rotationSpeedAlpha); // Quay từ từ theo trục y để quay nhân vật xung 
            Quaternion currentRotate = transform.rotation;
            float Dir = Vector3.Dot(aimDir, transform.right) > 0 ? 1 : -1;
            float rotationDelta = Quaternion.Angle(preRotate, currentRotate) * Dir;
            currentTurnSpeed = rotationDelta / Time.deltaTime;
        }
        return currentTurnSpeed;
    }
}
