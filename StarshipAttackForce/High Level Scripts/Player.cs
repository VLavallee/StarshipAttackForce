using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    // Health, Shield etc Config
    public string activeElement;
    public bool isAlive = true;
    public int shieldAmount = 1;
    public bool shieldIsActive = false;
    public Animator shieldIconAnimator;
    public TextMeshProUGUI shieldAmountText;
    //public bool shieldIsDeactivating = false;
    //public bool playerHasShieldSaved = false;
    public float playerHealth;
    public float playerHealthLimit;
    [SerializeField] float crashDamageAmount;
    [SerializeField] bool isCrashDamageSet = false;
    public float shieldTime, shieldTimeLimit;
    [SerializeField] float explosionDuration;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.7f;
    [SerializeField] List<Collider2D> playerColliders;
    [SerializeField] GameObject shieldObj;
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitEffect;
    [SerializeField] bool hasPlayerBeenKilled = false;
    public CameraShake cameraShake;

    SoundPlayer soundPlayer;
    bool isSoundPlayerFound;

    // Projectile Weapon Config
    ObjectPooler objectPooler;
    PlayerWeaponDatabase playerWeaponDatabase;
    public bool canShoot = true;
    [SerializeField] bool isWeaponDatabaseLoaded = false;
    public bool secondaryFiring = false;
    public bool hasPressedFireButton = false;
    [SerializeField] bool isMainWeaponLoaded = false;
    bool isSecondaryWeaponLoaded = false;

    public bool temporaryMainWeaponActive = false;
    public bool dronesAreActive = false;
    //public float droneEnergyTime, droneEnergyTimeLimit;
    public bool temporarySecondaryWeaponActive = false;
    public float temporaryMainWeaponElapsedTime, temporarySecondaryWeaponElapsedTime, temporaryWeaponTimeLimit;

    [SerializeField] List<Transform> centerGunPosition;
    [SerializeField] List<Transform> gunPositionsB;
    [SerializeField] List<Transform> gunPositionsC;
    [SerializeField] List<Transform> leftAngleGunPositionsA;
    [SerializeField] List<Transform> rightAngleGunPositionsA;
    [SerializeField] List<Transform> leftAngleGunPositionsB;
    [SerializeField] List<Transform> rightAngleGunPositionsB;
    [SerializeField] List<Transform> leftAngleGunPositionsC;
    [SerializeField] List<Transform> rightAngleGunPositionsC;
    [SerializeField] List<Transform> leftAngleGunPositionsD;
    [SerializeField] List<Transform> rightAngleGunPositionsD;
    [SerializeField] Transform leftSaucerGun, rightSaucerGun;
    [SerializeField] Transform leftDroneFiringPos, rightDroneFiringPos;
    

    [SerializeField] float fireballRate, photonRate, iceBulletRate, bladeRate, shockSphereRate, prismRate;

    public string mainWeapon;
    public string secondaryWeapon;
    [SerializeField] bool areWeaponsLoaded;
    public GameObject playerMainWeapon;
    public int mainFiringMode = 1;
    public int secondaryFiringMode = 1;

    public float mainDmgMultiplier = 1;
    public float secondaryDmgMultiplier = 1;
    public float mainGunProjectileVelocity = 20f;
    public float mainGunFireRate;
    public float mainGunLevel1FireRate = .25f;
    // UNIQUE ABILITIES
    public float uniqueAbilityElapsedTime, uniqueAbilityTimeLimit;
    public bool uniqueAbilityIsActive, uniqueAbilityIsTimed, isDCUniqueAbility, isEXTUniqueAbility, isSaucerUniqueAbility, isUltDCUniqueAbility, isUniqueAbilityLoaded, isPlayerInvincible;
    //[SerializeField] float AL1ENUniqueHealthTime, AL1ENUniqueHealthTimeLimit, AL1ENUniqueHealthPerTick;
    [SerializeField] GameObject dCSpecialBody;
    [SerializeField] List<GameObject> saucerAttackOrbs;

    public bool isBeamWeaponLoaded, isDroneLoaded, isHomingMissileLoaded, isRocketLoaded, isSpinnerLoaded, isProximityMineLoaded, isFireballLoaded, isIceBulletLoaded,
        isSuperPhotonLoaded, isBladeLoaded, isShockSphereLoaded, isGravityBallLoaded;
    [SerializeField] List<GameObject> beamWeapons;
    public GameObject spinner;
    [SerializeField] List<GameObject> drones;
    public bool isLeftDroneActive, isRightDroneActive;

    // one shot ultra weapon
    [SerializeField] float ultraIceBulletVelocity;
    [SerializeField] float ultraPhotonBulletVelocity;
    [SerializeField] float proximityMineVelocity;

    public bool secondaryWeaponIsActive;
    public GameObject playerSecondaryWeapon;

    public bool beamLevel1Active, beamLevel2Active, beamLevel3Active, shieldProjectileLevel1Active, shieldProjectileLevel2Active, shieldProjectileLevel3Active, dronesLevel1Active, dronesLevel2Active,
        dronesLevel3Active;
    
    public float secondaryGunDefaultProjectileVelocity = 25f;
    public float secondaryGunProjectileVelocity;
    public float secondaryGunFireRate;
    public float secondaryGunLevel1FireRate = .25f;
    public float homingMissileFiringRate = 2f;
    public float homingMissileVelocity = 10f;
    public float augHomingMissileVelocity = 12f;
    public float superHomingMissileVelocity = 15f;

    // ult DC unique abilty
    [SerializeField] Transform aimedShotCannonArm, aimedShotCannonFirePoint;
    [SerializeField] SpriteRenderer aimedShotCannonSprite;
    [SerializeField] Color aimedShotCannonColorSolid, aimedShotCannonColorInvisible;
    [SerializeField] public float aimedShotFireRate, aimedShotVelocityModifier;
    [SerializeField] Transform target;
    [SerializeField] List<Transform> enemyTargets;
    
    [SerializeField] float leftAngleAForXAxis, rightAngleAForXAxis, leftAngleAForEuler, rightAngleAForEuler;
    [SerializeField] float leftAngleBForXAxis, rightAngleBForXAxis, leftAngleBForEuler, rightAngleBForEuler;
    [SerializeField] float leftAngleCForXAxis, rightAngleCForXAxis, leftAngleCForEuler, rightAngleCForEuler;
    [SerializeField] float leftAngleDForXAxis, rightAngleDForXAxis, leftAngleDForEuler, rightAngleDForEuler;
    [SerializeField] Transform bulletTransformParent; //this is only used to keep the bullets in one tidy place within the scene view
    Coroutine firingCoroutine;

    // Player Flying Sequence Config
    public bool shipStartFlyingSequenceStarted;
    [SerializeField] float flyAwaySpeed;
    [SerializeField] float slowVelocity;

    // Player Ship Type , Sprite & Animation Config
    public bool isDC1000, isEXT9, isSaucer, isUltDC, isUltBomber, isUltSaucer, isAltDC, isAltBomber, isAltSaucer, isAltUltDC, isAltUltBomber, isAltUltSaucer;
    public int blueEnergyModifier, orangeEnergyModifier, purpleUpgradeCost;
    [SerializeField] public bool altAnimTriggered = false;
    [SerializeField] private bool shipArtChecked = false;
    public float shipStartSequenceTime, shipStartSequenceTimeLimit;
    [SerializeField] Sprite defaultDefault, defaultWhite, defaultYellow, defaultOrange, defaultRed, defaultPink, defaultPurple, defaultBlue, defaultGreen, defaultBrown;
    [SerializeField] Sprite altDefault, altWhite, altYellow, altOrange, altRed, altPink, altPurple, altBlue, altGreen, altBrown;
    [SerializeField] SpriteRenderer bodySpriteRenderer, exhaustSpriteRenderer;
    [SerializeField] Animator exhaustAnimator;
    [SerializeField] string activeColorAndType;
    [SerializeField] PlayerDroneControl playerDroneControl;
    CrystalCanvas crystalCanvas;
    GameSession gameSession;
    [SerializeField] GameObject pauseButton;
    bool isGameSessionFound;
    private void Start()
    {
        playerHealthLimit = playerHealth;
        TriggerStartFlyingSequence();
        SetShipTypeAndColor();
        objectPooler = FindObjectOfType<ObjectPooler>();
        crystalCanvas = FindObjectOfType<CrystalCanvas>();
        soundPlayer = FindObjectOfType<SoundPlayer>();
        gameSession = FindObjectOfType<GameSession>();
        StartCoroutine(ApplySavedColor());
    }

    void Update()
    {
        if(!isGameSessionFound)
        {
            gameSession = FindObjectOfType<GameSession>();
            isGameSessionFound = true;
            gameSession.StartFreshTimer();
        }
        SetCrashDamageAmount();
        if(isSoundPlayerFound == false)
        {
            soundPlayer = FindObjectOfType<SoundPlayer>();
            isSoundPlayerFound = true;
            soundPlayer.PlaySoundEffect("playerShipFlightSound");
        }
        if (isWeaponDatabaseLoaded == false)
        {
            playerWeaponDatabase = FindObjectOfType<PlayerWeaponDatabase>();
            isWeaponDatabaseLoaded = true;
        }
        if (isWeaponDatabaseLoaded && isMainWeaponLoaded == false)
        {
            ChangeMainAmmunition(mainWeapon);
            isMainWeaponLoaded = true;
        }
        if(!isUniqueAbilityLoaded && FindObjectOfType<PurpleButton>())
        {
            SetShipUniqueAbility();
        }
        //CheckWhichShipArtToUse();
        DroneStatus();
        Fire();
        if(temporaryMainWeaponActive)
        {
            temporaryMainWeaponElapsedTime += Time.deltaTime;
            
            if(temporaryMainWeaponElapsedTime > temporaryWeaponTimeLimit)
            {
                DowngradeMainWeaponAmmunition();
                temporaryMainWeaponActive = false;
                temporaryMainWeaponElapsedTime = 0;
            }
        }
        if(temporarySecondaryWeaponActive)
        {
            temporarySecondaryWeaponElapsedTime += Time.deltaTime;
            if (temporarySecondaryWeaponElapsedTime > temporaryWeaponTimeLimit)
            {
                DowngradeSecondaryWeaponAmmunition();
                temporarySecondaryWeaponActive = false;
                temporarySecondaryWeaponElapsedTime = 0;
            }
        }
        if(uniqueAbilityIsActive)
        {
            if(isDCUniqueAbility)
            {
                uniqueAbilityElapsedTime += Time.deltaTime;
                if(uniqueAbilityElapsedTime > uniqueAbilityTimeLimit)
                {
                    uniqueAbilityIsActive = false;
                    uniqueAbilityElapsedTime = 0;
                    DeactivateDCUniqueAbility();
                }
            }
            if(isEXTUniqueAbility)
            {
                uniqueAbilityElapsedTime += Time.deltaTime;
                if (uniqueAbilityElapsedTime > uniqueAbilityTimeLimit)
                {
                    uniqueAbilityIsActive = false;
                    uniqueAbilityElapsedTime = 0;
                    DeactivateEXTUNiqueAbility();
                }
            }
            if(isUltDCUniqueAbility)
            {
                uniqueAbilityElapsedTime += Time.deltaTime;
                if (uniqueAbilityElapsedTime > uniqueAbilityTimeLimit)
                {
                    uniqueAbilityIsActive = false;
                    uniqueAbilityElapsedTime = 0;
                    DeactivateUltDCUniqueAbility();
                }
            }
        }
        
        // what the code below does is when the shield is turned on it counts up from 0. If at anytime the shieldTime is less than the limit plus one second
        // then the shield can not be deactivating, to keep the deactivation animation from playing. If it passes that value then it triggers the bool and turns on
        // the deactivating animation. If the player finds another shield powerup at anytime while the shield is up, it resets the shield, setting the shieldTime
        // back to 0, and returning to the normal animation
        ShieldBehavior();
    }
    IEnumerator ApplySavedColor()
    {
        yield return new WaitUntil(() =>  gameSession.savedGameLoaded);
        if(gameSession.savedColor == ColorDB.defaultColor)
        {
            bodySpriteRenderer.sprite = defaultDefault;
        }
        if (gameSession.savedColor == ColorDB.white)
        {
            bodySpriteRenderer.sprite = defaultWhite;
        }
        if (gameSession.savedColor == ColorDB.yellow)
        {
            bodySpriteRenderer.sprite = defaultYellow;
        }
        if (gameSession.savedColor == ColorDB.orange)
        {
            bodySpriteRenderer.sprite = defaultOrange;
        }
        if (gameSession.savedColor == ColorDB.red)
        {
            bodySpriteRenderer.sprite = defaultRed;
        }
        if (gameSession.savedColor == ColorDB.pink)
        {
            bodySpriteRenderer.sprite = defaultPink;
        }
        if (gameSession.savedColor == ColorDB.purple)
        {
            bodySpriteRenderer.sprite = defaultPurple;
        }
        if (gameSession.savedColor == ColorDB.blue)
        {
            bodySpriteRenderer.sprite = defaultBlue;
        }
        if (gameSession.savedColor == ColorDB.green)
        {
            bodySpriteRenderer.sprite = defaultGreen;
        }
        if (gameSession.savedColor == ColorDB.brown)
        {
            bodySpriteRenderer.sprite = defaultBrown;
        }
    }
    public void ChangeMainAmmunition(string newAmmunitionName)
    {
        mainWeapon = newAmmunitionName;
        if(newAmmunitionName == WeaponDB.fireballName)
        {
            mainGunFireRate = fireballRate;
            mainGunProjectileVelocity = WeaponDB.fireballVelocity;
            soundPlayer.ChangeWeaponSound("fireball");
        }
        else if(newAmmunitionName == WeaponDB.superPhotonBulletName)
        {
            mainGunFireRate = photonRate;
            mainGunProjectileVelocity = WeaponDB.photonVelocity;
            soundPlayer.ChangeWeaponSound("photon");
        }
        else if (newAmmunitionName == WeaponDB.iceBulletName)
        {
            mainGunFireRate = iceBulletRate;
            mainGunProjectileVelocity = WeaponDB.iceBulletVelocity;
            soundPlayer.ChangeWeaponSound("iceBullet");
        }
        else if (newAmmunitionName == WeaponDB.bladeName)
        {
            mainGunFireRate = bladeRate;
            mainGunProjectileVelocity = WeaponDB.bladeVelocity;
            soundPlayer.ChangeWeaponSound("blade");
        }
        else if (newAmmunitionName == WeaponDB.shockSphereName)
        {
            mainGunFireRate = shockSphereRate;
            mainGunProjectileVelocity = WeaponDB.shockSphereVelocity;
            soundPlayer.ChangeWeaponSound("shockSphere");
        }
        else if (newAmmunitionName == WeaponDB.prismName)
        {
            mainGunFireRate = prismRate;
            mainGunProjectileVelocity = WeaponDB.prismBulletVelocity;
            //soundPlayer.ChangeWeaponSound("");
        }
    }
    public void ChangeMainAmmunition(string newAmmunitionName, string temp)
    {
        mainWeapon = newAmmunitionName;
        // change all these to match new superfireball etc.
        if (newAmmunitionName == WeaponDB.superFireballName)
        {
            mainGunFireRate = fireballRate;
            mainGunProjectileVelocity = WeaponDB.fireballVelocity;
            soundPlayer.ChangeWeaponSound("superFireball");
        }
        else if (newAmmunitionName == WeaponDB.superPhotonBulletName)
        {
            mainGunFireRate = photonRate;
            mainGunProjectileVelocity = WeaponDB.photonVelocity;
            soundPlayer.ChangeWeaponSound("photon");
        }
        else if (newAmmunitionName == WeaponDB.iceBulletName)
        {
            mainGunFireRate = iceBulletRate;
            mainGunProjectileVelocity = WeaponDB.iceBulletVelocity;
        }
        else if (newAmmunitionName == WeaponDB.superBladeName)
        {
            mainGunFireRate = bladeRate;
            mainGunProjectileVelocity = WeaponDB.bladeVelocity;
            soundPlayer.ChangeWeaponSound("superBlade");
        }
        else if (newAmmunitionName == WeaponDB.superShockSphereName)
        {
            mainGunFireRate = shockSphereRate;
            mainGunProjectileVelocity = WeaponDB.shockSphereVelocity;
            soundPlayer.ChangeWeaponSound("superShockSphere");
        }
        if (temp == "temp")
        {
            temporaryMainWeaponActive = true;
        }
    }
    public void ChangeSecondaryAmmunition(string newAmmunitionName)
    {
        secondaryWeapon = newAmmunitionName;
        if (newAmmunitionName == WeaponDB.superHomingMissileName)
        {
            secondaryGunFireRate = homingMissileFiringRate;
            secondaryGunProjectileVelocity = WeaponDB.homingMissileVelocity;
        }
    }
    private void DowngradeMainWeaponAmmunition()
    {
        if(mainWeapon == WeaponDB.superBladeName)
        {
            ChangeMainAmmunition(WeaponDB.bladeName);
            return;
        }
        if (mainWeapon == WeaponDB.superFireballName)
        {
            ChangeMainAmmunition(WeaponDB.fireballName);
            return;
        }
        if (playerMainWeapon == FindObjectOfType<PlayerWeaponDatabase>().superPrismBulletObj)
        {
            playerMainWeapon = FindObjectOfType<PlayerWeaponDatabase>().augPrismBulletObj;
            return;
        }
        if (playerMainWeapon == FindObjectOfType<PlayerWeaponDatabase>().superSaucerShotObj)
        {
            playerMainWeapon = FindObjectOfType<PlayerWeaponDatabase>().augSaucerShotObj;
            return;
        }
        if (mainWeapon == WeaponDB.superShockSphereName)
        {
            ChangeMainAmmunition(WeaponDB.shockSphereName);
            return;
        }
    }
    public void ClearSecondWeapon()
    {
        playerSecondaryWeapon = null;
    }
    private void DowngradeSecondaryWeaponAmmunition()
    {
        if(isBeamWeaponLoaded)
        {
            DeactivateBeamWeapons();
            return;
        }
    }
    private void ShieldBehavior()
    {
        if (shieldIsActive)
        {
            shieldTime += Time.deltaTime;
            if(shieldTime > shieldTimeLimit)
            {
                DeactivateShield();
                shieldTime = 0;
            }
        }
    }
    public void ActivateShield()
    {
        shieldObj.SetActive(true);
        shieldIsActive = true;
        shieldAmount -= 1;
        shieldAmountText.text = "x" + shieldAmount.ToString();
        if(soundPlayer != null)
        {
            soundPlayer.PlaySoundEffect("playerShieldSound");
        }
    }
    public void DeactivateShield()
    {
        shieldObj.SetActive(false);
        shieldIsActive = false;
        UpdateShieldIcon();
    }
    public void UpdateShieldIcon()
    {
        shieldAmountText.text = "x" + shieldAmount.ToString();
        if (shieldAmount > 0)
        {
            shieldIconAnimator.SetTrigger("ShieldCharged");
        }
        else
        {
            shieldIconAnimator.SetTrigger("ShieldNotCharged");
        }
    }
    public void ResetShield()
    {
        shieldTime = 0;
    }

    private void SetCrashDamageAmount()
    {
        if(isCrashDamageSet) { return; }
        else
        {
            crashDamageAmount = playerHealth / 4;
            isCrashDamageSet = true;
        }
    }

    private void SetShipTypeAndColor()
    {
        if(FindObjectOfType<SelectedShip>())
        {
            if(FindObjectOfType<SelectedShip>().GetShipType() == null)
            {
                bodySpriteRenderer.sprite = defaultDefault;
            }
        }
        if(FindObjectOfType<SelectedShip>())
        {
            if(FindObjectOfType<SelectedShip>().GetShipColor() != null)
            {
                exhaustAnimator.SetTrigger("Default");
            }
        }
        string shipType = FindObjectOfType<SelectedShip>().GetShipType();
        string shipColor = FindObjectOfType<SelectedShip>().GetShipColor();

        if(shipType == "Default" || shipType == "")
        {
            if(shipColor == "Default")
            {
                bodySpriteRenderer.sprite = defaultDefault;
            }
            if (shipColor == "White")
            {
                bodySpriteRenderer.sprite = defaultWhite;
            }
            if (shipColor == "Yellow")
            {
                bodySpriteRenderer.sprite = defaultYellow;
            }
            if (shipColor == "Orange")
            {
                bodySpriteRenderer.sprite = defaultOrange;
            }
            if (shipColor == "Red")
            {
                bodySpriteRenderer.sprite = defaultRed;
            }
            if (shipColor == "Pink")
            {
                bodySpriteRenderer.sprite = defaultPink;
            }
            if (shipColor == "Purple")
            {
                bodySpriteRenderer.sprite = defaultPurple;
            }
            if (shipColor == "Blue")
            {
                bodySpriteRenderer.sprite = defaultBlue;
            }
            if (shipColor == "Green")
            {
                bodySpriteRenderer.sprite = defaultGreen;
            }
            if (shipColor == "Brown")
            {
                bodySpriteRenderer.sprite = defaultBrown;
            }
            exhaustAnimator.SetTrigger("Default");
        }
        if (shipType == "Alternate")
        {
            if (shipColor == "Default")
            {
                bodySpriteRenderer.sprite = altDefault;
            }
            if (shipColor == "White")
            {
                bodySpriteRenderer.sprite = altWhite;
            }
            if (shipColor == "Yellow")
            {
                bodySpriteRenderer.sprite = altYellow;
            }
            if (shipColor == "Orange")
            {
                bodySpriteRenderer.sprite = altOrange;
            }
            if (shipColor == "Red")
            {
                bodySpriteRenderer.sprite = altRed;
            }
            if (shipColor == "Pink")
            {
                bodySpriteRenderer.sprite = altPink;
            }
            if (shipColor == "Purple")
            {
                bodySpriteRenderer.sprite = altPurple;
            }
            if (shipColor == "Blue")
            {
                bodySpriteRenderer.sprite = altBlue;
            }
            if (shipColor == "Green")
            {
                bodySpriteRenderer.sprite = altGreen;
            }
            if (shipColor == "Brown")
            {
                bodySpriteRenderer.sprite = altBrown;
            }
            exhaustAnimator.SetTrigger("Alternate");
        }
    }
    // this is just for reference to easily see what is currently active
    public void SetActiveColorAndSprite(string activeColorAndTypeString)
    {
        activeColorAndType = activeColorAndTypeString;
    }
    private void CheckWhichShipArtToUse()
    {
        if(shipArtChecked)
        {
            return;
        }
        if(!shipArtChecked)
        {
            if (FindObjectOfType<GameSession>().GetDefaultArtStatus() == false)
            {
                // this code only runs if default art status is false
                if (isDC1000)
                {
                    isAltDC = true;
                    shipArtChecked = true;
                    return;
                }
                if (isEXT9)
                {
                    isAltBomber = true;
                    shipArtChecked = true;
                    return;
                }
                if (isSaucer)
                {
                    isAltSaucer = true;
                    shipArtChecked = true;
                    return;
                }
                if (isUltDC)
                {
                    isAltUltDC = true;
                    shipArtChecked = true;
                    return;
                }
                if (isUltBomber)
                {
                    isAltUltBomber = true;
                    shipArtChecked = true;
                    return;
                }
                if (isUltSaucer)
                {
                    isAltUltSaucer = true;
                    shipArtChecked = true;
                    return;
                }
            }
        }
    }

    public float GetHealthAmount()
    {
        return playerHealth;
    }

    public void AddHealth(float healthAmount)
    {
        if(playerHealth < playerHealthLimit)
        {
            FindObjectOfType<GreenBorder>().ActivateGreenBorder();
            if (playerHealth + healthAmount <= playerHealthLimit)
            {
                playerHealth += healthAmount;
                return;
            }
            else if (playerHealth + healthAmount > playerHealthLimit)
            {
                playerHealth = playerHealthLimit;
                return;
            }
        }
        else if(playerHealth == playerHealthLimit)
        {
            FindObjectOfType<GameSession>().IncreaseCredits(500);
        }
    }

    public void Fire()
    {
        if (Input.GetButtonDown("Fire1") && !hasPressedFireButton)
        {
            firingCoroutine = StartCoroutine(FireContinously());
            
            //if (secondaryFiring)
            //{
            //    StartCoroutine(FireSecondaryContinuously());
            //}
            hasPressedFireButton = true;
        }
    }
    public void FireSecondWeapon()
    {
        if(secondaryWeaponIsActive) { return; }
        
        secondaryWeaponIsActive = true;
        StartCoroutine(FireSecondaryContinuously());
    }
    public void StopSecondWeaponFiring()
    {
        StopCoroutine(FireSecondaryContinuously());
        secondaryWeaponIsActive = false;
    }
    public void StopFiring()
    {
        canShoot = false;
        hasPressedFireButton = false;
        StopAllCoroutines();
    }
    public void EnableFiring()
    {
        canShoot = true;
        hasPressedFireButton = false;
    }
    public void ChangeMainFiringMode(int newMode)
    {
        mainFiringMode = newMode;
    }
    public void ChangeSeconaryFiringMode(int newMode)
    {
        secondaryFiringMode = newMode;
    }
    IEnumerator FireContinously()
    {
        while (true && canShoot)
        {
            if (objectPooler == null)
            {
                objectPooler = FindObjectOfType<ObjectPooler>();
            }
            if (isDC1000)
            {
                if (mainFiringMode == 1)
                {
                    foreach (Transform position in gunPositionsB)
                    {
                        //GameObject projectile = Instantiate(playerMainWeapon, position.transform.position, Quaternion.identity);
                        //projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                        //projectile.transform.parent = bulletTransformParent;
                        
                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.identity);
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                        
                    }
                }
                if(mainFiringMode == 2)
                {
                    foreach (Transform position in centerGunPosition)
                    {
                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.identity);
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                        projectile.transform.parent = bulletTransformParent;
                    }
                    foreach (Transform position in gunPositionsB)
                    {
                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.identity);
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                    }
                }
                if (mainFiringMode == 3)
                {
                    if (objectPooler == null)
                    {
                        objectPooler = FindObjectOfType<ObjectPooler>();
                    }
                    foreach (Transform position in centerGunPosition)
                    {
                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.identity);
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                    }
                    foreach (Transform position in gunPositionsB)
                    {
                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.identity);
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                    }
                    foreach (Transform position in gunPositionsC)
                    {
                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.identity);
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                    }
                }
            }
            
            if(isEXT9)
            {
                if (mainFiringMode == 1)
                {
                    foreach (Transform position in centerGunPosition)
                    {
                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.identity);
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                    }
                    foreach (Transform position in leftAngleGunPositionsA)
                    {
                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleAForEuler));
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleAForXAxis, mainGunProjectileVelocity);
                    }
                    foreach (Transform position in rightAngleGunPositionsA)
                    {
                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleAForEuler));
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleAForXAxis, mainGunProjectileVelocity);
                    }
                }
                if(mainFiringMode == 1 && secondaryFiring)
                {
                    if (playerSecondaryWeapon != FindObjectOfType<PlayerWeaponDatabase>().homingMissileObj &&
                        playerSecondaryWeapon != FindObjectOfType<PlayerWeaponDatabase>().augHomingMissileObj &&
                        playerSecondaryWeapon != FindObjectOfType<PlayerWeaponDatabase>().superHomingMissileObj)
                    {
                        foreach (Transform position in leftAngleGunPositionsB)
                        {
                            GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(secondaryWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleBForEuler));
                            projectile.SetActive(true);
                            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleBForXAxis, mainGunProjectileVelocity);
                        }
                        foreach (Transform position in rightAngleGunPositionsB)
                        {
                            GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(secondaryWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleBForEuler));
                            projectile.SetActive(true);
                            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleBForXAxis, mainGunProjectileVelocity);
                        }
                    }
                }
                if (mainFiringMode == 2)
                {
                    foreach (Transform position in centerGunPosition)
                    {
                        //GameObject bullet = Instantiate(playerMainWeapon, position.transform.position, Quaternion.identity);
                        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                        

                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.identity);
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                        
                    }
                    foreach (Transform position in leftAngleGunPositionsA)
                    {
                        //GameObject bullet = Instantiate(playerMainWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleAForEuler));
                        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleAForXAxis, mainGunProjectileVelocity);
                        

                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleAForEuler));
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleAForXAxis, mainGunProjectileVelocity);
                    }
                    foreach (Transform position in rightAngleGunPositionsA)
                    {
                        //GameObject bullet = Instantiate(playerMainWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleAForEuler));
                        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleAForXAxis, mainGunProjectileVelocity);

                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleAForEuler));
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleAForXAxis, mainGunProjectileVelocity);
                    }

                    foreach (Transform position in leftAngleGunPositionsB)
                    {
                        //GameObject bullet = Instantiate(playerMainWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleBForEuler));
                        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleBForXAxis, mainGunProjectileVelocity);

                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleBForEuler));
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleBForXAxis, mainGunProjectileVelocity);
                    }
                    foreach (Transform position in rightAngleGunPositionsB)
                    {
                        //GameObject bullet = Instantiate(playerMainWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleBForEuler));
                        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleBForXAxis, mainGunProjectileVelocity);

                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleBForEuler));
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleBForXAxis, mainGunProjectileVelocity);
                    }
                }
                if (mainFiringMode == 3)
                {
                    foreach (Transform position in centerGunPosition)
                    {
                        //GameObject bullet = Instantiate(playerMainWeapon, position.transform.position, Quaternion.identity);
                        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);

                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.identity);
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                    }
                    foreach (Transform position in leftAngleGunPositionsA)
                    {
                        //GameObject bullet = Instantiate(playerMainWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleAForEuler));
                        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleAForXAxis, mainGunProjectileVelocity);

                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleAForEuler));
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleAForXAxis, mainGunProjectileVelocity);
                    }
                    foreach (Transform position in rightAngleGunPositionsA)
                    {
                        //GameObject bullet = Instantiate(playerMainWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleAForEuler));
                        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleAForXAxis, mainGunProjectileVelocity);

                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleAForEuler));
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleAForXAxis, mainGunProjectileVelocity);
                    }
                    foreach (Transform position in leftAngleGunPositionsB)
                    {
                        //GameObject bullet = Instantiate(playerMainWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleBForEuler));
                        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleBForXAxis, mainGunProjectileVelocity);

                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleBForEuler));
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleBForXAxis, mainGunProjectileVelocity);

                    }
                    foreach (Transform position in rightAngleGunPositionsB)
                    {
                        //GameObject bullet = Instantiate(playerMainWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleBForEuler));
                        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleBForXAxis, mainGunProjectileVelocity);
                        

                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleBForEuler));
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleBForXAxis, mainGunProjectileVelocity);
                        
                    }
                    foreach (Transform position in leftAngleGunPositionsC)
                    {
                        //GameObject bullet = Instantiate(playerMainWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleCForEuler));
                        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleCForXAxis, mainGunProjectileVelocity);
                       

                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleCForEuler));
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleCForXAxis, mainGunProjectileVelocity);
                        
                    }
                    foreach (Transform position in rightAngleGunPositionsC)
                    {
                        //GameObject bullet = Instantiate(playerMainWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleCForEuler));
                        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleCForXAxis, mainGunProjectileVelocity);
                        

                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleCForEuler));
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleCForXAxis, mainGunProjectileVelocity);
                        
                    }
                }
                
            }
            if(isSaucer)
            {
                if (mainFiringMode == 1)
                {
                    foreach (Transform position in centerGunPosition)
                    {
                        GameObject projectile1 = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.identity);
                        projectile1.SetActive(true);
                        projectile1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                    }
                    GameObject projectile2 = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, leftSaucerGun.transform.position, Quaternion.identity);
                    projectile2.SetActive(true);
                    projectile2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);

                    GameObject projectile3 = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, rightSaucerGun.transform.position, Quaternion.identity);
                    projectile3.SetActive(true);
                    projectile3.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                }
                if (mainFiringMode == 2)
                {
                    foreach (Transform position in centerGunPosition)
                    {
                        
                    }
                }
            }

            if (isUltDC)
            {
                if (mainFiringMode == 1)
                {
                    //foreach (Transform position in centerGunPosition)
                    //{
                    //    GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.identity);
                    //    projectile.SetActive(true);
                    //    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                    //}
                    foreach (Transform position in gunPositionsB)
                    {
                        GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.identity);
                        projectile.SetActive(true);
                        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                    }
                }
                if (mainFiringMode == 2)
                {
                    foreach (Transform position in centerGunPosition)
                    {
                        GameObject bullet = Instantiate(playerMainWeapon, position.transform.position, Quaternion.identity);
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                        bullet.transform.parent = bulletTransformParent;
                    }
                    foreach (Transform position in gunPositionsB)
                    {
                        GameObject bullet = Instantiate(playerMainWeapon, position.transform.position, Quaternion.identity);
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                        bullet.transform.parent = bulletTransformParent;
                    }
                    //foreach (Transform position in gunPositionsC)
                    //{
                    //    GameObject bullet = Instantiate(level1BulletType, position.transform.position, Quaternion.identity);
                    //    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
                    //    bullet.transform.parent = bulletTransformParent;
                    //}
                }
                
            }
            if(isUltBomber)
            {
                //foreach (Transform position in centerGunPosition)
                //{
                //    GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.identity);
                //    projectile.SetActive(true);
                //    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                //}
                foreach (Transform position in leftAngleGunPositionsA)
                {
                    GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleAForEuler));
                    projectile.SetActive(true);
                    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleAForXAxis, mainGunProjectileVelocity);
                    
                }
                foreach (Transform position in rightAngleGunPositionsA)
                {
                    GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleAForEuler));
                    projectile.SetActive(true);
                    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleAForXAxis, mainGunProjectileVelocity);
                    
                }
                foreach (Transform position in leftAngleGunPositionsB)
                {
                    GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleBForEuler));
                    projectile.SetActive(true);
                    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleBForXAxis, mainGunProjectileVelocity);
                    
                }
                foreach (Transform position in rightAngleGunPositionsB)
                {
                    GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleBForEuler));
                    projectile.SetActive(true);
                    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleBForXAxis, mainGunProjectileVelocity);
                    
                }
                //foreach (Transform position in leftAngleGunPositionsC)
                //{
                //    GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleCForEuler));
                //    projectile.SetActive(true);
                //    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleCForXAxis, mainGunProjectileVelocity);
                    
                //}
                //foreach (Transform position in rightAngleGunPositionsC)
                //{
                //    GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(mainWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleCForEuler));
                //    projectile.SetActive(true);
                //    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleCForXAxis, mainGunProjectileVelocity);
                    
                //}
            }
            if(isUltSaucer)
            {
                if (mainFiringMode == 1)
                {
                    foreach (Transform position in centerGunPosition)
                    {
                        GameObject bullet = Instantiate(FindObjectOfType<PlayerWeaponDatabase>().mainUltSwirlProjectile, position.transform.position, Quaternion.identity);
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                        bullet.transform.parent = bulletTransformParent;
                    }
                }
            }
            if(soundPlayer != null)
            {
                soundPlayer.PlayMainWeaponSound();
            }
            yield return new WaitForSeconds(mainGunFireRate);
        }
    }

    IEnumerator FireSecondaryContinuously()
    {
        while (true && canShoot) // had isSecondaryShooting
        {
            if (isDC1000)
            {
                if (secondaryFiringMode == 1)
                {
                    GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon(secondaryWeapon, centerGunPosition[0].transform.position, Quaternion.identity);
                    projectile.SetActive(true);
                    projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, secondaryGunProjectileVelocity);
                }
            }

            else if (isEXT9)
            {
                if (mainFiringMode == 1)
                {
                    if (playerSecondaryWeapon != FindObjectOfType<PlayerWeaponDatabase>().homingMissileObj &&
                        playerSecondaryWeapon != FindObjectOfType<PlayerWeaponDatabase>().augHomingMissileObj &&
                        playerSecondaryWeapon != FindObjectOfType<PlayerWeaponDatabase>().superHomingMissileObj)
                    {
                        // run regular secondary weaponry through FireContinously function. leave this section for missiles
                    }
                    else
                    {
                        foreach (Transform position in centerGunPosition)
                        {
                            GameObject bullet = Instantiate(playerSecondaryWeapon, position.transform.position, Quaternion.identity);
                            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, secondaryGunProjectileVelocity);
                            bullet.transform.parent = bulletTransformParent;
                        }
                    }

                    //foreach (Transform position in leftAngleGunPositionsB)
                    //{
                    //    GameObject bullet = Instantiate(playerSecondaryWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleBForEuler));
                    //    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleBForXAxis, mainGunProjectileVelocity);
                    //    bullet.transform.parent = bulletTransformParent;
                    //}
                    //foreach (Transform position in rightAngleGunPositionsB)
                    //{
                    //    GameObject bullet = Instantiate(playerSecondaryWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleBForEuler));
                    //    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleBForXAxis, mainGunProjectileVelocity);
                    //    bullet.transform.parent = bulletTransformParent;
                    //}
                }
                if (mainFiringMode == 2)
                {
                    foreach (Transform position in centerGunPosition)
                    {
                        GameObject bullet = Instantiate(playerSecondaryWeapon, position.transform.position, Quaternion.identity);
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, mainGunProjectileVelocity);
                        bullet.transform.parent = bulletTransformParent;
                    }
                    foreach (Transform position in leftAngleGunPositionsA)
                    {
                        GameObject bullet = Instantiate(playerSecondaryWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleAForEuler));
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleAForXAxis, mainGunProjectileVelocity);
                        bullet.transform.parent = bulletTransformParent;
                    }
                    foreach (Transform position in rightAngleGunPositionsA)
                    {
                        GameObject bullet = Instantiate(playerSecondaryWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleAForEuler));
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleAForXAxis, mainGunProjectileVelocity);
                        bullet.transform.parent = bulletTransformParent;
                    }

                    foreach (Transform position in leftAngleGunPositionsB)
                    {
                        GameObject bullet = Instantiate(playerSecondaryWeapon, position.transform.position, Quaternion.Euler(0, 0, leftAngleBForEuler));
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(leftAngleBForXAxis, mainGunProjectileVelocity);
                        bullet.transform.parent = bulletTransformParent;
                    }
                    foreach (Transform position in rightAngleGunPositionsB)
                    {
                        GameObject bullet = Instantiate(playerSecondaryWeapon, position.transform.position, Quaternion.Euler(0, 0, rightAngleBForEuler));
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(rightAngleBForXAxis, mainGunProjectileVelocity);
                        bullet.transform.parent = bulletTransformParent;
                    }
                }
            }

            else if(isSaucer)
            {
                if (mainFiringMode == 1)
                {
                    if (playerSecondaryWeapon != FindObjectOfType<PlayerWeaponDatabase>().homingMissileObj &&
                        playerSecondaryWeapon != FindObjectOfType<PlayerWeaponDatabase>().augHomingMissileObj &&
                        playerSecondaryWeapon != FindObjectOfType<PlayerWeaponDatabase>().superHomingMissileObj)
                    {
                        foreach (Transform position in centerGunPosition)
                        {
                            GameObject bullet = Instantiate(FindObjectOfType<PlayerWeaponDatabase>().secondarySwirlProjectile, position.transform.position, Quaternion.identity);
                            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, secondaryGunProjectileVelocity);
                            bullet.transform.parent = bulletTransformParent;
                        }
                    }
                    else
                    {
                        foreach (Transform position in centerGunPosition)
                        {
                            GameObject bullet = Instantiate(playerSecondaryWeapon, position.transform.position, Quaternion.identity);
                            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, secondaryGunProjectileVelocity);
                            bullet.transform.parent = bulletTransformParent;
                        }
                    }
                }
            }
            else if(isUltDC)
            {
                if (mainFiringMode == 1)
                {
                    if (playerSecondaryWeapon != FindObjectOfType<PlayerWeaponDatabase>().homingMissileObj &&
                        playerSecondaryWeapon != FindObjectOfType<PlayerWeaponDatabase>().augHomingMissileObj &&
                        playerSecondaryWeapon != FindObjectOfType<PlayerWeaponDatabase>().superHomingMissileObj)
                    {
                        foreach (Transform position in gunPositionsC)
                        {
                            GameObject bullet = Instantiate(playerSecondaryWeapon, position.transform.position, Quaternion.identity);
                            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, secondaryGunProjectileVelocity);
                            bullet.transform.parent = bulletTransformParent;
                        }
                    }
                    else 
                    {
                        foreach (Transform position in centerGunPosition)
                        {
                            GameObject bullet = Instantiate(playerSecondaryWeapon, position.transform.position, Quaternion.identity);
                            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, secondaryGunProjectileVelocity);
                            bullet.transform.parent = bulletTransformParent;
                        }
                    }
                }
            }
            else if (isUltBomber)
            {
                if (mainFiringMode == 1)
                {
                    if (playerSecondaryWeapon != FindObjectOfType<PlayerWeaponDatabase>().homingMissileObj &&
                        playerSecondaryWeapon != FindObjectOfType<PlayerWeaponDatabase>().augHomingMissileObj &&
                        playerSecondaryWeapon != FindObjectOfType<PlayerWeaponDatabase>().superHomingMissileObj)
                    {
                        foreach (Transform position in gunPositionsC)
                        {
                            GameObject bullet = Instantiate(playerSecondaryWeapon, position.transform.position, Quaternion.identity);
                            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, secondaryGunProjectileVelocity);
                            bullet.transform.parent = bulletTransformParent;
                        }
                        foreach (Transform position in centerGunPosition)
                        {
                            GameObject bullet = Instantiate(playerSecondaryWeapon, position.transform.position, Quaternion.identity);
                            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, secondaryGunProjectileVelocity);
                            bullet.transform.parent = bulletTransformParent;
                        }
                    }
                    else
                    {
                        foreach (Transform position in centerGunPosition)
                        {
                            GameObject bullet = Instantiate(playerSecondaryWeapon, position.transform.position, Quaternion.identity);
                            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, secondaryGunProjectileVelocity);
                            bullet.transform.parent = bulletTransformParent;
                        }
                    }

                }
            }
            //if (FindObjectOfType<SoundCenter>().CheckPlayerFiringSoundCooldown())
            //{
            //    AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume * PlayerPrefsController.GetMasterSFXVolume());
            //    //Destroy(shootSound, 1.5f);
            //}

            yield return new WaitForSeconds(secondaryGunFireRate);
        }
    }

    public void StartFiringAimedShot()
    {
        StartCoroutine(FireAimedShot());
    }
    public void StopFiringAimedShot()
    {
        StopCoroutine(FireAimedShot());
        aimedShotCannonSprite.color = aimedShotCannonColorInvisible;
    }
    
    public void AddEnemyToTransformList(Transform transformToAdd)
    {
        enemyTargets.Add(transformToAdd);
    }
    public void RemoveEnemyFromTransformList(Transform transformToRemove)
    {
        enemyTargets.Remove(transformToRemove);
    }
    IEnumerator FireAimedShot()
    {
        while (uniqueAbilityIsActive && canShoot)
        {
            if (enemyTargets.Count > 0)
            {
                var aimDirection = (enemyTargets[0].transform.position - transform.position).normalized;
                aimedShotCannonSprite.color = aimedShotCannonColorSolid;
                
                GameObject projectile = objectPooler.SpawnFromPoolPlayerWeapon("playerSuperFireball", aimedShotCannonFirePoint.transform.position, Quaternion.identity);
                projectile.SetActive(true);
                aimedShotCannonArm.transform.up = enemyTargets[0].position - transform.position;
                projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(aimDirection.x * aimedShotVelocityModifier, aimDirection.y * aimedShotVelocityModifier);
                projectile.transform.up = enemyTargets[0].position - transform.position;
                soundPlayer.PlaySoundEffect("playerFireballSound");
            }
            yield return new WaitForSeconds(aimedShotFireRate);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trigger Velocity Slow"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, slowVelocity);
            //Debug.Log("Slow Triggered");
        }
        if (collision.gameObject.CompareTag("Trigger Velocity Fast"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, flyAwaySpeed);
        }
        if (collision.gameObject.CompareTag("Trigger Player Control True"))
        {
            FindObjectOfType<PlayerMovement>().ChangePlayerControl("true");
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(isPlayerInvincible) { return; }
            if(!shieldIsActive && shieldAmount >= 1)
            {
                ActivateShield();
                return;
            }
            if(shieldIsActive) { return; }
            if (collision.gameObject.GetComponent<Enemy>().isEnemy)
            {
                StartCoroutine(cameraShake.Shake(0.15f, .1f));
                playerHealth -= crashDamageAmount;
            }
            if (collision.gameObject.GetComponent<Enemy>().isBoss)
            {
                playerHealth = 0;
                Die();
            }
            if (playerHealth <= 0)
            {
                Die();
            }
        }

        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        if(damageDealer && !shieldIsActive)
        {
            HandleHit(damageDealer);
        }
        
    }

    private void HandleHit(DamageDealer damageDealer)
    {

        if(isPlayerInvincible == true) { return; }
        if(!shieldIsActive && shieldAmount >= 1)
        {
            ActivateShield();
            
            return;
        }
        if(shieldIsActive == true) { return; }

        HitEffect();
        playerHealth -= damageDealer.GetDamage();
        damageDealer.TriggerEffect();
        if (playerHealth <= 0)
        {
            Die();
        }
        damageDealer.TriggerEffect();
    }
    public void TriggerFlyAwaySequence()
    {
        Debug.Log("FlyAwaySequenceTriggered");
        StageCompleteFlight();
        canShoot = false;
        foreach(Collider2D collider in playerColliders)
        {
            collider.enabled = false;
        }
        GetComponent<PlayerMovement>().ChangePlayerControl("false");
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, flyAwaySpeed);
    }
    public void TriggerStartFlyingSequence()
    {
        GetComponent<PlayerMovement>().ChangePlayerControl("false");
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, flyAwaySpeed);
        //shipStartFlyingSequenceStarted = true;
    }
    public void StopFlyAwaySequence()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    public void StageCompleteFlight()
    {
        //GetComponent<Animator>().SetTrigger("FastFlight");
    }
    public bool GetPlayerCanShootStatus()
    {
        return canShoot;
    }

    public void CanShoot(string trueOrFalse)
    {
        if(trueOrFalse == "true")
        {
            canShoot = true;
        }

        if(trueOrFalse == "false")
        {
            canShoot = false;
        }
    }

    private void Die()
    {
        if(!hasPlayerBeenKilled)
        {
            isAlive = false;
            soundPlayer.StopAllPlayerSounds();
            pauseButton.SetActive(false);
            gameSession.StopLiveTimer();
            Time.timeScale = 1f;
            GameObject explosion = Instantiate(deathFX, transform.position, Quaternion.identity);
            Destroy(explosion, explosionDuration);
            soundPlayer.PlayBossDeathFinalSound();
            gameSession.SetNewTopTenScore(gameSession.FindScorePosition(gameSession.score), gameSession.score);
            FindObjectOfType<StageManager>().LoadGameOver();
            gameSession.GameIsNoLongerInSession();
            gameObject.SetActive(false);
            if(FindObjectOfType<EnemyHomingProjectile>())
            {
                FindObjectOfType<EnemyHomingProjectile>().ChangeTargetStatus();
            }
            hasPlayerBeenKilled = true;
        }
        
    }
    IEnumerator LoadGameOverAfterSave()
    {
        
        //FindObjectOfType<GameSession>().LoadSavedGame();

        yield return new WaitForSeconds(3);

        
    }

    private void Idle()
    {
        GetComponent<Animator>().SetTrigger("Flying");
    }

    private void HitEffect()
    {
        if(gameObject.activeInHierarchy)
        {
            StartCoroutine(cameraShake.Shake(0.15f, .1f));
            soundPlayer.PlayPlayerHitSound();
            if (hitEffect == null) { return; }
            {
                hitEffect.SetActive(true);
            }
            if (GetComponent<Animator>() == null) { return; }

            GetComponent<Animator>().SetTrigger("HitEffect");
        }
    }
    // have special boss weapons call this, such as the battle cruiser beam weapon on hit
    public void TakeBossDamage(int damageAmount)
    {
        if(isPlayerInvincible == true)
        { return; }
        if(!shieldIsActive && shieldAmount >= 1)
        {
            ActivateShield();
            return;
        }
        StartCoroutine(cameraShake.Shake(0.15f, .1f));
        if (shieldIsActive) { return; }
        hitEffect.SetActive(true);
        playerHealth -= damageAmount;
        if(playerHealth <= 0)
        {
            Die();
        }
    }

    // beam weapons

    public void ActivateBeamWeapons()
    {
        FindObjectOfType<PlayerWeaponDatabase>().ClearSecondWeapon();
        secondaryFiring = false;
        DeactivateSpinner();
        DeactivateDrones();
        foreach (GameObject beam in beamWeapons)
        {
            beam.SetActive(true);
        }
        beamLevel1Active = true;
    }
    public void DeactivateBeamWeapons()
    {
        foreach (GameObject beam in beamWeapons)
        {
            beam.SetActive(false);
        }
    }
    // shield projectile weapons
    public void ActivateSpinner()
    {
        secondaryFiring = false;
        DeactivateAllBeamWeapons();
        DeactivateDrones();
        spinner.GetComponent<PublicActivator>().Activate();
    }
    public void DeactivateSpinner()
    {
        spinner.GetComponent<PublicActivator>().Deactivate();
        isSpinnerLoaded = false;
    }
    public void DeactivateAllBeamWeapons()
    {
        DeactivateBeamWeapons();
    }
    public void ActivateDrones()
    {
        DeactivateDrones();
        secondaryFiring = false;
        foreach (GameObject drones in drones)
        {
            drones.SetActive(true);
            drones.GetComponentInChildren<PlayerDrone>().hasDroneBeenDestroyed = false;
            drones.GetComponentInChildren<PlayerDrone>().ResetDroneHealth();
        }
        dronesLevel3Active = true;
    }
    public void DeactivateDrones()
    {
        playerDroneControl.DeactivateDrones();
        dronesAreActive = false;
    }
    public void DroneStatus()
    {
        if(dronesAreActive)
        {
            if(crystalCanvas == null)
            {
                crystalCanvas = FindObjectOfType<CrystalCanvas>();
            }
            if (FindObjectOfType<LeftPlayerDrone>() == true)
            {
                isLeftDroneActive = true;
            }
            if (FindObjectOfType<LeftPlayerDrone>() == false)
            {
                isLeftDroneActive = false;
            }
            if (FindObjectOfType<RightPlayerDrone>() == true)
            {
                isRightDroneActive = true;
            }
            if (FindObjectOfType<RightPlayerDrone>() == false)
            {
                isRightDroneActive = false;
            }
        }
    }
    public void FireUltraIceBullet()
    {
        //GameObject uIceBullet = Instantiate(ultraIceBullet, centerGunPosition[0].position, Quaternion.identity);
        //uIceBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, ultraIceBulletVelocity);

        GameObject ultraIceBullet = objectPooler.SpawnFromPoolPlayerWeapon(WeaponDB.ultraIceBulletName, centerGunPosition[0].position, Quaternion.identity);
        ultraIceBullet.SetActive(true);
        ultraIceBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, ultraIceBulletVelocity);
    }
    public void FireUltraPhotonBullet()
    {
        //GameObject uPhotonBullet = Instantiate(ultraPhotonBullet, centerGunPosition[0].position, Quaternion.identity);
        //uPhotonBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, ultraPhotonBulletVelocity);

        GameObject ultraPhotonBullet = objectPooler.SpawnFromPoolPlayerWeapon(WeaponDB.ultraPhotonBulletName, centerGunPosition[0].position, Quaternion.identity);
        ultraPhotonBullet.SetActive(true);
        ultraPhotonBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, ultraPhotonBulletVelocity);
        if(soundPlayer != null)
        {
            soundPlayer.PlaySoundEffect("playerUltraPhotonSound");
        }
    }
    public void SetShipUniqueAbility()
    {
        if(isDCUniqueAbility)
        {
            FindObjectOfType<PurpleButton>().isDCUnique = true;
            isUniqueAbilityLoaded = true;
        }
        if(isEXTUniqueAbility)
        {
            FindObjectOfType<PurpleButton>().isEXTUnique = true;
            isUniqueAbilityLoaded = true;
        }
        if(isSaucerUniqueAbility)
        {
            FindObjectOfType<PurpleButton>().isSaucerUnique = true;
            isUniqueAbilityLoaded = true;
        }
        if(isUltDCUniqueAbility)
        {
            FindObjectOfType<PurpleButton>().isUltDCUnique = true;
            isUniqueAbilityLoaded = true;
        }
    }
    public void ActivateDCUniqueAbility()
    {
        shieldAmount += 1;
        UpdateShieldIcon();
    }
    public void DeactivateDCUniqueAbility()
    {
        dCSpecialBody.SetActive(false);
        isPlayerInvincible = false;
        uniqueAbilityIsActive = false;
    }
    public void ActivateEXTUniqueAbility()
    {
        mainFiringMode = 2;
        uniqueAbilityIsTimed = true;
        uniqueAbilityIsActive = true;
    }
    public void ActivateSaucerUniqueAbility()
    {
        foreach(GameObject orb in saucerAttackOrbs)
        {
            GameObject attackOrb = Instantiate(orb, transform.position, Quaternion.identity);
            attackOrb.SetActive(true);
        }
        //uniqueAbilityIsTimed = true;
        //uniqueAbilityIsActive = true;
    }
    public void DeactivateEXTUNiqueAbility()
    {
        mainFiringMode = 1;
        uniqueAbilityIsActive = false;
        uniqueAbilityIsTimed = false;
    }
    public void ActivateUltDCUniqueAbility()
    {
        uniqueAbilityIsTimed = true;
        uniqueAbilityIsActive = true;
        StartFiringAimedShot();
    }
    public void DeactivateUltDCUniqueAbility()
    {
        StopFiringAimedShot();
        uniqueAbilityIsActive = false;
        uniqueAbilityIsTimed = false;
    }
    public void ActivateTimedSecondaryWeapon()
    {
        if (isBeamWeaponLoaded)
        {
            ActivateBeamWeapons();
        }
        if (dronesLevel1Active)
        {
            ActivateDrones();
        }
        temporarySecondaryWeaponActive = true;
    }
    public void ActivateProximityMines()
    {
        GameObject mine1 = objectPooler.SpawnFromPoolPlayerWeapon(WeaponDB.proximityMineName, transform.position, Quaternion.identity);
        mine1.SetActive(true);
        mine1.GetComponent<Rigidbody2D>().velocity = new Vector2(-6, 6);

        GameObject mine2 = objectPooler.SpawnFromPoolPlayerWeapon(WeaponDB.proximityMineName, transform.position, Quaternion.identity);
        mine2.SetActive(true);
        mine2.GetComponent<Rigidbody2D>().velocity = new Vector2(6, 6);

        GameObject mine3 = objectPooler.SpawnFromPoolPlayerWeapon(WeaponDB.proximityMineName, transform.position, Quaternion.identity);
        mine3.SetActive(true);
        mine3.GetComponent<Rigidbody2D>().velocity = new Vector2(-6, -6);

        GameObject mine4 = objectPooler.SpawnFromPoolPlayerWeapon(WeaponDB.proximityMineName, transform.position, Quaternion.identity);
        mine4.SetActive(true);
        mine4.GetComponent<Rigidbody2D>().velocity = new Vector2(6, -6);
    }
    public void ActivateRocket()
    {
        GameObject rocket = objectPooler.SpawnFromPoolPlayerWeapon(WeaponDB.rocketName, transform.position, Quaternion.identity);
        rocket.SetActive(true);
        
    }
    public void ActivateHomingMissile()
    {
        GameObject homingMissile = objectPooler.SpawnFromPoolPlayerWeapon(WeaponDB.superHomingMissileName, transform.position, Quaternion.identity);
        homingMissile.SetActive(true);
        homingMissile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, WeaponDB.homingMissileVelocity);
    }
    public void DeactivateAllSecondaryWeapons()
    {
        isHomingMissileLoaded = false;
        isBeamWeaponLoaded = false;
        isSpinnerLoaded = false;
        isDroneLoaded = false;
        isRocketLoaded = false;
        isProximityMineLoaded = false;
        StopSecondWeaponFiring();
        DeactivateAllBeamWeapons();
        DeactivateDrones();
        DeactivateSpinner();
    }
    public void StopPlayerCoroutines()
    {
        StopCoroutine(FireContinously());
        StopCoroutine(FireAimedShot());
    }
}
