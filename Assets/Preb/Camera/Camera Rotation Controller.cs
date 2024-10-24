using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways] //Để tùy chỉnh độ xa của camera trực tiếp trong scene không cần nhấn play
public class CameraRotationController : MonoBehaviour //Script này chỉ dùng để thực hiện chỉnh xa gần giữa camera và position của cha chứa camera (ngắn gọn là độ xa của camera chiếu vào scene)
{
    [SerializeField] float armLength = 0f; //độ xa của camera
    [SerializeField] Transform child; //transform của camera để điều chỉnh
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        child.position = transform.position - child.forward * armLength; //chỉnh độ xa của camera
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(child.position, transform.position); //vẽ nét nối giữa camera và cha của camera
    }
}
