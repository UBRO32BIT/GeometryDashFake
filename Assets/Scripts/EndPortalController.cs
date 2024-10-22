using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPortalController : MonoBehaviour
{
    public GameController controller;
    [SerializeField] public bool goNextLevel;
    [SerializeField] public bool goEndGame;
    [SerializeField] public string levelName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (goNextLevel)
            {
                SceneController.instance.NextLevel();
            }
            else if (goEndGame)
            {
                controller.EndGame();
            }
            else SceneController.instance.LoadScene(levelName);
        }
    }
}
