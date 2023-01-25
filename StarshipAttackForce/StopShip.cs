using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopShip : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<Player>().GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
