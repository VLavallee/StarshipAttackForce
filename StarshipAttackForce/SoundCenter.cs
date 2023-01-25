using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCenter : MonoBehaviour
{
    [SerializeField]
    float playerFiringTime, playerFiringLimit, playerDamageTime, playerDamageLimit,
        enemyAttackTime, enemyAttackLimit, enemyDamageTime, enemyDamageLimit, soundRefTime, soundRefLimit;
    [SerializeField] bool playerHasFired, playerWasDamaged, enemyHasAttacked, enemyWasDamaged, refSoundHasPlayed;

    public void SetPlayerFiringInterval(float seconds)
    {
        playerFiringLimit = seconds;
    }
    public bool CheckPlayerFiringSoundCooldown()
    {
        if (playerHasFired)
        {
            return false;
        }
        else
        {
            playerHasFired = true;
            return true;
        }
    }

    public bool CheckRefSoundCooldown()
    {
        if(refSoundHasPlayed)
        {
            return false;
        }
        else
        {
            refSoundHasPlayed = true;
            return true;
        }
    }

    public bool CheckEnemyFiringSoundCooldown()
    {
        return true;
    }

    public bool CheckEnemyDamageSoundCooldown()
    {
        if(enemyWasDamaged)
        {
            return false;
        }
        else
        {
            enemyWasDamaged = true;
            return true;
        }
    }

    public void Update()
    {
        if(playerHasFired)
        {
            playerFiringTime += Time.deltaTime;
            if(playerFiringTime > playerFiringLimit)
            {
                playerHasFired = false;
                playerFiringTime = 0;
            }
        }

        if(enemyWasDamaged)
        {
            enemyDamageTime += Time.deltaTime;
            if(enemyDamageTime > enemyDamageLimit)
            {
                enemyWasDamaged = false;
                enemyDamageTime = 0;
            }
        }
        if(refSoundHasPlayed)
        {
            soundRefTime += Time.deltaTime;
            if(soundRefTime > soundRefLimit)
            {
                refSoundHasPlayed = false;
                soundRefTime = 0;
            }
        }
    }


}


