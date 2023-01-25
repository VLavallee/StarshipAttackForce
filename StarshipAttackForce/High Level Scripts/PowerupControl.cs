using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupControl : MonoBehaviour
{
    [SerializeField] float timeWithoutAPowerup;
    [SerializeField] float powerupThresholdLimit;
    public bool powerupFound;
    [SerializeField] float powerupFoundTime;
    [SerializeField] float powerupFoundThresholdLimit;
    [SerializeField] int powerupChanceLevel;
    [SerializeField] int currentChance;
    int level0Chance = 5;
    int level1Chance = 10;
    int level2Chance = 15;
    int level3Chance = 20;
    int level4Chance = 30;
    int level5Chance = 40;
    int level6Chance = 50;
    int level7Chance = 60;
    int level8Chance = 80;
    int level9Chance = 100;


    private void Update()
    {
        UpdateChance();
        if(!powerupFound)
        {
            timeWithoutAPowerup += Time.deltaTime;
            if(timeWithoutAPowerup > powerupThresholdLimit && powerupChanceLevel < 9)
            {
                powerupChanceLevel++;
                timeWithoutAPowerup = 0;
            }
        }
        if(powerupFound)
        {
            powerupChanceLevel = 0;
            powerupFoundTime += Time.deltaTime;
            if(powerupFoundTime > powerupFoundThresholdLimit)
            {
                powerupFound = false;
                powerupFoundTime = 0;
            }
        }
    }

    public int GetPowerupChance()
    {
        return currentChance;
    }

    public void PowerupFound()
    {
        powerupFound = true;
    }

    private void UpdateChance()
    {
        if(powerupChanceLevel == 0)
        {
            currentChance = level0Chance;
        }
        if (powerupChanceLevel == 1)
        {
            currentChance = level1Chance;
        }
        if (powerupChanceLevel == 2)
        {
            currentChance = level2Chance;
        }
        if (powerupChanceLevel == 3)
        {
            currentChance = level3Chance;
        }
        if (powerupChanceLevel == 4)
        {
            currentChance = level4Chance;
        }
        if (powerupChanceLevel == 5)
        {
            currentChance = level5Chance;
        }
        if (powerupChanceLevel == 6)
        {
            currentChance = level6Chance;
        }
        if (powerupChanceLevel == 7)
        {
            currentChance = level7Chance;
        }
        if (powerupChanceLevel == 8)
        {
            currentChance = level8Chance;
        }
        if (powerupChanceLevel == 9)
        {
            currentChance = level9Chance;
        }
    }
}
