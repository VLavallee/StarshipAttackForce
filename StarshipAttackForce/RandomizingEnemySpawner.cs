using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizingEnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> fullWaveList;
    //[SerializeField] List<WaveConfig> removableFullPathList;
    //public List<WaveConfig> scrambledPathList;
    [SerializeField] WaveConfig currentWave;
    [SerializeField] int startingWave;
    [SerializeField] bool isActive, looping, containsBoss, isLowEnemyTrigger, isNoEnemyTrigger;
    
    [SerializeField] float bossSpawnDelay, delayValueInSec;
    //[SerializeField] List<GameObject> fullBossList;
    //[SerializeField] List<GameObject> removableFullBossList;
    public List<GameObject> scrambledBossList;
    [SerializeField] GameObject currentBoss;
    [SerializeField] Transform bossSpawnLocation;

    // at start the scrambled List will be filled and randomized. This list will be pulled from for all the waves. When the list reaches the end it will be deleted and 
    // another scrambled list will be created. End funcionality is not set up.

    private void Start()
    {
        //scrambledPathList = fullPathList;
        StartCoroutine(BeginSpawn());
    }

    IEnumerator BeginSpawn()
    {
        if (isActive)
        {
            do
            {
                yield return StartCoroutine(SpawnAllWaves());
                
                FindObjectOfType<RandomizingBossSpawner>().SpawnRandomBoss();
            }
            while (looping);
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < fullWaveList.Count; waveIndex++)
        {
            var currentWave = fullWaveList[waveIndex];

            //  new way 
            //  Should check the enemies on screen have been destroyed before continuing
            if (isLowEnemyTrigger)
            {
                yield return new WaitUntil(LowEnemiesPresent);
            }
            if (isNoEnemyTrigger)
            {
                yield return new WaitUntil(NoEnemiesPresent);
            }
            yield return new WaitForSeconds(delayValueInSec);

            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetRandomEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    public void StartSpawning()
    {
        isActive = true;
        StartCoroutine(TurnOnSpawn());
    }

    IEnumerator TurnOnSpawn()
    {
        if (isActive)
        {
            do
            {
                yield return StartCoroutine(SpawnAllWaves());
            }
            while (looping);
        }
    }

    public bool NoEnemiesPresent()
    {
        return FindObjectOfType<EnemyTracker>().noActiveEnemiesInScene;
    }

    public bool LowEnemiesPresent()
    {
        return FindObjectOfType<EnemyTracker>().lowEnemiesInScene;
    }
}
