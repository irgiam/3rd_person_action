using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    CharacterController controller;
    public float maxHealth = 100;
    public float health;
    public float gravity = 20f;

    public GameObject canvas;
    public Slider healthBar;
    public Transform cam;
    public LayerMask groundLayer;
    Vector3 velocity = new Vector3();
    bool isGrounded;

    private void Awake()
    {
        controller = this.GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetHealth();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(this.transform.position, 0.1f, groundLayer);
        GravityHandler();
    }

    private void LateUpdate()
    {
        canvas.transform.LookAt(canvas.transform.position + cam.forward);
    }

    void GravityHandler()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity = Vector3.zero;
            //Debug.Log("Grounded!!");
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    public void TakenDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            this.gameObject.SetActive(false);
        }
        healthBar.value = health;
    }

    void SetHealth()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }
}
