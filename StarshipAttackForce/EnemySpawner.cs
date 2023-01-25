using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    [SerializeField] bool enemiesInactive = false;
    public bool IsActive = false;
    [SerializeField] bool isLowEnemyTrigger, isNoEnemyTrigger;
    [SerializeField] float delayValueInSec;

    [Header("Boss Details")]
    [SerializeField] bool containsBoss;
    [SerializeField] GameObject bossPrefab;
    [SerializeField] Transform bossSpawnPosition;
    [SerializeField] float bossSpawnDelay;

    
    IEnumerator Start()
    {
        if(IsActive)
        {
            do
            {
                yield return StartCoroutine(SpawnAllWaves());

                // will run this code after all waves have been spawned
                if (!looping && containsBoss)
                {
                    yield return new WaitForSeconds(bossSpawnDelay);
                    GameObject boss = Instantiate(bossPrefab, transform.position, Quaternion.identity);
                }
            }
            while (looping);
        }
    }
    private IEnumerator SpawnAllWaves()
    {
        for(int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];

            //  new way 
            //  Should check the enemies on screen have been destroyed before continuing
            if(isLowEnemyTrigger)
            {
                yield return new WaitUntil(LowEnemiesPresent);
            }
            if(isNoEnemyTrigger)
            {
                yield return new WaitUntil(NoEnemiesPresent);
            }
            yield return new WaitForSeconds(delayValueInSec);
            
            //  old way
            //  yield return new WaitForSeconds(waveIndex / 2);
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for(int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
           var newEnemy = Instantiate(waveConfig.GetRandomEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
    public void StartSpawning()
    {
        IsActive = true;
        StartCoroutine(TurnOnSpawn());
    }
    IEnumerator TurnOnSpawn()
    {
        if (IsActive)
        {
            do
            {
                yield return StartCoroutine(SpawnAllWaves());

                // will run this code after all waves have been spawned
                if (!looping && containsBoss)
                {
                    yield return new WaitForSeconds(bossSpawnDelay);
                    GameObject boss = Instantiate(bossPrefab, transform.position, Quaternion.identity);
                }
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
