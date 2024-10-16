using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortalController : MonoBehaviour
{
    public GameController controller;
    private void Awake()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            controller.EndGame();
        }
    }
}
