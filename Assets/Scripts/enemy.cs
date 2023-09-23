using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Transform []gunPoint;
    public GameObject enemyBullet;
    public float enemyBulletSpawnTime = 0.5f;
    public GameObject enemyExplosion;  
    public GameObject damageEffect;
    public Healthbar healthbar;
    public GameObject coinPrefab;
    public float health = 2f;
    public float speed = 2f;
    public AudioClip bulletAudio;
    public AudioClip damageAudio;
    public AudioClip explosionAudio;
    public AudioSource audioSource;
    public player PlayerScript;


    float barSize = 1f;
    float damage = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shooting());
        damage = barSize / health;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime );
    }

    void OnDestroy()
    {
        FindObjectOfType<Spawner>().EnemyDestroyed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "PlayerBullet")
        {
            audioSource.PlayOneShot(damageAudio);
            DamageHealth();
            Destroy(collision.gameObject);
            GameObject damageVfx = Instantiate(damageEffect,collision.transform.position, Quaternion.identity);
            Destroy(damageVfx, 0.04f);
            if (health <= 0)
            {
                AudioSource.PlayClipAtPoint(explosionAudio, Camera.main.transform.position, 0.5f);
                Instantiate(coinPrefab,transform.position,Quaternion.identity);
                Destroy(gameObject);
                GameObject enemyExplode = Instantiate(enemyExplosion, transform.position, Quaternion.identity);
                Destroy(enemyExplode, 0.4f);
            }
        }
        if (collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(explosionAudio, Camera.main.transform.position, 0.5f);
            Destroy(gameObject);
            GameObject enemyExplode = Instantiate(enemyExplosion, transform.position, Quaternion.identity);
            Destroy(enemyExplode, 0.4f);
        }
    }

    void DamageHealth()
    {
        if (health> 0)
        {
            health -= 1;
            barSize = barSize - damage;
            healthbar.setSize(barSize);
        }
    }

    void Fire()
    {
        for (int i = 0; i < gunPoint.Length; i++) 
        {
            Instantiate(enemyBullet, gunPoint[i].position, Quaternion.identity);
        }
    }

    IEnumerator Shooting()
    {
        while(true)
        {
            audioSource.PlayOneShot(bulletAudio, 0.5f);
            yield return new WaitForSeconds(enemyBulletSpawnTime);
            Fire();
        }  
    }


}
