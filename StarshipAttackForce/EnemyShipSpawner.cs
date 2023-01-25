using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    public GameObject[] enemyShipPrefab;
    [SerializeField]
    float spawnTime, spawnTimeLimit, spawnTimeLimit2, spawnTimeLimit3, spawnTimeLimit4, spawnTimeLimit5,
        spawnTimeLimit6, spawnTimeLimit7;
    private Vector2 screenBounds;
    [SerializeField] List<Transform> yPlaneTransforms;
    [SerializeField] float padding;
    EnemyVariables enemyVariables;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        enemyVariables = FindObjectOfType<EnemyVariables>();
    }

    private void SpawnRandomEnemyShip()
    {
        // old way with screenbounds for y axis
        //var randomRoll = Random.Range(0, enemyShipPrefab.Length);
        //GameObject enemyShip = Instantiate(enemyShipPrefab[randomRoll]);
        //enemyShip.transform.position = new Vector2(Random.Range(-screenBounds.x + padding, screenBounds.x - padding), screenBounds.y * 2);
        //enemyShip.transform.parent = transform;

        // new way with transform list for y axis
        var randomRoll = Random.Range(0, enemyShipPrefab.Length);
        GameObject enemyShip = Instantiate(enemyShipPrefab[randomRoll]);
       
        enemyShip.transform.position = new Vector2(Random.Range(-screenBounds.x + padding, screenBounds.x - padding), 
            Random.Range(yPlaneTransforms[0].position.y, yPlaneTransforms[yPlaneTransforms.Count - 1].position.y));
        enemyShip.transform.parent = transform;
    }

    private void Update()
    {
        if(enemyVariables.GetSpawnLevel() == 1)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime >= spawnTimeLimit)
            {
                SpawnRandomEnemyShip();
                spawnTime = 0;
            }
        }
        else if (enemyVariables.GetSpawnLevel() == 2)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime >= spawnTimeLimit2)
            {
                SpawnRandomEnemyShip();
                spawnTime = 0;
            }
        }
        else if (enemyVariables.GetSpawnLevel() == 3)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime >= spawnTimeLimit3)
            {
                SpawnRandomEnemyShip();
                spawnTime = 0;
            }
        }
        else if (enemyVariables.GetSpawnLevel() == 4)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime >= spawnTimeLimit4)
            {
                SpawnRandomEnemyShip();
                spawnTime = 0;
            }
        }
        else if (enemyVariables.GetSpawnLevel() == 5)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime >= spawnTimeLimit5)
            {
                SpawnRandomEnemyShip();
                spawnTime = 0;
            }
        }
        else if (enemyVariables.GetSpawnLevel() == 6)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime >= spawnTimeLimit6)
            {
                SpawnRandomEnemyShip();
                spawnTime = 0;
            }
        }
        else if (enemyVariables.GetSpawnLevel() == 7)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime >= spawnTimeLimit7)
            {
                SpawnRandomEnemyShip();
                spawnTime = 0;
            }
        }

    }
}
