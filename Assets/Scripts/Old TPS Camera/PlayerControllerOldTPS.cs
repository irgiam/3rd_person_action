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
}
