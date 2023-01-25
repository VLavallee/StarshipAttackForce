using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipStatUIElements : MonoBehaviour
{
    public Sprite[] hull;
    public Sprite[] shield;
    public Sprite[] cargo;
    public Sprite[] weaponI;
    public Sprite[] weaponII;
    public Sprite[] special;

    [SerializeField] Image hullStatImage;
    [SerializeField] Image shieldStatImage;
    [SerializeField] Image cargoStatImage;
    [SerializeField] Image weaponIStatImage;
    [SerializeField] Image weaponIIStatImage;
    [SerializeField] Image specialStatImage;

    Sprite hullStatSprite;
    Sprite shieldStatSprite;
    Sprite cargoStatSprite;
    Sprite weaponIStatSprite;
    Sprite weaponIIStatSprite;
    Sprite specialStatSprite;

    ShipStats shipStats;

    private void Start()
    {
        shipStats = FindObjectOfType<ShipStats>();
        SetShipStatImages();
    }

    void SetShipStatImages()
    {
        SetHullImage();
        SetShieldStatImage();
        SetCargoStatImage();
        SetWeaponIStatImage();
        SetWeaponIIStatImage();
        SetSpecialStatImage();
    }

    void SetHullImage()
    {
        int hullStatIndex = shipStats.GetHullStat() - 1;
        hullStatSprite = hull[hullStatIndex];
        hullStatImage.sprite = hullStatSprite;
    }

    void SetShieldStatImage()
    {
        int shieldStatIndex = shipStats.GetShieldStat() - 1;
        shieldStatSprite = shield[shieldStatIndex];
        shieldStatImage.sprite = shieldStatSprite;
    }

    void SetCargoStatImage()
    {
        int cargoStatIndex = shipStats.GetCargoStat();
        cargoStatSprite = cargo[cargoStatIndex];
        cargoStatImage.sprite = cargoStatSprite;
    }

    void SetWeaponIStatImage()
    {
        int weaponIStatIndex = shipStats.GetWeaponIStat() - 1;
        weaponIStatSprite = weaponI[weaponIStatIndex];
        weaponIStatImage.sprite = weaponIStatSprite;
    }
    void SetWeaponIIStatImage()
    {
        int weaponIIStatIndex = shipStats.GetWeaponIIStat();
        weaponIIStatSprite = weaponII[weaponIIStatIndex];
        weaponIIStatImage.sprite = weaponIIStatSprite;
    }

    void SetSpecialStatImage()
    {
        int specialStatIndex = shipStats.GetSpecialStat();
        specialStatSprite = special[specialStatIndex];
        specialStatImage.sprite = specialStatSprite;
    }
}
