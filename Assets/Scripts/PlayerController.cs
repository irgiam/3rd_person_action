using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    Animator thisaAnimator;
    public LayerMask groundLayer;
    public float moveSpeed = 2f;
    public float jumpHeight = 2f;
    public float gravity = 20f;
    public float turnSmoothTime = 0.1f;
    public Transform cam;

    float horizontalMove = 0;
    float verticalMove = 0;
    float turnSmoothVelocity;
    bool isGrounded;
    Vector3 velocity = new Vector3();

    float attackRate = 2f;
    float nextAttackTime = 0f;
    public Transform attackPoint;
    public float attackDamage = 30f;

    private void Awake()
    {
        controller = this.GetComponent<CharacterController>();
        thisaAnimator = this.GetComponent<Animator>();
    }

    private void Start()
    {
        SetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        isGrounded = Physics.CheckSphere(this.transform.position, 0.1f, groundLayer);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 6f;
        } else
        {
            moveSpeed = 2f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            TakeDamage(20f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isGrounded && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 2f / attackRate;
        }

        float movement = Mathf.Clamp((Mathf.Abs(horizontalMove) + Mathf.Abs(verticalMove)), 0, 1);
        thisaAnimator.SetFloat("Speed", (movement * moveSpeed));
        thisaAnimator.SetBool("IsJumping", !isGrounded);
        if (Time.time >= nextAttackTime)
        {
            HandleMovement();
        }
        
        GravityHandler();
    }


    void HandleMovement()
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

    void GravityHandler()
    {
        if (isGrounded && velocity.y<0)
        {
            velocity = Vector3.zero;
            //Debug.Log("Grounded!!");
        } else
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        velocity.y += Mathf.Sqrt(jumpHeight * 3.0f * gravity);
    }

    public LayerMask enemyLayer;
    void Attack()
    {
        thisaAnimator.SetTrigger("Attack");
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, 0.8f, enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            //Debug.Log("we hit" + enemy.name);
            enemy.GetComponent<EnemyController>().TakenDamage(attackDamage);
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(attackPoint.position, 0.8f);
    }


    public Slider healthBar;
    public float maxHealth = 100f;
    float health;

    void SetHealth()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;
    }
}
