using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerOldTPS : MonoBehaviour
{
    public float rotationSpeed = 10;
    public Transform target, player;
    float mouseX, mouseY;

    private void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        CamControl();
    }

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 40);

        transform.LookAt(target);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        } else
        {
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            //player.rotation = Quaternion.Euler(0, mouseX, 0);
        }
        
    }
}
