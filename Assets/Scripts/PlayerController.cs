using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float walkSpeed = 2f;
    public float turnSmoothTime = 0.1f;
    public Transform cam;

    float horizontalMove = 0;
    float verticalMove = 0;
    float turnSmoothVelocity;

    private void Awake()
    {
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(horizontalMove, 0, verticalMove).normalized; //normalized biar kecepatan konstan saat bergerak diagonal
        if  (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            //transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized);
        }
    }
}
