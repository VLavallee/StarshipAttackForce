using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousSpawn : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject enemyA, enemyB, enemyC;
    [SerializeField] GameObject[] asteroids;
    [SerializeField] float timer, timeLimit;

    
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > timeLimit)
        {
            GameObject enemy = Instantiate(enemyA, spawnPoints[Random.Range(0, spawnPoints.Length)]);
            timer = 0;
        }
    }
}
