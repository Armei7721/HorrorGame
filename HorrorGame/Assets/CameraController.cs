using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float lookSpeed = 2.0f;
    public float moveSpeed = 2.0f;

    private float rotationX = 0;
    private float rotationY = 0;

    void Update()
    {
        // 마우스 회전
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -90, 90);
        rotationY += Input.GetAxis("Mouse X") * lookSpeed;
        //rotationY = Mathf.Clamp(rotationY, -180, 180);
        
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);


    }
}
