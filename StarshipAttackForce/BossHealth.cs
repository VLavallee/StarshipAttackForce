using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] string bossName = "Boss Name";
    public float health = 10000;
    [SerializeField] float explosionDuration = 1f;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] int pointsOnKill = 10000;

    [Header("Component references")]
    [SerializeField] GameObject deathFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] GameObject bossCanvas;

    [Header("State variables")]
    public bool bossHasBeenConnectedToStageCompleteHandler = false;
    [SerializeField] bool playerHasKilledBoss;
    [SerializeField] string bossStageName;

    private void Update()
    {
        if (health > 0)
        {
            bossCanvas.SetActive(true);
        }
    }

    //in the first idle animation frame the boss will call this function linking boss health to stage complete handler. if stage complete handler
    //is found it will respond by making the bool true
    public void FindBossFunction()
    {
        if (!bossHasBeenConnectedToStageCompleteHandler)
        {
            FindObjectOfType<StageCompleteHandler>().FindBoss();
        }
    }
    public string GetBossName()
    {
        return bossName;
    }
    public int GetPointsOnKill()
    {
        return pointsOnKill;
    }

    public float GetBossHealth()
    {
        return health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        HandleHit(damageDealer);
    }

    private void HandleHit(DamageDealer damageDealer)
    {
        HitEffect();
        health -= damageDealer.GetDamage();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        playerHasKilledBoss = true;
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(explosion, explosionDuration);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSoundVolume);
    }
    private void Idle()
    {
        GetComponent<Animator>().SetTrigger("Idle");
    }
    private void HitEffect()
    {
        if (GetComponent<Animator>() == null) { return; }

        GetComponent<Animator>().SetTrigger("HitEffect");
    }
    private void OnDestroy()
    {
        if (playerHasKilledBoss)
        {
            FindObjectOfType<GameSession>().AddToScore(GetPointsOnKill());
            FindObjectOfType<GameSession>().KillsPlusOne();
            GetComponent<PowerupDispenser>().hasBeenDestroyedByPlayer = true;
            //FindObjectOfType<StageManager>().SetStage1AsComplete();
            //looks for all GameObjects in the scene with the EnemyWeaponDestroyer script and calls the function to destroy them
            //this is done to prevent the player from the potential of being killed after destroying the boss
            EnemyWeaponDestroyer[] bossWeapons = FindObjectsOfType<EnemyWeaponDestroyer>();
            if (bossWeapons != null)
            {
                foreach (EnemyWeaponDestroyer weapon in bossWeapons)
                {
                    weapon.DestroyThisWeapon();
                }
            }
        }
    }
}
