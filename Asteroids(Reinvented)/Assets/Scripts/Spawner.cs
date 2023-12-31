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
    public int asteroidSpawnCount;

    private void Start()
    {
        asteroidSpawnCooldown = 5;
        currentAsteroidSpawnCooldown = asteroidSpawnCooldown;
        spawnCollisionCheckradius = 5;
        InvokeRepeating("SpawnAsteroid", 3.0f, 3.0f);
    }

    public void SpawnAsteroid() //int count
    {
        if (player.GetComponent<PlayerScript>()._playerHP <= 0)
        {
            return;
        }

        float _randomX;
        float _randomY;
        Vector3 _randomPosition;

        for (int i = 0; i < asteroidSpawnCount; i++) //count
        {
            _randomX = Random.Range(-20, 20);
            _randomY = Random.Range(-16, 16);
            _randomPosition = new Vector3(_randomX, _randomY, 0);

            GameObject asteroid = Instantiate(asteroidPrefab, _randomPosition, Quaternion.identity);
            //asteroid.inner = false;
            asteroid.transform.parent = transform;

            asteroids.Add(asteroid);
            /*
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
            */
        }

        currentAsteroidSpawnCooldown = asteroidSpawnCooldown;
    }
}
