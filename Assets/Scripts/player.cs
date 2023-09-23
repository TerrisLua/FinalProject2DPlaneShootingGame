using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public GameObject playerExplosion;
    public playerHealth playerhealthbar;
    public float speed = 5f;
    public float padding = 0.8f;
    public float maxX;
    public float maxY;
    public float minX; 
    public float minY;
    public GameObject damageEffect;
    public Coincount coinCountScript;
    public GameController gameController;
    public AudioSource audioSource;
    public AudioClip damageAudio;
    public AudioClip explosionAudio;
    public AudioClip coinAudio;
    public GameObject poweredUpPrefab;
    public AudioClip powerUpAudio;
    public GameObject powerUpAnimation;




    public float health = 20f;
    float barFillAmount = 1f;
    float damage = 0;

    // Start is called before the first frame update
    void Start()
    {
        FindBoundaries();
        damage = barFillAmount / health;
    }

    void FindBoundaries()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0,0,0)).x + padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Move the plane based on the input.
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f) * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + movement;

        float newXpos = Mathf.Clamp(newPosition.x, minX, maxX);
        float newYpos = Mathf.Clamp(newPosition.y, minY, maxY);
        newPosition = new Vector3(newXpos, newYpos, 0f);

        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Powerup")
        {
            Destroy(collision.gameObject);

            if (poweredUpPrefab != null)
            {
                audioSource.PlayOneShot(powerUpAudio, 0.5f);
                GameObject powerUp = Instantiate(powerUpAnimation, transform.position, Quaternion.identity);
                Destroy(powerUp, 0.4f);
                GameObject newPlayer = Instantiate(poweredUpPrefab, transform.position, transform.rotation);
                // Optionally, you can transfer any state from the old player to the new one here
                newPlayer.GetComponent<player>().playerhealthbar = this.playerhealthbar;
                newPlayer.GetComponent<player>().coinCountScript = this.coinCountScript;
                newPlayer.GetComponent<player>().gameController = this.gameController;
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("poweredUpPrefab is not set");
            }
        }

        if (collision.tag == "EnemyBullet")
        {
            audioSource.PlayOneShot(damageAudio, 0.5f);
            DamagePlayerHealthBar();
            Destroy(collision.gameObject);
            GameObject damageVfx = Instantiate(damageEffect, collision.transform.position, Quaternion.identity);
            Destroy(damageVfx, 0.04f);
            if (health <= 0)
            {
                AudioSource.PlayClipAtPoint(explosionAudio, Camera.main.transform.position, 0.5f);
                gameController.GameOver();
                Destroy(gameObject);
                GameObject enemyExplode = Instantiate(playerExplosion, transform.position, Quaternion.identity);
                Destroy(enemyExplode, 0.4f);
            }   
        }
        if (collision.tag == "coin")
        {
            audioSource.PlayOneShot(coinAudio, 0.5f);
            Destroy(collision.gameObject);
            coinCountScript.addCount();
        }

        if (collision.tag == "EnemyShip" || collision.tag == "Asteroid")
        {
            AudioSource.PlayClipAtPoint(explosionAudio, Camera.main.transform.position, 0.5f);
            Destroy(gameObject);
            GameObject enemyExplode = Instantiate(playerExplosion, transform.position, Quaternion.identity);
            Destroy(enemyExplode, 0.4f);
            gameController.DelayGameOver();
        }
    }
    void DamagePlayerHealthBar()
    {
        if(health > 0)
        {
            health -= 1;
            barFillAmount = barFillAmount - damage;
            playerhealthbar.SetAmount(barFillAmount);
        }
    }
}
