using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject[] asteroids;  // Add an array for asteroid prefabs
    public GameObject bossEnemyPrefab;
    public float enemyRespawnTime = 2f;
    public float asteroidRespawnTime = 4f;  // Add respawn time for asteroids
    public int enemySpawnCount = 5;
    public GameController gameController;
    private bool lastEnemySpawned = false;
    private bool bossSpawned = false;
    public int enemiesDestroyed = 0;


    void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    void Update()
    {
        if (lastEnemySpawned && FindObjectOfType<enemy>() == null)
        {
            StartCoroutine(gameController.LevelComplete());
        }
    }

    IEnumerator EnemySpawner()
    {    
        for (int i = 0; i < enemySpawnCount + 1; i++)
        {
            yield return new WaitForSeconds(enemyRespawnTime);

            if (enemiesDestroyed >= enemySpawnCount && !bossSpawned && bossEnemyPrefab != null)
            {
                SpawnBoss();
                bossSpawned = true;
            }
            else
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(1f); // Wait for 1 second before spawning asteroid
            SpawnAsteroid();  // Call your method to spawn an asteroid
        }

        lastEnemySpawned = true;
    }


    void SpawnEnemy()
    {
        int randomValue = Random.Range(0, enemy.Length);
        int randomXValue = Random.Range(-3, 3);
        Instantiate(enemy[randomValue], new Vector2(randomXValue, transform.position.y), Quaternion.identity);
    }

    void SpawnAsteroid()
    {
        int randomValue = Random.Range(0, asteroids.Length);
        int randomXValue = Random.Range(-6, 6);
        Instantiate(asteroids[randomValue], new Vector2(randomXValue, transform.position.y), Quaternion.identity);
    }

    void SpawnBoss()
    {
        int randomXValue = Random.Range(-3, 3);
        Instantiate(bossEnemyPrefab, new Vector2(randomXValue, transform.position.y), Quaternion.identity);
    }

    public void EnemyDestroyed()
    {
        enemiesDestroyed++;
    }
}
