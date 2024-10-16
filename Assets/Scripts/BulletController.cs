using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public BossController bossController;
    [SerializeField] public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Boss"))
        {
            bossController.Hurt(damage);
        }
        if (!collision.collider.CompareTag("Player") && !collision.collider.CompareTag("ShootPosition"))
        {
            //Debug.Log(collision.collider);
            Destroy(gameObject);
        }
    }
}
