using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;

    public GameObject canvas;
    public Slider healthBar;
    public Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        SetHealth();
    }

    private void LateUpdate()
    {
        canvas.transform.LookAt(canvas.transform.position + cam.forward);
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
