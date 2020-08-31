﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    Animator thisaAnimator;
    public LayerMask groundLayer;
    public float moveSpeed = 2f;
    public float gravity = 20f;
    public float turnSmoothTime = 0.1f;
    public Transform cam;

    float horizontalMove = 0;
    float verticalMove = 0;
    float turnSmoothVelocity;

    private void Awake()
    {
        controller = this.GetComponent<CharacterController>();
        thisaAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 6f;
        } else
        {
            moveSpeed = 2f;
        }

        float movement = Mathf.Clamp((Mathf.Abs(horizontalMove) + Mathf.Abs(verticalMove)), 0, 1);
        thisaAnimator.SetFloat("Speed", (movement * moveSpeed));
        HanddleMovement();
        GravityHanddler();
    }

    private void FixedUpdate()
    {
        
    }

    void HanddleMovement()
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
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }

    void GravityHanddler()
    {
        Vector3 velocity = new Vector3();
        if (Physics.CheckSphere(this.transform.position, 0.1f, groundLayer))
        {
            velocity = Vector3.zero;
            //Debug.Log("Grounded!!");
        } else
        {
            velocity.y -= gravity * Time.deltaTime;
            controller.Move(velocity);
        }
    }

}
