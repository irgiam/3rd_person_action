using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerOldTPS : MonoBehaviour
{
    public float walkSpeed = 10f;
    public float gravity = 20f;
    public Vector3 moveDirection = Vector3.zero;
    float horizontalMove = 0;
    float verticalMove = 0;
    float currentSpeed, horizontalRotation, verticalRotation;
    public GameObject center;
    CharacterController controller;


    public Transform cam;
    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal");
            verticalMove = Input.GetAxisRaw("Vertical");
        }
        if (horizontalMove!=0 || verticalMove != 0)
        {
            horizontalRotation = horizontalMove;
            verticalRotation = verticalMove;
        }
    }

    private void FixedUpdate()
    {
        //this.transform.Rotate(0, horizontalMove, 0);
        moveDirection = new Vector3(horizontalMove, 0, verticalMove);

        Vector3 faceDirection = new Vector3(horizontalRotation, 0, verticalRotation);
        this.transform.rotation = Quaternion.LookRotation(faceDirection);
        //moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= walkSpeed;

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }


    void HanddleMovementBrackeysMethod() // brackeys method, langsung taro di fixedupdate/update. pake chinemachine->freelook
    {
        Vector3 direction = new Vector3(horizontalMove, 0, verticalMove).normalized; //normalized biar kecepatan konstan saat bergerak diagonal
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            //transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection.normalized);
        }
    }
}
