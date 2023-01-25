using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariables : MonoBehaviour
{
    [SerializeField] float startEnemyMoveSpeed, enemyMoveSpeed, enemyMoveSpeedModifier;
    [SerializeField] float hitPointsModifier, hitPointsModifier1, hitPointsModifier2, hitPointsModifier3, hitPointsModifier4, hitPointsModifier5,
        hitPointsModifier6, hitPointsModifier7;
    [Header("Time Thresholds")]
    [SerializeField] float modifierThreshold1, modifierThreshold2, modifierThreshold3, modifierThreshold4, modifierThreshold5, modifierThreshold6,
        modifierThreshold7;

    [SerializeField] float startAsteroidMoveSpeed, asteroidMoveSpeed, asteroidMoveSpeedModifier;
    [SerializeField] float timeSinceLevelStart, timer, timeLimit;
    
    [SerializeField] int spawnLevel;

    [SerializeField] bool secondAsteroidSpawnerActivated, thirdAsteroidSpawnerActivated;
    [SerializeField] GameObject secondAsteroidSpawner, thirdAsteroidSpawner;

    private void Start()
    {
        timeSinceLevelStart = 0;
        spawnLevel = 1;
    }
    private void Update()
    {
        timeSinceLevelStart += Time.deltaTime;
        if(timeSinceLevelStart > modifierThreshold1 && timeSinceLevelStart < modifierThreshold2)
        {
            hitPointsModifier = hitPointsModifier1;
            spawnLevel = 2;
        }
        else if(timeSinceLevelStart > modifierThreshold2 && timeSinceLevelStart < modifierThreshold3)
        {
            hitPointsModifier = hitPointsModifier2;
            spawnLevel = 3;
            
        }
        else if (timeSinceLevelStart > modifierThreshold3 && timeSinceLevelStart < modifierThreshold4)
        {
            hitPointsModifier = hitPointsModifier3;
            spawnLevel = 4;
        }
        else if (timeSinceLevelStart > modifierThreshold4 && timeSinceLevelStart < modifierThreshold5)
        {
            hitPointsModifier = hitPointsModifier4;
            spawnLevel = 5;
        }
        else if (timeSinceLevelStart > modifierThreshold5 && timeSinceLevelStart < modifierThreshold6)
        {
            hitPointsModifier = hitPointsModifier5;
            spawnLevel = 6;
        }
        else if (timeSinceLevelStart > modifierThreshold6 && timeSinceLevelStart < modifierThreshold7)
        {
            hitPointsModifier = hitPointsModifier6;
            spawnLevel = 7;
        }
        
        timer += Time.deltaTime;
        if(timer > timeLimit)
        {
            timer = 0;
            IncrementMoveSpeed();
        }
    }
    public int GetSpawnLevel()
    {
        return spawnLevel;
    }

    private void IncrementMoveSpeed()
    {
        enemyMoveSpeed += enemyMoveSpeedModifier;
        asteroidMoveSpeed += asteroidMoveSpeedModifier;
        
    }

    public float GetEnemyMoveSpeed()
    {
        return enemyMoveSpeed;
    }

    public float GetEnemyMoveSpeedModifier()
    {
        return enemyMoveSpeedModifier;
    }

    public float GetAsteroidMoveSpeed()
    {
        return asteroidMoveSpeed;
    }

    public float GetHitPointsModifier()
    {
        return hitPointsModifier;
    }
    

}
