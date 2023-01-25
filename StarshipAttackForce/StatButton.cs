using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI upgradeButtonText;
    [SerializeField] TextMeshProUGUI buyButtonPriceText;
    [SerializeField] TextMeshProUGUI dropDownInfoText;

    [TextArea(2, 5)] [SerializeField] string hullDescription, shieldDescription, cargoDescription, weaponIDescription, weaponIIDescription,
        specialDescription;

    string hull = "hull";
    string shield = "shield";
    string cargo = "cargo";
    string weaponI = "weapon i";
    string weaponII = "weapon ii";
    string special = "special";
    string upgrade = "upgrade ";
    string buyFor = "buy for ";

    // SHIP STATS //
    public void LoadHullStatUpgradeInfo()
    {
        dropDownInfoText.text = hullDescription;
        upgradeButtonText.text = upgrade + hull + "?";
        buyButtonPriceText.text = buyFor + HullUpgradePrice().ToString();
    }

    public void LoadShieldStatUpgradeInfo()
    {
        dropDownInfoText.text = shieldDescription;
        upgradeButtonText.text = upgrade + shield + "?";
        buyButtonPriceText.text = buyFor + ShieldUpgradePrice().ToString();
    }

    public void LoadCargoStatUpgradeInfo()
    {
        dropDownInfoText.text = cargoDescription;
        upgradeButtonText.text = upgrade + cargo + "?";
    }

    public void LoadWeaponIStatUpgradeInfo()
    {
        dropDownInfoText.text = weaponIDescription;
        upgradeButtonText.text = upgrade + weaponI + "?";
    }

    public void LoadWeaponIIStatUpgradeInfo()
    {
        dropDownInfoText.text = weaponIIDescription;
        upgradeButtonText.text = upgrade + weaponII + "?";
    }

    public void LoadSpecialStatUpgradeInfo()
    {
        dropDownInfoText.text = specialDescription;
        upgradeButtonText.text = upgrade + special + "?";
    }

    // SHIP STAT UPGRADE PRICE RETRIEVAL

    public int HullUpgradePrice()
    {
        return FindObjectOfType<ShipStats>().GetHullStatUpgradePrice();
    }

    public int ShieldUpgradePrice()
    {
        return FindObjectOfType<ShipStats>().GetShieldStatUpgradePrice();
    }
}
