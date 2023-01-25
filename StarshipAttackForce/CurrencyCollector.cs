using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Power Core"))
        {
            //FindObjectOfType<GameSession>().AddToPowerCoresCollected();
            Destroy(collision.gameObject);
        }
    }
}
