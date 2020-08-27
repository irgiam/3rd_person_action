using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 10f;
    public float gravity = 20f;
    Vector3 moveDirection = Vector3.zero;
    float horizontalMove = 0;
    float verticalMove = 0;
    float currentSpeed;
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
    }

    private void FixedUpdate()
    {
        this.transform.Rotate(0, horizontalMove, 0);
        moveDirection = new Vector3(0, 0, verticalMove);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= walkSpeed;

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        //transform.RotateAround(center.transform.position, Vector3.up*verticalMove, walkSpeed * Time.deltaTime); //jgn lupa verticalmove diluar grounded
    }
}
