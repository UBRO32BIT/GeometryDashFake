using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossController : MonoBehaviour
{
    [SerializeField]
    public int maxHealth;
    [SerializeField]
    HealthBar healthBar;
    private int currentHealth;
    bool isAlive;
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D bossCollider;
    private int direction = -1;

    // Variables for shooting
    public GameObject bullet;
    public Transform bulletPos;
    private float timer;

    // Variables for running to player
    private float runTimer = 0f;
    private bool isRunningToPlayer = false;
    private Transform player;
    private float runDuration = 2f;
    private float timeBetweenRuns = 30f;
    private float runSpeed = 5f;

    private GameController gameController;

    private void Awake()
    {
        isAlive = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bossCollider = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }
    private void Update()
    {
        if (!isAlive)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer > 2f)
        {
            Shoot();
            timer = 0;
        }

        // Handle running towards player logic
        runTimer += Time.deltaTime;
        if (isRunningToPlayer)
        {
            // Run for 2 seconds
            if (runTimer <= runDuration)
            {
                RunToPlayer();
            }
            else
            {
                // Stop running after 2 seconds
                isRunningToPlayer = false;
                anim.SetBool("isRun", false);
                runTimer = 0;
            }
        }
        else if (runTimer >= timeBetweenRuns)
        {
            // Start running to player every 30 seconds
            isRunningToPlayer = true;
            anim.SetBool("isRun", true);
            runTimer = 0;
        }
    }
    public void Shoot()
    {
        anim.SetTrigger("attack");
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
    public void Attack()
    {

    }
    private void RunToPlayer()
    {
        // Calculate direction toward the player
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(directionToPlayer.x * runSpeed, rb.velocity.y);
    }
    public void Hurt(int damage)
    {
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        anim.SetTrigger("hurt");
        if (direction == 1)
            rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
        else
            rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //Hide health bar when die
        healthBar.gameObject.SetActive(false);
        anim.SetTrigger("die");
        isAlive = false;

        // Disable the collider to prevent further collisions
        bossCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.CompareTag("Player"))
        {
            gameController.Die();
        }
    }
}
