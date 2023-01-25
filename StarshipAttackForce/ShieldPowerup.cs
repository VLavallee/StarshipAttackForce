using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : MonoBehaviour
{
    [SerializeField] GameObject DCShield, DCUltShield, BomberShield, SaucerShield, UltBomberShield, UltSaucerShield;
    [SerializeField] Vector3 EXT_Transform_Offset;
    private GameObject playerShip;
    

    private void Start()
    {
        FindObjectOfType<PowerupControl>().PowerupFound();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerShip = FindObjectOfType<Player>().gameObject;
            if(FindObjectOfType<Player>().isDC1000)
            {
                if(FindObjectOfType<Player>().shieldIsActive == false)
                {
                    GameObject theShield = Instantiate(DCShield, playerShip.transform.position, Quaternion.identity);
                    theShield.transform.parent = playerShip.transform;
                }
                else if (FindObjectOfType<Player>().shieldIsActive == true)
                {
                    FindObjectOfType<Player>().ResetShield();
                }
            }
            if (FindObjectOfType<Player>().isEXT9)
            {
                if (FindObjectOfType<Player>().shieldIsActive == false)
                {
                    GameObject theShield = Instantiate(BomberShield, playerShip.transform.position + EXT_Transform_Offset, Quaternion.identity);
                    theShield.transform.parent = playerShip.transform;
                }
                else if (FindObjectOfType<Player>().shieldIsActive == true)
                {
                    FindObjectOfType<Player>().ResetShield();
                }
            }
            if (FindObjectOfType<Player>().isSaucer)
            {
                if (FindObjectOfType<Player>().shieldIsActive == false)
                {
                    GameObject theShield = Instantiate(SaucerShield, playerShip.transform.position, Quaternion.identity);
                    theShield.transform.parent = playerShip.transform;
                }
                else if (FindObjectOfType<Player>().shieldIsActive == true)
                {
                    FindObjectOfType<Player>().ResetShield();
                }
            }
            if (FindObjectOfType<Player>().isUltDC)
            {
                if (FindObjectOfType<Player>().shieldIsActive == false)
                {
                    GameObject theShield = Instantiate(DCUltShield, playerShip.transform.position, Quaternion.identity);
                    theShield.transform.parent = playerShip.transform;
                }
                else if (FindObjectOfType<Player>().shieldIsActive == true)
                {
                    FindObjectOfType<Player>().ResetShield();
                }
            }
            if (FindObjectOfType<Player>().isUltBomber)
            {
                if (FindObjectOfType<Player>().shieldIsActive == false)
                {
                    GameObject theShield = Instantiate(UltBomberShield, playerShip.transform.position, Quaternion.identity);
                    theShield.transform.parent = playerShip.transform;
                }
                else if (FindObjectOfType<Player>().shieldIsActive == true)
                {
                    FindObjectOfType<Player>().ResetShield();
                }
            }
            if (FindObjectOfType<Player>().isUltSaucer)
            {
                if (FindObjectOfType<Player>().shieldIsActive == false)
                {
                    GameObject theShield = Instantiate(UltSaucerShield, playerShip.transform.position, Quaternion.identity);
                    theShield.transform.parent = playerShip.transform;
                }
                else if (FindObjectOfType<Player>().shieldIsActive == true)
                {
                    FindObjectOfType<Player>().ResetShield();
                }
            }

            FindObjectOfType<Player>().ActivateShield();
            FindObjectOfType<BlueBorder>().ActivateBlueBorder();
            Destroy(gameObject);
        }
    }
}
