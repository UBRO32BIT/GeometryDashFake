using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float speed;
    public float spread;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        // Calculate random spread
        float spreadAngle = Random.Range(-spread, spread);  // Random angle between -spread and +spread
        Vector2 spreadDirection = Quaternion.Euler(0, 0, spreadAngle) * direction; // Rotate by spreadAngle
        rb.velocity = new Vector2(spreadDirection.x, spreadDirection.y).normalized * speed;
        Debug.Log($"{direction.x} {direction.y}");
        float rot = Mathf.Atan2(-direction.y, -direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }
}
