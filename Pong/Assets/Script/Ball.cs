using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float initialVelocity = 4f;
    [SerializeField] private float velocityMultiplayer = 1.1f;

    private Rigidbody2D ballRb;
    private AudioManager audioManager;

    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>();
        Launch();
    }

    private void Launch()
    {
        float xVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        float yVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        ballRb.velocity = new Vector2(xVelocity, yVelocity) * initialVelocity;
    }

    [SerializeField] private float wallFrequency = 660f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            ballRb.velocity *= velocityMultiplayer;

            if (collision.gameObject.name == "Paddle1")
            {
                audioManager.PlayFrequency(440); 
            }
            else if (collision.gameObject.name == "Paddle2")
            {
                audioManager.PlayFrequency(550);
            }
        }
        else if (collision.gameObject.CompareTag("TopWall") || collision.gameObject.CompareTag("BottomWall"))
        {
            audioManager.PlayFrequency(wallFrequency); 
        }
    }

    [SerializeField] private float goalFrequency = 880f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal1"))
        {
            GameManager.Instance.Paddle2Scored();
            GameManager.Instance.Restart();
            Launch();
            audioManager.PlayFrequency(goalFrequency);
        }
        else if (collision.gameObject.CompareTag("Goal2"))
        {
            GameManager.Instance.Paddle1Scored();
            GameManager.Instance.Restart();
            Launch();
            audioManager.PlayFrequency(goalFrequency);
        }
    }
}
