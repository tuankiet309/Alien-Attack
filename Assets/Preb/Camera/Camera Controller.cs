using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform FollowTrans; //Truyền vào transform của player để camera đi theo
    [SerializeField] float rotateSpeed = 0f; //tốc độ quay
    private void LateUpdate()
    {
        transform.position = FollowTrans.position; //Đi theo player
    }
    public void AddYawnInput(float amt)
    {
        transform.Rotate(Vector3.up, amt * Time.deltaTime * rotateSpeed); //Quay camera theo tham số truyền vào theo joystick trong script Player
    }
}
