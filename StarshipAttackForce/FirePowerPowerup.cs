using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePowerPowerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //FindObjectOfType<Player>().IncreaseFirepower();
            Destroy(gameObject);
        }
    }
}
