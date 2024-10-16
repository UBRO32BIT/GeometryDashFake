using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectfile : MonoBehaviour
{
    [SerializeField][Range(0, 30f)] public float speed;
    [SerializeField][Range(0, 5f)] public float spread;
    //bullet 
    public GameObject bullet;
    public Transform launchPosition;
    public AudioSource gunshotSound;

    public void Shoot(InputAction.CallbackContext callback)
    {
        if (callback.started && callback.control.name == "leftButton")
        {
            Debug.Log("Shoot");
            GameObject currentBullet = Instantiate(bullet, launchPosition.position, Quaternion.identity);
            Debug.Log(currentBullet.transform.position);
            gunshotSound.time = 0.3f;
            gunshotSound.Play();
            Rigidbody2D rb = currentBullet.GetComponent<Rigidbody2D>();
            CircleCollider2D bulletCollider = currentBullet.GetComponent<CircleCollider2D>();
            //Ignore collision with player
            if (bulletCollider != null)
            {
                // Get the player object by tag
                GameObject player = GameObject.FindWithTag("Player");

                if (player != null)
                {
                    Collider2D playerCollider = player.GetComponent<Collider2D>();

                    // Ignore collision between bullet and player if player exists
                    if (playerCollider != null)
                    {
                        Physics2D.IgnoreCollision(bulletCollider, playerCollider);
                    }
                }

                // Get the player object by tag
                GameObject shootPosition = GameObject.FindWithTag("ShootPosition");

                if (player != null)
                {
                    Collider2D shootPositionCollider = shootPosition.GetComponent<Collider2D>();

                    // Ignore collision between bullet and player if player exists
                    if (shootPositionCollider != null)
                    {
                        Physics2D.IgnoreCollision(bulletCollider, shootPositionCollider);
                    }
                }
            }
            // Calculate random spread
            float spreadAngle = Random.Range(-spread, spread);  // Random angle between -spread and +spread
            Vector2 shootingDirection = launchPosition.right;
            Vector2 spreadDirection = Quaternion.Euler(0, 0, spreadAngle) * shootingDirection; // Rotate by spreadAngle
            rb.velocity = spreadDirection * speed;
            //Debug.Log(launchPosition.position);
        }
    }
}
