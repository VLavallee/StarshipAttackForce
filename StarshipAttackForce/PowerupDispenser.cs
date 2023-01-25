using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupDispenser : MonoBehaviour
{
    [SerializeField] List<GameObject> currencyCrystal;
    public bool hasBeenDestroyedByPlayer = false;
    [SerializeField] bool isGivingCurrencyOnDeath;
    

    private void OnDestroy()
    {
        if(hasBeenDestroyedByPlayer)
        {
            if(isGivingCurrencyOnDeath)
            {
                var currencyRoll = Random.Range(0, currencyCrystal.Count);
                //Debug.Log(currencyRoll);
                if (currencyRoll == 0)
                {
                    GameObject crystal = Instantiate(currencyCrystal[0], transform.position, Quaternion.identity);
                }
                if (currencyRoll == 1)
                {
                    GameObject crystal = Instantiate(currencyCrystal[1], transform.position, Quaternion.identity);
                }
                if (currencyRoll == 2)
                {
                    GameObject crystal = Instantiate(currencyCrystal[2], transform.position, Quaternion.identity);
                }
                if (currencyRoll == 3)
                {
                    GameObject crystal = Instantiate(currencyCrystal[3], transform.position, Quaternion.identity);
                }
                if (currencyRoll == 4)
                {
                    GameObject crystal = Instantiate(currencyCrystal[4], transform.position, Quaternion.identity);
                }
                if (currencyRoll == 5)
                {
                    GameObject crystal = Instantiate(currencyCrystal[5], transform.position, Quaternion.identity);
                }
                if (currencyRoll == 6)
                {
                    GameObject crystal = Instantiate(currencyCrystal[6], transform.position, Quaternion.identity);
                }
                if (currencyRoll == 7)
                {
                    GameObject crystal = Instantiate(currencyCrystal[7], transform.position, Quaternion.identity);
                }
                if (currencyRoll == 8)
                {
                    GameObject crystal = Instantiate(currencyCrystal[8], transform.position, Quaternion.identity);
                }
                if (currencyRoll == 9)
                {
                    GameObject crystal = Instantiate(currencyCrystal[9], transform.position, Quaternion.identity);
                }
                if (currencyRoll == 10)
                {
                    GameObject crystal = Instantiate(currencyCrystal[10], transform.position, Quaternion.identity);
                }
                if (currencyRoll == 11)
                {
                    GameObject crystal = Instantiate(currencyCrystal[11], transform.position, Quaternion.identity);
                }
                if (currencyRoll == 12)
                {
                    GameObject crystal = Instantiate(currencyCrystal[12], transform.position, Quaternion.identity);
                }
                if (currencyRoll == 13)
                {
                    GameObject crystal = Instantiate(currencyCrystal[13], transform.position, Quaternion.identity);
                }
                if (currencyRoll == 14)
                {
                    GameObject crystal = Instantiate(currencyCrystal[14], transform.position, Quaternion.identity);
                }
            }
        }

        else
        {
            return;
        }
    }
}
