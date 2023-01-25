using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public List<GameObject> asteroidPrefabs;
    [SerializeField] float spawnTime, spawnTimeLimit;
    [SerializeField] float spawnTimeMin, spawnTimeMax;
    [SerializeField] float spawnTimeLimitMin, spawnTimeLimitMax;
    [SerializeField] bool asteroidSpawned;
    private Vector2 screenBounds;
    [SerializeField] float screenBoundMultiplier = 2;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    private void SpawnAsteroid()
    {
        var randomNum = Random.Range(0, asteroidPrefabs.Count);
        GameObject asteroid = Instantiate(asteroidPrefabs[randomNum]);
        asteroid.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y * screenBoundMultiplier);
        asteroid.transform.parent = transform;
        ResetSpawnTime();
    }

    private void Update()
    {
        spawnTime += Time.deltaTime;
        if(spawnTime >= spawnTimeLimit)
        {
            SpawnAsteroid();
            spawnTime = 0;
        }
    }
    private void ResetSpawnTime()
    {
        asteroidSpawned = false;
        spawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
        spawnTimeLimit = Random.Range(spawnTimeLimitMin, spawnTimeLimitMax);
    }
}
