using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] public float respawnTime;
    public AudioSource playerDeathSound;
    public AudioSource checkpointSound;
    public GameObject gameOverScreen;

    Vector2 checkpointPos;
    Rigidbody2D playerRb;
    bool isDied;
    bool gameHasEnded;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        gameHasEnded = false;
        isDied = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        checkpointPos = transform.position;
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        if (checkpointPos != pos)
        {
            checkpointPos = pos;
            checkpointSound.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("Died");
            Die();
        }
    }

    public void Die()
    {
        if (!isDied)
        {
            playerDeathSound.Play();
            StartCoroutine(Respawn(respawnTime));
            isDied = true;
        }
    }

    IEnumerator Respawn(float duration)
    {
        playerRb.simulated = false;
        //Reset player velocity
        playerRb.velocity = new Vector2(0, 0);
        //Make player disappear
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;
        transform.localScale = new Vector3(1, 1, 1);
        playerRb.simulated = true;
        isDied = false;
    }

    public void EndGame()
    {
        if (!gameHasEnded)
        {
            Debug.Log("GAME OVER");
            gameOverScreen.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
