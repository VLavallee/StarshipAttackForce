using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDB : MonoBehaviour
{
    public const string fireballName = "playerFireball";
    public const string superFireballName = "playerSuperFireball";
    public const string miniBladeName = "playerMiniBlade";
    public const string bladeName = "playerBlade";
    public const string superBladeName = "playerSuperBlade";
    public const string iceBulletName = "playerIceBullet";
    public const string superIceBulletName = "playerSuperIceBullet";
    public const string iceExplosionnName = "playerIceExplosion";
    public const string ultraIceBulletName = "playerUltraIceBullet";
    public const string superPhotonBulletName = "playerSuperPhoton";
    public const string ultraPhotonBulletName = "playerUltraPhoton";
    public const string microShockSphereName = "playerMicroShockSphere";
    public const string shockSphereName = "playerShockSphere";
    public const string superShockSphereName = "playerSuperShockSphere";
    public const string proximityMineName = "playerProximityMine";
    public const string superHomingMissileName = "playerSuperHomingMissile";
    public const string rocketName = "playerRocket";
    public const string prismName = "playerPrismBulletName";
    public const string superPrismName = "playerSuperPrismBulletName";
    //saucer special projectiles
    
    public const string saucerPhotonLeftName = "playerSaucerPhotonLeft";
    public const string saucerPhotonRightName = "playerSaucerPhotonRight";
    public const string saucerBladeLeftName = "playerSaucerBladeLeft";
    public const string saucerBladeRightName = "playerSaucerBladeRight";
    public const string saucerFireballLeftName = "playerSaucerFireballLeft";
    public const string saucerFireballRightName = "playerSaucerFireballRight";
    public const string saucerSuperFireballLeftName = "playerSaucerSuperFireballLeft";
    public const string saucerSuperFireballRightName = "playerSaucerSuperFireballRight";
    public const string saucerIceBulletLeftName = "playerSaucerIceBulletLeft";
    public const string saucerIceBulletRightName = "playerSaucerIceBulletRight";
    public const string saucerSuperIceBulletLeftName = "playerSaucerSuperIceBulletLeft";
    public const string saucerSuperIceBulletRightName = "playerSaucerSuperIceBulletRight";
    public const string saucerShockSphereLeftName = "playerSaucerShockSphereLeft";
    public const string saucerShockSphereRightName = "playerSaucerShockSphereRight";
    public const string saucerSuperShockSphereLeftName = "playerSaucerSuperShockSphereLeft";
    public const string saucerSuperShockSphereRightName = "playerSaucerSuperShockSphereRight";

    public const int ultraPhotonCost = 9;
    public const int superBladeCost = 6;
    public const int ultraIceBulletCost = 6;
    public const int superShockSphereCost = 7;
    public const int proximityMineCost = 5;
    public const int rocketCost = 2;
    public const int homingMissileCost = 5;
    public const int torpedoCost = 9;
    public const int beamCost = 8;
    public const int bladeShieldCost = 5;
    public const int droneCost = 6;
    public const int superFireballCost = 7;
    public const int superGravityBallCost = 10;
    public const int superPrismCost = 8;
    

    public const float playerFireballDamage = 1600;
    public const float playerSuperFireballDamage = 5750;
    public const float playerMiniBladeDamage = 1000;
    public const float playerBladeDamage = 1800;
    public const float playerSuperBladeDamage = 3600;
    public const float playerIceBulletDamage = 1800;
    public const float playerSuperIceBulletDamage = 2500;
    public const float playerIceExplosionDamage = 1200;
    public const float playerUltraIceBulletDamage = 200;
    public const float playerSuperPhotonDamage = 2400;
    public const float playerUltraPhotonDamage = 75000;
    public const float playerMiniShockSphereDamage = 750;
    public const float playerShockSphereDamage = 2000;
    public const float playerSuperShockSphereDamage = 2500;
    public const float playerSuperHomingMissileDamage = 3500;
    public const float playerPrismBulletDamage = 2500;
    public const float playerSuperPrismBulletDamage = 5000;

    public const float playerBeamDamage = 800;
    public const float playerHomingMissileDamage = 50000;
    public const float playerRocketDamage = 35000;
    public const float playerProximityMineDamage = 40000;

    public const float playerMainWeaponDamageMultiplierPerStage = 0.95f;
    public const float playerSecondaryWeaponDamageMultiplierPerStage = 1.15f;

    public const float fireballVelocity = 40f;
    public const float photonVelocity = 45f;
    public const float iceBulletVelocity = 30f;
    public const float bladeVelocity = 30f;
    public const float shockSphereVelocity = 35f;
    public const float homingMissileVelocity = 10f;
    public const float prismBulletVelocity = 25f;


}
