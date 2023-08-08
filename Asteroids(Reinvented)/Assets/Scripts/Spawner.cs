using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject player;
    public GameObject asteroidPrefab;
    public List<GameObject> asteroids;
    public float spawnCollisionCheckradius;
    public float asteroidSpawnCooldown;
    public float currentAsteroidSpawnCooldown;
    private bool playerIsAlive;

    private void Start()
    {
        asteroidSpawnCooldown = 5;
        currentAsteroidSpawnCooldown = asteroidSpawnCooldown;
        spawnCollisionCheckradius = 5;
        playerIsAlive = true;
        StartCoroutine(SpawnAsteroidTimer());
    }

    public IEnumerator SpawnAsteroidTimer()
    {
        while (playerIsAlive)
        {
            float _randomX;
            float _randomY;
            Vector3 _randomPosition;

            for (int i = 0; i < 1; i++)
            {
                _randomX = Random.Range(-20, 20);
                _randomY = Random.Range(-16, 16);
                _randomPosition = new Vector3(_randomX, _randomY, 0);

                if (!Physics2D.OverlapCircle(_randomPosition, spawnCollisionCheckradius))
                {
                    GameObject asteroid = Instantiate(asteroidPrefab, _randomPosition, Quaternion.identity);
                    asteroid.transform.parent = transform;

                    asteroids.Add(asteroid);
                }
                else
                {
                    i--;
                }
            }
            yield return new WaitForSeconds(asteroidSpawnCooldown);
        }
    }

    public void SpawnAsteroid(int count)
    {
        float _randomX;
        float _randomY;
        Vector3 _randomPosition;

        for (int i = 0; i < count; i++)
        {
            _randomX = Random.Range(-20, 20);
            _randomY = Random.Range(-16, 16);
            _randomPosition = new Vector3(_randomX, _randomY, 0);

            if (!Physics2D.OverlapCircle(_randomPosition, spawnCollisionCheckradius))
            {
                GameObject asteroid = Instantiate(asteroidPrefab, _randomPosition, Quaternion.identity);
                asteroid.transform.parent = transform;

                asteroids.Add(asteroid);
            }
            else
            {
                i--;
            }
        }

        currentAsteroidSpawnCooldown = asteroidSpawnCooldown;
    }
}
