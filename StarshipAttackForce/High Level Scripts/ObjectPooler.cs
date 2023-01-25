using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    #region Singleton

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
    public List<Pool> playerWeaponPools, playerEffectPools, enemyWeaponPools, enemyEffectPools, enemyShipPools;
    public Dictionary<string, Queue<GameObject>> playerWeaponPoolDictionary, playerEffectPoolDictionary, enemyWeaponPoolDictionary,
        enemyEffectPoolDictionary, enemyShipPoolDictionary;
    public Transform playerWeaponTransformParent, playerEffectTransformParent, enemyWeaponTransformParent, enemyEffectTransformParent, enemyShipTransformParent;
    void Start()
    {
        playerWeaponPoolDictionary = new Dictionary<string, Queue<GameObject>>();
        playerEffectPoolDictionary = new Dictionary<string, Queue<GameObject>>();
        enemyWeaponPoolDictionary = new Dictionary<string, Queue<GameObject>>();
        enemyEffectPoolDictionary = new Dictionary<string, Queue<GameObject>>();
        enemyShipPoolDictionary = new Dictionary<string, Queue<GameObject>>();


        //playerWeaponPools
        foreach (Pool pool in playerWeaponPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.parent = playerWeaponTransformParent;
                objectPool.Enqueue(obj);
            }

            playerWeaponPoolDictionary.Add(pool.tag, objectPool);
        }

        //playerEffectPools
        foreach (Pool pool in playerEffectPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.parent = playerEffectTransformParent;
                objectPool.Enqueue(obj);
            }

            playerEffectPoolDictionary.Add(pool.tag, objectPool);
        }

        //enemyWeaponPools
        foreach (Pool pool in enemyWeaponPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.parent = enemyWeaponTransformParent;
                objectPool.Enqueue(obj);
            }

            enemyWeaponPoolDictionary.Add(pool.tag, objectPool);
        }

        //enemyEffectPools
        foreach (Pool pool in enemyEffectPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.parent = enemyEffectTransformParent;
                objectPool.Enqueue(obj);
            }

            enemyEffectPoolDictionary.Add(pool.tag, objectPool);
        }

        //enemyShipPools
        foreach (Pool pool in enemyShipPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.parent = enemyShipTransformParent;
                objectPool.Enqueue(obj);
            }

            enemyShipPoolDictionary.Add(pool.tag, objectPool);
        }



    }
    
        

    public GameObject SpawnFromPoolPlayerWeapon(string tag, Vector2 position, Quaternion rotation)
    {
        if(!playerWeaponPoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist!");
            return null;
        }

        GameObject objectToSpawn = playerWeaponPoolDictionary[tag].Dequeue();
        objectToSpawn.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        playerWeaponPoolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
    public GameObject SpawnFromPoolPlayerEffect(string tag, Vector2 position, Quaternion rotation)
    {
        if (!playerEffectPoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist!");
            return null;
        }

        GameObject objectToSpawn = playerEffectPoolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        playerEffectPoolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
    public GameObject SpawnFromPoolEnemyWeapon(string tag, Vector2 position, Quaternion rotation)
    {
        if (!enemyWeaponPoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist!");
            return null;
        }

        GameObject objectToSpawn = enemyWeaponPoolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        enemyWeaponPoolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
    public GameObject SpawnFromPoolEnemyEffect(string tag, Vector2 position, Quaternion rotation)
    {
        if (!enemyEffectPoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist!");
            return null;
        }

        GameObject objectToSpawn = enemyEffectPoolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        enemyEffectPoolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
    public GameObject SpawnFromPoolEnemyShip(string tag, Vector2 position, Quaternion rotation)
    {
        if (!enemyShipPoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist!");
            return null;
        }

        GameObject objectToSpawn = enemyShipPoolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        enemyShipPoolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
