using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    [Header("Ship Stats")]
    [SerializeField] int hullStat;
    [SerializeField] int shieldStat;
    [SerializeField] int cargoStat;
    [SerializeField] int weaponIStat;
    [SerializeField] int weaponIIStat;
    [SerializeField] int specialStat;

    [Header("Ship Stat Upgrade Prices")]
    [SerializeField]
    int[] hullStatUpgradePrices, shieldStatUpgradePrices, cargoStatUpgradePrices, weaponIStatUpgradePrices, weaponIIStatUpgradePrices,
        specialStatUpgradePrices;

    public int GetHullStat()
    {
        return hullStat;
    }
    public void SetHullStat(int stat)
    {
        hullStat = stat;
    }
    public int GetShieldStat()
    {
        return shieldStat;
    }
    public void SetShieldStat(int stat)
    {
        shieldStat = stat;
    }
    public int GetCargoStat()
    {
        return cargoStat;
    }
    public void SetCargoStat(int stat)
    {
        cargoStat = stat;
    }
    public int GetWeaponIStat()
    {
        return weaponIStat;
    }
    public void SetWeaponIStat(int stat)
    {
        weaponIStat = stat;
    }
    public int GetWeaponIIStat()
    {
        return weaponIIStat;
    }
    public void SetWeaponIIStat(int stat)
    {
        weaponIIStat = stat;
    }
    public int GetSpecialStat()
    {
        return specialStat;
    }
    public void SetSpecialStat(int stat)
    {
        specialStat = stat;
    }

    //SHIP STAT UPGRADE PRICES//

    public int GetHullStatUpgradePrice()
    {
        return hullStatUpgradePrices[GetHullStat()];
    }

    public int GetShieldStatUpgradePrice()
    {
        return shieldStatUpgradePrices[GetShieldStat()];
    }
}
