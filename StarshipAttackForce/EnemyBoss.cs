using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    [SerializeField] Transform weaponOnePosA, weaponOnePosB, weaponTwoPosA, weaponTwoPosB, weaponThreePosA, weaponThreePosB;
    
    //weapon 1 (energy blaster)
    [SerializeField] float weaponOneStartingShotTime, weaponThreeStartingShotTime;
    [SerializeField] float weaponOneDefaultShotTime, weaponTwoDefaultShotTime, weaponThreeDefaultShotTime;
    [SerializeField] float weaponOneShotTime, weaponThreeShotTime;

    [Header("Large Energy Blasters")]
    //weapon 2 (large energy blaster)
    [SerializeField] Transform[] largeEnergyBlasters;
    [SerializeField] GameObject largeEnergyBlasterBulletPrefab;
    [SerializeField] float largeEnergyBlasterBulletVelocity;
    [SerializeField] float largeEnergyBlasterFiringTime;
    [SerializeField] float largeEnergyBlasterFiringTimeLimit;
    [SerializeField] float largeEnergyBlasterDefaultFiringTime = 3f;

    //weapon 3 (wave beam)
    [SerializeField] float laserOscillatorTimeLimitA, laserOscillatorTimeLimitB;
    public float waveBeamATimeLimitShort, waveBeamATimeLimitLong;
    [SerializeField] bool isShortTimeLimit = true;
    public float waveBeamBVelocity;



    [SerializeField] GameObject bossBulletPrefabOne, bossBulletPrefabTwo, bossBulletPrefabThree;
    [SerializeField] float weaponOneBulletVelocity, weaponTwoBulletVelocity, weaponThreeBulletVelocity;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.2f;
    [SerializeField] bool shouldShootRandomly = false;
    [SerializeField] bool canShoot = true;

    [Header("Enemy Rarity Info")]
    [SerializeField] float rarityLevel;

    void Start()
    {
        weaponOneShotTime = weaponOneStartingShotTime;
        largeEnergyBlasterFiringTime = largeEnergyBlasterDefaultFiringTime;
        weaponThreeShotTime = weaponThreeStartingShotTime;
    }

    void Update()
    {
        if (canShoot)
        {
            WeaponOneShootingSequence();
            FireLargeEnergyBlaster();
            WeaponThreeShootingSequence();
        }
    }

    private void WeaponOneShootingSequence()
    {
        weaponOneShotTime -= Time.deltaTime;
        if (weaponOneShotTime <= 0)
        {
            FireWeaponOne();
            weaponOneShotTime = weaponOneDefaultShotTime;
        }
    }

    private void FireLargeEnergyBlaster()
    {
        largeEnergyBlasterFiringTime -= Time.deltaTime;
        if(largeEnergyBlasterFiringTime <= 0)
        {
            foreach(Transform blasterPosition in largeEnergyBlasters)
            {
                GameObject blasterBullet = Instantiate(largeEnergyBlasterBulletPrefab, blasterPosition.transform.position, blasterPosition.transform.rotation);
                blasterBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -largeEnergyBlasterBulletVelocity);
            }
            largeEnergyBlasterFiringTime = weaponTwoDefaultShotTime;
        }
    }

    IEnumerator LaserOscillatorShotA()
    {
        FireWeaponThreeA();
        yield return new WaitForSeconds(laserOscillatorTimeLimitA);
        FireWeaponThreeB();
        yield return new WaitForSeconds(laserOscillatorTimeLimitB);
        FireWeaponThreeA();
    }

    IEnumerator LaserOscillatorShotB()
    {
        FireWeaponThreeB();
        yield return new WaitForSeconds(laserOscillatorTimeLimitB);
        FireWeaponThreeA();
        yield return new WaitForSeconds(laserOscillatorTimeLimitA);
        FireWeaponThreeB();
    }

    public float GetWaveBeamATimeLimit()
    {
        if(isShortTimeLimit)
        {
            isShortTimeLimit = false;
            return waveBeamATimeLimitShort;
        }
        else
        {
            isShortTimeLimit = true;
            return waveBeamATimeLimitLong;
        }
    }

    private void WeaponThreeShootingSequence()
    {
        weaponThreeShotTime -= Time.deltaTime;
        if(weaponThreeShotTime <= 0)
        {
            StartCoroutine(LaserOscillatorShotA());
            StartCoroutine(LaserOscillatorShotB());
            weaponThreeShotTime = weaponThreeDefaultShotTime + laserOscillatorTimeLimitA + laserOscillatorTimeLimitB;
        }
    }

    private void FireWeaponOne()
    {
        if (!canShoot) { return; }
        GameObject bulletOneA = Instantiate(bossBulletPrefabOne, weaponOnePosA.transform.position, weaponOnePosA.rotation);
        bulletOneA.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -weaponOneBulletVelocity);

        GameObject bulletOneB = Instantiate(bossBulletPrefabOne, weaponOnePosB.transform.position, weaponOnePosB.rotation);
        bulletOneB.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -weaponOneBulletVelocity);
        
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void FireWeaponThreeA()
    {
        if(!canShoot) { return; }
        GameObject bulletThreeA = Instantiate(bossBulletPrefabThree, weaponThreePosA.transform.position, weaponThreePosA.rotation);
        bulletThreeA.GetComponent<Rigidbody2D>().velocity = new Vector2(weaponThreeBulletVelocity, 0);

        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void FireWeaponThreeB()
    {
        GameObject bulletThreeB = Instantiate(bossBulletPrefabThree, weaponThreePosB.transform.position, weaponThreePosB.rotation);
        bulletThreeB.GetComponent<Rigidbody2D>().velocity = new Vector2(-weaponThreeBulletVelocity, 0);

        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("CanShoot False"))
        {
            canShoot = false;
        }

        if (collision.gameObject.CompareTag("CanShoot True"))
        {
            canShoot = true;
        }
    }
}
