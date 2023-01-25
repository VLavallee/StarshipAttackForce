using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] List<GameObject> enemyPrefabList;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = .75f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 25f;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }
    public GameObject GetRandomEnemyPrefab()
    {
        var randomEnemyRoll = Random.Range(0, enemyPrefabList.Count);
        return enemyPrefabList[randomEnemyRoll];
    }
    public List<Transform> GetWaypoints() 
    {
        var waveWaypoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }
    public float GetTimeBetweenSpawns() {return timeBetweenSpawns;}
    public float GetSpawnRandomFactor() {return spawnRandomFactor;}
    public int GetNumberOfEnemies() {return numberOfEnemies;}
    public float GetMoveSpeed() {return moveSpeed;}

}
