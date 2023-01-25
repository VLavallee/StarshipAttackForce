using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCrystal : MonoBehaviour
{
    Player player;
    [SerializeField] float healthAmount = 25;
    private void Start()
    {
        
        player = FindObjectOfType<Player>();
        FindObjectOfType<PowerupControl>().PowerupFound();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        player.AddHealth(healthAmount);
    }
}
