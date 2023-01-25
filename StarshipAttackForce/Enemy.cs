using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameSession gameSession;
    ObjectPooler objectPooler;
    SoundPlayer soundPlayer;
    [SerializeField] bool isDrone = false;
    [SerializeField] bool isInvulnerable = false;
    [SerializeField] bool isAValidTarget;
    [SerializeField] bool isDummyEnemy;
    private EnemyVariables enemyVariables;
    [SerializeField] SoundCenter soundCenter;
    
    [Header("Enemy Life & Death")]
    [SerializeField] float health;
    public bool isHitByBeam = false;
    public bool hasEffectOccured = false;
    [SerializeField] float playerBeamElapsedHitTime;
    [SerializeField] float playerBeamElapsedHitTimeLimit = 0.15f;
    public bool isAffectedByFire, isAffectedByIce, isAffectedByElectricity;
    //[SerializeField] bool isOverridingAutoHealth = false;
    [SerializeField] string destructionEffectName = "mediumExplosion";
    //[SerializeField] float explosionDuration = 1f;
    //[SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] int pointsPerKill = 1000;
    [SerializeField] int pointsPerBossKill = 10000;
    //[SerializeField] int creditsPerKill = 100;
    //[SerializeField] int creditsPerBossKill = 1000;
    public bool playerHasKilledEnemy;
    [SerializeField] GameObject hitBody;

    public bool isEnemy, isBoss, hasBeenAddedToScene, hasBeenRemovedFromScene, isAddedToEnemyList, isRemovedFromEnemyList;
    public bool isWeakEnemy, isNormalEnemy, isStrongEnemy;
    [SerializeField] float rarityLevel;
    //[SerializeField] bool enemyA, enemyB, enemyC, enemyD;
    //[SerializeField] bool isUniqueEnemy = false;
    [SerializeField] Sprite uniqueSprite;
    public CameraShake cameraShake;

    [SerializeField] bool isRed, isYellow, isGreen, isPink, isBlue, isGrey, isBrown, isHit;
    [SerializeField] Sprite red, yellow, green, pink, blue, grey, brown, hitEffectSprite;
    [SerializeField] SpriteRenderer bodyRenderer;

    [SerializeField] bool isScoreAlreadyUpdated = false;

    [SerializeField] PowerupDispenser powerupDispenser;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Vector2 velocity;
    DPSCounter dpsCounter;
    BossAndEnemyLevelTracker bossAndEnemyLevelTracker;
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        cameraShake = FindObjectOfType<CameraShake>();
        objectPooler = FindObjectOfType<ObjectPooler>();
        soundPlayer = FindObjectOfType<SoundPlayer>();
        bossAndEnemyLevelTracker = FindObjectOfType<BossAndEnemyLevelTracker>();
        bossAndEnemyLevelTracker.AddEnemyToScene();
        if(!isDummyEnemy)
        {
            ChooseColor();
        }
        if(isDummyEnemy)
        {
            dpsCounter = FindObjectOfType<DPSCounter>();
        }
        rb = GetComponent<Rigidbody2D>();
        if(isEnemy && isWeakEnemy && !isDummyEnemy)
        {
            health = FindObjectOfType<BossAndEnemyLevelTracker>().GetWeakEnemyHealth();
        }
        if(isEnemy && isNormalEnemy && !isDummyEnemy)
        {
            health = FindObjectOfType<BossAndEnemyLevelTracker>().GetNormalEnemyHealth();
        }
        if (isEnemy && isStrongEnemy && !isDummyEnemy)
        {
            health = FindObjectOfType<BossAndEnemyLevelTracker>().GetStrongEnemyHealth();
        }
        if (isBoss)
        {
            health = FindObjectOfType<BossAndEnemyLevelTracker>().GetBossHealth();
        }
        if(isDrone)
        {
            health = FindObjectOfType<BossAndEnemyLevelTracker>().GetBossHealth() / 7;
        }
        if(GetComponent<PowerupDispenser>())
        {
            powerupDispenser = GetComponent<PowerupDispenser>();
        }
    }
    
    void Update()
    {
        if(!hasBeenAddedToScene)
        {
            FindObjectOfType<EnemyTracker>().AddEnemyToScene();
            hasBeenAddedToScene = true;
        }
        CheckForPlayerBeamCollisions();
    }
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player Projectile") || collision.gameObject.CompareTag("Shield Projectile"))
        {
            DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            damageDealer.TriggerEffect();
            HandleHit(damageDealer);
        }
    }
    
    private void AddToEnemyTargetList()
    {
        if(!isAddedToEnemyList)
        {
            FindObjectOfType<Player>().AddEnemyToTransformList(gameObject.transform);
            isAddedToEnemyList = true;
        }
    }
    private void RemoveEnemyFromTargetList()
    {
        if(!isRemovedFromEnemyList)
        {
            FindObjectOfType<Player>().RemoveEnemyFromTransformList(gameObject.transform);
            isRemovedFromEnemyList = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Aimed Shot Target Area"))
        {
            AddToEnemyTargetList();
        }
        if (collision.gameObject.CompareTag("Player Beam"))
        {
            if (collision.gameObject.GetComponent<PlayerBeamBehavior>())
            {
                if (isHitByBeam) { return; }
                else if(!isHitByBeam)
                {
                    isHitByBeam = true;
                    health -= FindObjectOfType<PlayerBeamBehavior>().damagePerTick;
                    
                    if (health <= 0)
                    {
                        Die();
                    }
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Aimed Shot Target Area"))
        {
            RemoveEnemyFromTargetList();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            damageDealer.TriggerEffect();
            HandleHit(damageDealer);
        }
    }
    public void FireEffect()
    {
        isAffectedByFire = true;
    }

    public void IceEffect()
    {
        isAffectedByIce = true;
    }
    public void ElectricityEffect()
    {
        isAffectedByElectricity = true;
    }

    // run when the enemy script first starts so GetMaxHealth is equal to starting health
    public float GetMaxHealth()
    {
        return health;
    }

    // run anytime to get the current health of the enemy
    public float GetHealth()
    {
        return health;
    }
    

    public void HandleHit(DamageDealer damageDealer)
    {
        if(isInvulnerable) { return; }
        HitEffect();
        soundPlayer.PlaySoundEffect("Impact_small");
        if(!isAffectedByFire)
        {
            health -= damageDealer.GetDamage();
            if (isDummyEnemy)
            {
                FindObjectOfType<DPSCounter>().CountDPS();
                FindObjectOfType<DPSCounter>().totalDamageCounted += damageDealer.GetDamage();
            }
        }
        if(isAffectedByFire)
        {
            health -= damageDealer.GetDamage() * FindObjectOfType<PlayerWeaponDatabase>().fireDamageMultiplier;
            if (isDummyEnemy)
            {
                FindObjectOfType<DPSCounter>().CountDPS();
                FindObjectOfType<DPSCounter>().totalDamageCounted += damageDealer.GetDamage();
            }
        }
        if (health <= 0)
        {
            RemoveEnemyFromTargetList();
            Die();
        }
    }

    private void Die()
    {
        //GameObject explosion = Instantiate(deathFX, transform.position, Quaternion.identity);
        //Destroy(explosion, explosionDuration);
        GameObject destructionEffect = objectPooler.SpawnFromPoolEnemyEffect(destructionEffectName, transform.position, transform.rotation);
        destructionEffect.SetActive(true);
        if(isDummyEnemy)
        {
            dpsCounter.StopCountingDPS();
        }
        if (!hasBeenRemovedFromScene)
        {
            FindObjectOfType<EnemyTracker>().SubtractEnemyFromScene();
            hasBeenRemovedFromScene = true;
        }

        playerHasKilledEnemy = true;
        if(GetComponent<WeaponAugmentDispenser>())
        {
            GetComponent<WeaponAugmentDispenser>().hasBeenDestroyedByPlayer = true;
        }
        if(powerupDispenser != null)
        {
            powerupDispenser.hasBeenDestroyedByPlayer = true;
        }
        if(isAffectedByIce)
        {
            if(GetComponent<IceExplosion>())
            {
                GetComponent<IceExplosion>().PickExplosion();
            }
        }
        soundPlayer.PlayRandomExplosion();
        Destroy(gameObject);
    }
    
   
   
    
    private void HitEffect()
    {
        if(hitBody != null)
        {
            hitBody.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        if(playerHasKilledEnemy && !isScoreAlreadyUpdated)
        {
            bossAndEnemyLevelTracker.RemoveEnemyFromScene();
            if(isBoss && !isEnemy)
            {
                gameSession.KillsPlusOne();
                gameSession.BossKillsPlusOne();
                gameSession.AddToScore(pointsPerBossKill);
                isScoreAlreadyUpdated = true;
                return;
            }
            if(isEnemy && !isBoss)
            {
                gameSession.KillsPlusOne();
                gameSession.AddToScore(pointsPerKill);
                isScoreAlreadyUpdated = true;
                return;
            }
        }
        
    }
    private void ChooseColor()
    {
        ResetColor();
        var colorRoll = Random.Range(0, 7);
        if(colorRoll == 0)
        {
            isRed = true;
            bodyRenderer.sprite = red;
        }
        if(colorRoll == 1)
        {
            isYellow = true;
            bodyRenderer.sprite = yellow;
        }
        if (colorRoll == 2)
        {
            isGreen = true;
            bodyRenderer.sprite = green;
        }
        if (colorRoll == 3)
        {
            isPink = true;
            bodyRenderer.sprite = pink;
        }
        if (colorRoll == 4)
        {
            isBlue = true;
            bodyRenderer.sprite = blue;
        }
        if (colorRoll == 5)
        {
            isGrey = true;
            bodyRenderer.sprite = grey;
        }
        if (colorRoll == 6)
        {
            isBrown = true;
            bodyRenderer.sprite = brown;
        }
    }
    private void ResetColor()
    {
        isRed = false;
        isYellow = false;
        isGreen = false;
        isPink = false;
        isBlue = false;
        isGrey = false;
        isBrown = false;
    }
    
    private void Idle()
    {
        if(isRed)
        {
            bodyRenderer.sprite = red;
        }
        if (isYellow)
        {
            bodyRenderer.sprite = yellow;
        }
        if (isGreen)
        {
            bodyRenderer.sprite = green;
        }
        if (isPink)
        {
            bodyRenderer.sprite = pink;
        }
        if (isBlue)
        {
            bodyRenderer.sprite = blue;
        }
        if (isGrey)
        {
            bodyRenderer.sprite = grey;
        }
        if (isBrown)
        {
            bodyRenderer.sprite = brown;
        }
    }
    private void CheckForPlayerBeamCollisions()
    {
        if (isHitByBeam && !hasEffectOccured)
        {
            DamageExplosion();
            hasEffectOccured = true;
        }
        if (isHitByBeam)
        {
            playerBeamElapsedHitTime += Time.deltaTime;
            if (playerBeamElapsedHitTime > playerBeamElapsedHitTimeLimit)
            {
                isHitByBeam = false;
                hasEffectOccured = false;
                playerBeamElapsedHitTime = 0;
            }
        }
    }
    public void DamageExplosion()
    {
        HitEffect();
        if(FindObjectOfType<PlayerBeamBehavior>())
        {
            health -= FindObjectOfType<PlayerBeamBehavior>().damagePerTick;
        }
        GameObject destructionEffect = objectPooler.SpawnFromPoolEnemyEffect(destructionEffectName, transform.position, transform.rotation);
        destructionEffect.SetActive(true);
    }
    public void ChangeInvulnerableStatus(string trueOrFalse)
    {
        if(trueOrFalse == "true")
        {
            isInvulnerable = true;
            return;
        }
        if(trueOrFalse == "false")
        {
            isInvulnerable = false;
            return;
        }
    }
}
