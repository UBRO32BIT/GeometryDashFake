using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem movementParticle;
    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity;
    [Range(0f, 0.2f)]
    [SerializeField] float dustFormationPeriod;
    [SerializeField] Rigidbody2D playerRb;
    float counter;
    bool isOnGround;

    [SerializeField] ParticleSystem fallParticle;
    [SerializeField] ParticleSystem touchParticle;

    private void Start()
    {
        touchParticle.transform.parent = null;
    }
    private void Update()
    {
        counter += Time.deltaTime;
        if (isOnGround && Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity)
        {
            if (counter > dustFormationPeriod)
            {
                movementParticle.Play();
                counter = 0;
            }
        }
    }

    public void PlayTouchParticle(Vector2 position)
    {
        touchParticle.transform.position = position;
        touchParticle.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isOnGround = true;
            fallParticle.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}
