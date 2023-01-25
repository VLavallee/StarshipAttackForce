using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour 
{
    public float damage;
    [SerializeField] bool isDisablingOnHit = true;
    [SerializeField] bool isDisablingParentOnHit = false;
    [SerializeField] GameObject parentObj;
    [SerializeField] bool isFixedDamage = false;
    [SerializeField] bool isSetToDestroy = false;
    [SerializeField] bool isAddingEffectOnHit = false;
    [SerializeField] bool isAddingProjectileOnHit = false;
    [SerializeField] bool isAddingVelocity;
    [SerializeField] float optionalVelocity;
    [SerializeField] bool enemyEffect, playerEffect;
    [SerializeField] bool enemyProjectile, playerProjectile;
    [SerializeField] string effectName;
    [SerializeField] string projectileName;
    [SerializeField] bool isEnemyWeapon;
    [SerializeField]
    bool isPlayerMiniBlade, isPlayerBlade, isPlayerSuperBlade, isPlayerFireball, isPlayerSuperFireball, isPlayerIceBullet, isPlayerSuperIceBullet, isIceExplosion, isPlayerUltraIceBullet,
        isPlayerSuperPhoton, isPlayerUltraPhoton, isPlayerBeam, isPlayerHomingMissile, isPlayerRocket, isPlayerProximityMine, isPlayerMiniShockSphere, isPlayerShockSphere, isSuperShockSphere,
        isPlayerPrismBullet, isPlayerSuperPrismBullet;


    BossAndEnemyLevelTracker bossAndEnemyLevelTracker;
    ObjectPooler objectPooler;
    SoundPlayer soundPlayer;
    private void Start()
    {
        AttachNecessaryScripts();
        CalculateDamage();
    }
    private void AttachNecessaryScripts()
    {
        bossAndEnemyLevelTracker = FindObjectOfType<BossAndEnemyLevelTracker>();
        objectPooler = FindObjectOfType<ObjectPooler>();
        soundPlayer = FindObjectOfType<SoundPlayer>();
    }
    public void CalculateDamage()
    {
        if(isFixedDamage)
        {
            return;
        }
        if(isPlayerMiniBlade)
        {
            damage = WeaponDB.playerMiniBladeDamage;
        }
        if(isPlayerBlade)
        {
            damage = WeaponDB.playerBladeDamage;
        }
        else if(isPlayerSuperBlade)
        {
            damage = WeaponDB.playerSuperBladeDamage;
        }
        else if(isPlayerFireball)
        {
            damage = WeaponDB.playerFireballDamage;
        }
        else if(isPlayerSuperFireball)
        {
            damage = WeaponDB.playerSuperFireballDamage;
        }
        else if(isPlayerIceBullet)
        {
            damage = WeaponDB.playerIceBulletDamage;
        }
        else if (isPlayerSuperIceBullet)
        {
            damage = WeaponDB.playerSuperIceBulletDamage;
        }
        else if(isIceExplosion)
        {
            damage = WeaponDB.playerIceExplosionDamage;
        }
        else if(isPlayerUltraIceBullet)
        {
            damage = WeaponDB.playerUltraIceBulletDamage;
        }
        else if(isPlayerSuperPhoton)
        {
            damage = WeaponDB.playerSuperPhotonDamage;
        }
        else if(isPlayerUltraPhoton)
        {
            damage = WeaponDB.playerUltraPhotonDamage;
        }
        else if(isPlayerShockSphere)
        {
            damage = WeaponDB.playerShockSphereDamage;
        }
        else if (isPlayerMiniShockSphere)
        {
            damage = WeaponDB.playerMiniShockSphereDamage;
        }
        else if(isPlayerProximityMine)
        {
            damage = WeaponDB.playerProximityMineDamage;
        }
        else if(isSuperShockSphere)
        {
            damage = WeaponDB.playerSuperShockSphereDamage;
        }
        else if(isPlayerRocket)
        {
            damage = WeaponDB.playerRocketDamage;
        }
        else if(isPlayerPrismBullet)
        {
            damage = WeaponDB.playerPrismBulletDamage;
        }
        else if(isPlayerHomingMissile)
        {
            damage = WeaponDB.playerHomingMissileDamage;
        }
    }
    public float GetDamage()
    {
        return damage;
    }
   
    public void TriggerEffect()
    {
        if (isAddingEffectOnHit)
        {
            if (playerEffect)
            {
                GameObject effect = objectPooler.SpawnFromPoolPlayerEffect(effectName, transform.position, Quaternion.identity);
                effect.SetActive(true);
                
                if(isPlayerHomingMissile || isPlayerProximityMine)
                {
                    soundPlayer.PlayExplosionSFX_1();
                }
                if(isPlayerSuperBlade)
                {
                    soundPlayer.PlaySoundEffect("playerBladeSound");
                }
                if (isDisablingOnHit || isPlayerHomingMissile)
                {
                    gameObject.SetActive(false);
                }

            }
            if (enemyEffect)
            {
                GameObject effect = objectPooler.SpawnFromPoolEnemyEffect(effectName, transform.position, Quaternion.identity);
                effect.SetActive(true);
                soundPlayer.PlayExplosionSFX_0();
                if (isDisablingOnHit)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        if(isAddingProjectileOnHit)
        {
            if (playerProjectile)
            {
                GameObject effect = objectPooler.SpawnFromPoolPlayerWeapon(projectileName, transform.position, Quaternion.identity);
                effect.SetActive(true);
                if(isAddingVelocity)
                {
                    effect.GetComponent<Rigidbody2D>().velocity = new Vector2(0, optionalVelocity);
                }
                if (isDisablingOnHit)
                {
                    gameObject.SetActive(false);
                }
            }
            if (enemyProjectile)
            {
                GameObject effect = objectPooler.SpawnFromPoolEnemyWeapon(projectileName, transform.position, Quaternion.identity);
                effect.SetActive(true);
                if (isDisablingOnHit)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        if(isDisablingOnHit)
        {
            gameObject.SetActive(false);
        }
        if(isDisablingParentOnHit)
        {
            parentObj.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && !isEnemyWeapon)
        {
            if(isDisablingOnHit)
            {
                TriggerEffect();
            }
        }
        if(collision.gameObject.CompareTag("Shield"))
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(isPlayerHomingMissile)
            {
                TriggerEffect();
            }
        }
    }
}
