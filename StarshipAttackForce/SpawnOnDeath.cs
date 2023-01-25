using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDeath : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;

    private void OnDestroy()
    {
        GameObject obj = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
    }
}
