using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    AudioSource playerShipMainWeaponAudioSource, playerShipSecondaryWeaponAudioSource, playerShipSpecialAudioSource, playerShieldAudioSource, playerHitSoundAudioSource,
        ambientSoundAudioSource, musicAudioSource, uIAudioSource, upgradeAudioSource, shipTakeOffAudioSource, startSoundAudioSource, buySoundAudioSource;

    [SerializeField] AudioSource expAudioSource_0, expAudioSource_1, expAudioSource_2, bossDeathFinalSoundAudioSource,
        enemyWeaponAudioSource_1, enemyWeaponAudioSource_2, enemyWeaponAudioSource_3, enemyWeaponAudioSource_4, enemyAudioSource_1, enemyAudioSource_2;

    [SerializeField] AudioSource collectionAudioSource_1, collectionAudioSource_2, collectionAudioSource_3, collectionAudioSource_4;

    [SerializeField] DefaultSoundSettings defaultSoundSettings;

    [SerializeField]
    AudioClip flyAwaySound, fireballSound, superFireballSound, photonSound, ultraPhotonSound, beamWeaponSound, bladeSound, superBladeSound, shockSphereSound,
        superShockSphereSound, iceBulletSound, shipFlightSound, homingMissileSound, proximityMineSound, ultraIceBulletSound, energyBurstSound;

    [SerializeField]
    AudioClip enemyBeamSound, enemyHomingMissileSound, enemyIceBulletSound, enemyFireballSound, enemyRocketSound, enemyPhotonSound, enemyDropWeaponSound,
        enemyShockSphereSound, enemyProximityMineSound;

    [SerializeField] AudioClip blueCrystalSound, orangeCrystalSound, purpleCrystalSound, upgradeCoreSound, gameOverSound;
    [SerializeField] AudioClip shieldSound;
    [SerializeField] AudioClip UI_click, UI_down, UI_up;
    [SerializeField] float flyAwaySoundVolume, weaponVolume, shipFlightVolume;
    [SerializeField] int selectedExplosion;

    [SerializeField] float playerMainWepSFXVolMultiplier, playerSecWepSFXVolMultiplier, shieldSFXVolMultiplier, explosionSFXVolMultiplier, ambientEngineSFXVolMultiplier,
        uiSFXVolMultiplier, upgradeSFXVolMultiplier, musicVolMultiplier, enemyWepSFXMultiplier, shipTakeOffVolMultiplier;
    GameSession gameSession;
    [SerializeField] bool gameSessionChecked;
    private void Awake()
    {
        SetUpSingleton();
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        StartCoroutine(CheckForTimesSavedAndImplementSoundSettings());
    }
    IEnumerator CheckForTimesSavedAndImplementSoundSettings()
    {
        // This coroutine checks gameSession for how many times the game has been saved. gameSession pulls the data from the PlayerData script with the function LoadSavedGame(); 
        // if the game has been saved 0 times, it must be a new playthrough, and in that case the default sound values are set. After the first save it will always apply the user saved values.
        gameSession = FindObjectOfType<GameSession>();
        yield return new WaitUntil(() => gameSession.savedGameLoaded = true);
        if (gameSession.GetTotalGameSaves() == 0)
        {
            gameSession.isMusicOn = true;
            gameSession.isSfxOn = true;
            PlayerPrefsController.SetMasterDefaultSoundValues();
            GetVolumeSettings();
            Debug.Log("Number of game saves is Zero. Default sound settings have been implemented!");
            gameSessionChecked = true;
            PlayMusic();
        }
        else if(gameSession.GetTotalGameSaves() > 0)
        {
            GetVolumeSettings();
            Debug.Log("Number of saves is " + gameSession.GetTotalGameSaves() + ". Sfx volume set to " + PlayerPrefsController.GetMasterSFXVolume()
                + " & music volume set to " + PlayerPrefsController.GetMasterMusicVolume());
            gameSessionChecked = true;
            PlayMusic();
        }
    }
    
    public void GetVolumeSettings()
    {
        // Music
        musicAudioSource.volume = PlayerPrefsController.GetMasterMusicVolume();

        //TEST AREA
        //playerShipMainWeaponAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
        //playerShieldAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
        //playerShipSpecialAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
        //playerShipSecondaryWeaponAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
        //enemyWeaponAudioSource_1.volume = PlayerPrefsController.GetMasterSFXVolume();
        //enemyWeaponAudioSource_2.volume = PlayerPrefsController.GetMasterSFXVolume();
        //enemyWeaponAudioSource_3.volume = PlayerPrefsController.GetMasterSFXVolume();
        //enemyWeaponAudioSource_4.volume = PlayerPrefsController.GetMasterSFXVolume();
        //expAudioSource_0.volume = PlayerPrefsController.GetMasterSFXVolume();
        //expAudioSource_1.volume = PlayerPrefsController.GetMasterSFXVolume();
        //expAudioSource_2.volume = PlayerPrefsController.GetMasterSFXVolume();
        //playerHitSoundAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
        //ambientSoundAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
        //uIAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
        //upgradeAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
        //startSoundAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
        //buySoundAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
        //collectionAudioSource_1.volume = PlayerPrefsController.GetMasterSFXVolume();
        //collectionAudioSource_2.volume = PlayerPrefsController.GetMasterSFXVolume();
        //collectionAudioSource_3.volume = PlayerPrefsController.GetMasterSFXVolume();
        //collectionAudioSource_4.volume = PlayerPrefsController.GetMasterSFXVolume();
        //bossDeathFinalSoundAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
        //shipTakeOffAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();

        // SFX
        if (playerMainWepSFXVolMultiplier <= 1)
        {
            playerShipMainWeaponAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume() * playerMainWepSFXVolMultiplier;

        }
        else if (playerMainWepSFXVolMultiplier > 1)
        {
            playerShipMainWeaponAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();

        }//
        if (shieldSFXVolMultiplier <= 1)
        {
            playerShieldAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume() * playerSecWepSFXVolMultiplier;
            playerShipSpecialAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume() * shieldSFXVolMultiplier;
        }
        else if (shieldSFXVolMultiplier > 1)
        {
            playerShieldAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
            playerShipSpecialAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
            //
        }
        if (playerSecWepSFXVolMultiplier <= 1)
        {
            playerShipSecondaryWeaponAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume() * playerSecWepSFXVolMultiplier;
        }
        else if (playerSecWepSFXVolMultiplier > 1)
        {
            playerShipSecondaryWeaponAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
        }//
        if (enemyWepSFXMultiplier <= 1)
        {
            enemyWeaponAudioSource_1.volume = PlayerPrefsController.GetMasterSFXVolume() * enemyWepSFXMultiplier;
            enemyWeaponAudioSource_2.volume = PlayerPrefsController.GetMasterSFXVolume() * enemyWepSFXMultiplier;
            enemyWeaponAudioSource_3.volume = PlayerPrefsController.GetMasterSFXVolume() * enemyWepSFXMultiplier;
            enemyWeaponAudioSource_4.volume = PlayerPrefsController.GetMasterSFXVolume() * enemyWepSFXMultiplier;
        }
        else if (enemyWepSFXMultiplier > 1)
        {
            enemyWeaponAudioSource_1.volume = PlayerPrefsController.GetMasterSFXVolume();
            enemyWeaponAudioSource_2.volume = PlayerPrefsController.GetMasterSFXVolume();
            enemyWeaponAudioSource_3.volume = PlayerPrefsController.GetMasterSFXVolume();
            enemyWeaponAudioSource_4.volume = PlayerPrefsController.GetMasterSFXVolume();
            //
        }
        if (explosionSFXVolMultiplier <= 1)
        {
            expAudioSource_0.volume = PlayerPrefsController.GetMasterSFXVolume() * explosionSFXVolMultiplier;
            expAudioSource_1.volume = PlayerPrefsController.GetMasterSFXVolume() * explosionSFXVolMultiplier;
            expAudioSource_2.volume = PlayerPrefsController.GetMasterSFXVolume() * explosionSFXVolMultiplier;
            bossDeathFinalSoundAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume() * explosionSFXVolMultiplier;
            playerHitSoundAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume() * explosionSFXVolMultiplier;
        }
        else if (explosionSFXVolMultiplier > 1)
        {
            expAudioSource_0.volume = PlayerPrefsController.GetMasterSFXVolume();
            expAudioSource_1.volume = PlayerPrefsController.GetMasterSFXVolume();
            expAudioSource_2.volume = PlayerPrefsController.GetMasterSFXVolume();

            playerHitSoundAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
            //
        }
        if (ambientEngineSFXVolMultiplier <= 1)
        {
            ambientSoundAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume() * ambientEngineSFXVolMultiplier;
        }
        else if (ambientEngineSFXVolMultiplier > 1)
        {
            ambientSoundAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
            //
        }
        if (uiSFXVolMultiplier <= 1)
        {
            uIAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume() * uiSFXVolMultiplier;
            upgradeAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume() * uiSFXVolMultiplier;
            startSoundAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume() * uiSFXVolMultiplier;
            buySoundAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume() * uiSFXVolMultiplier;
        }
        else if (uiSFXVolMultiplier > 1)
        {
            uIAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
            upgradeAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
            startSoundAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
            buySoundAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
        }
        if (upgradeSFXVolMultiplier <= 1)
        {
            collectionAudioSource_1.volume = PlayerPrefsController.GetMasterSFXVolume() * upgradeSFXVolMultiplier;
            collectionAudioSource_2.volume = PlayerPrefsController.GetMasterSFXVolume() * upgradeSFXVolMultiplier;
            collectionAudioSource_3.volume = PlayerPrefsController.GetMasterSFXVolume() * upgradeSFXVolMultiplier;
            collectionAudioSource_4.volume = PlayerPrefsController.GetMasterSFXVolume() * upgradeSFXVolMultiplier;
            bossDeathFinalSoundAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume() * upgradeSFXVolMultiplier;
        }
        else if (upgradeSFXVolMultiplier > 1)
        {
            collectionAudioSource_1.volume = PlayerPrefsController.GetMasterSFXVolume();
            collectionAudioSource_2.volume = PlayerPrefsController.GetMasterSFXVolume();
            collectionAudioSource_3.volume = PlayerPrefsController.GetMasterSFXVolume();
            collectionAudioSource_4.volume = PlayerPrefsController.GetMasterSFXVolume();
            bossDeathFinalSoundAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
        }
        if (shipTakeOffVolMultiplier <= 1)
        {
            shipTakeOffAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume() * shipTakeOffVolMultiplier;
        }
        else if (shipTakeOffVolMultiplier > 1)
        {
            shipTakeOffAudioSource.volume = PlayerPrefsController.GetMasterSFXVolume();
        }
    }
    public void ChangeWeaponSound(string weaponName)
    {
        if(weaponName == "fireball")
        {
            playerShipMainWeaponAudioSource.clip = fireballSound;
        }
        if (weaponName == "superFireball")
        {
            playerShipMainWeaponAudioSource.clip = superFireballSound;
        }
        if (weaponName == "photon")
        {
            playerShipMainWeaponAudioSource.clip = photonSound;
        }
        if(weaponName == "ultraPhoton")
        {
            playerShipSecondaryWeaponAudioSource.clip = ultraPhotonSound;
        }
        if (weaponName == "shockSphere")
        {
            playerShipMainWeaponAudioSource.clip = shockSphereSound;
        }
        if (weaponName == "superShockSphere")
        {
            playerShipMainWeaponAudioSource.clip = superShockSphereSound;
        }
        if(weaponName == "iceBullet")
        {
            playerShipMainWeaponAudioSource.clip = iceBulletSound;
        }
        if (weaponName == "beamWeapon")
        {
            playerShipSecondaryWeaponAudioSource.clip = beamWeaponSound;
        }
        if(weaponName == "homingMissile")
        {
            playerShipSecondaryWeaponAudioSource.clip = homingMissileSound;
        }
        if(weaponName == "blade")
        {
            playerShipMainWeaponAudioSource.clip = bladeSound;
        }
        if(weaponName == "superBlade")
        {
            playerShipMainWeaponAudioSource.clip = superBladeSound;
        }
    }
    public void PlayStartSound()
    {
        if(gameSession.isSfxOn == false)
        {
            return;
        }
        startSoundAudioSource.Play();
    }
    public void PlayBuySound()
    {
        if (gameSession.isSfxOn == false)
        {
            return;
        }
        buySoundAudioSource.Play();
    }
    public void PlayPlayerHitSound()
    {
        if (gameSession.isSfxOn == false)
        {
            return;
        }
        playerHitSoundAudioSource.Play();
    }
    public void PlayUISound(string name)
    {
        if (gameSession.isSfxOn == false)
        {
            return;
        }
        if (name == "click")
        {
            uIAudioSource.clip = UI_click;
            uIAudioSource.Play();
        }
        if(name == "down")
        {
            uIAudioSource.clip = UI_down;
            uIAudioSource.Play();
        }
        if (name == "up")
        {
            uIAudioSource.clip = UI_up;
            uIAudioSource.Play();
        }
    }
    public void PlayMainWeaponSound()
    {
        if (gameSession.isSfxOn == false)
        {
            return;
        }
        playerShipMainWeaponAudioSource.Play();
    }
    public void PlaySecondaryWeaponSound()
    {
        if (gameSession.isSfxOn == false)
        {
            return;
        }
        playerShipSecondaryWeaponAudioSource.Play();
    }
    public void PlaySoundEffect(string clipName)
    {
        if (gameSession.isSfxOn == false)
        {
            return;
        }
        // PLAYER SOUNDS
        if (clipName == "shipTakeOffSound")
        {
            shipTakeOffAudioSource.clip = flyAwaySound;
            shipTakeOffAudioSource.Play();
        }
        if(clipName == "playerFireballSound")
        {
            playerShipMainWeaponAudioSource.clip = fireballSound;
            playerShipMainWeaponAudioSource.Play();
        }
        if (clipName == "playerPhotonSound")
        {
            playerShipMainWeaponAudioSource.clip = photonSound;
            playerShipMainWeaponAudioSource.Play();
        }
        if (clipName == "playerUltraPhotonSound")
        {
            playerShipSecondaryWeaponAudioSource.clip = ultraPhotonSound;
            playerShipSecondaryWeaponAudioSource.Play();
        }
        if (clipName == "playerBeamWeaponSound")
        {
            playerShipSecondaryWeaponAudioSource.clip = beamWeaponSound;
            playerShipSecondaryWeaponAudioSource.Play();
        }
        if (clipName == "playerShipFlightSound")
        {
            ambientSoundAudioSource.clip = shipFlightSound;
            ambientSoundAudioSource.volume = 0.15f;
            ambientSoundAudioSource.Play();
        }
        if(clipName == "playerShieldSound")
        {
            playerShieldAudioSource.clip = shieldSound;
            playerShieldAudioSource.Play();
        }
        if(clipName == "blueCrystalSound")
        {
            upgradeAudioSource.clip = blueCrystalSound;
            upgradeAudioSource.Play();
        }
        if (clipName == "orangeCrystalSound")
        {
            upgradeAudioSource.clip = orangeCrystalSound;
            upgradeAudioSource.Play();
        }
        if (clipName == "purpleCrystalSound")
        {
            upgradeAudioSource.clip = purpleCrystalSound;
            upgradeAudioSource.Play();
        }
        if (clipName == "upgradeCoreSound")
        {
            upgradeAudioSource.clip = upgradeCoreSound;
            upgradeAudioSource.Play();
        }
        if(clipName == "Impact_small")
        {
            enemyAudioSource_1.Play();
        }
        if(clipName == "playerHomingMissileSound")
        {
            playerShipSecondaryWeaponAudioSource.clip = homingMissileSound;
            playerShipSecondaryWeaponAudioSource.Play();
        }
        if(clipName == "playerUltraIceBulletSound")
        {
            playerShipSecondaryWeaponAudioSource.clip = ultraIceBulletSound;
            playerShipSecondaryWeaponAudioSource.Play();
        }
        if(clipName == "playerProximityMineSound")
        {
            playerShipSecondaryWeaponAudioSource.clip = proximityMineSound;
            playerShipSecondaryWeaponAudioSource.Play();
        }
        if(clipName == "energyBurstSound")
        {
            playerShipSpecialAudioSource.clip = energyBurstSound;
            playerShipSpecialAudioSource.Play();
        }
        if(clipName == "playerBladeSound")
        {
            playerShipMainWeaponAudioSource.clip = bladeSound;
            playerShipMainWeaponAudioSource.Play();
        }
        if(clipName == "playerSuperBladeSound")
        {
            playerShipMainWeaponAudioSource.clip = superBladeSound;
            playerShipMainWeaponAudioSource.Play();

        }

        //ENEMY SOUNDS
        if(clipName == "enemyBeamSound")
        {
            enemyWeaponAudioSource_3.clip = enemyBeamSound;
            enemyWeaponAudioSource_3.Play();
        }
        if(clipName == "enemyFireballSound")
        {
            enemyWeaponAudioSource_1.clip = enemyFireballSound;
            enemyWeaponAudioSource_1.Play();
        }
        if (clipName == "enemyHomingMissileSound")
        {
            enemyWeaponAudioSource_3.clip = enemyHomingMissileSound;
            enemyWeaponAudioSource_3.Play();
        }
        if (clipName == "enemyRocketSound")
        {
            enemyWeaponAudioSource_3.clip = enemyRocketSound;
            enemyWeaponAudioSource_3.Play();
        }
        if (clipName == "enemyPhotonSound")
        {
            enemyWeaponAudioSource_4.clip = enemyPhotonSound;
            enemyWeaponAudioSource_4.Play();
        }
        if(clipName == "enemyDropWeaponSound")
        {
            enemyWeaponAudioSource_3.clip = enemyDropWeaponSound;
            enemyWeaponAudioSource_3.Play();
        }
        if(clipName == "enemyIceBulletSound")
        {
            enemyWeaponAudioSource_1.clip = enemyIceBulletSound;
            enemyWeaponAudioSource_1.Play();
        }
        if(clipName == "enemyShockSphereSound")
        {
            enemyWeaponAudioSource_3.clip = enemyShockSphereSound;
            enemyWeaponAudioSource_3.Play();
        }
        if(clipName == "enemyProximityMineSound")
        {
            enemyWeaponAudioSource_1.clip = enemyProximityMineSound;
            enemyWeaponAudioSource_1.Play();
        }
        // OTHER
        //if(clipName == "gameOverSound")
        //{
        //    uIAudioSource.clip = gameOverSound;
        //    uIAudioSource.Play();
        //}
    }
    public void PlayRandomExplosion()
    {
        if (gameSession.isSfxOn == false)
        {
            return;
        }
        var randomRoll = Random.Range(0, 1);
        if (randomRoll == 0)
        {
            PlayExplosionSFX_0();
        }
        if (randomRoll == 1)
        {
            PlayExplosionSFX_1();
        }
        if (randomRoll == 2)
        {
            PlayExplosionSFX_2();
        }
        return;
    }
    public void PlayExplosionSFX_0()
    {
        if (gameSession.isSfxOn == false)
        {
            return;
        }
        expAudioSource_0.Play();
    }
    public void PlayExplosionSFX_1()
    {
        if (gameSession.isSfxOn == false)
        {
            return;
        }
        expAudioSource_1.Play();
    }
    public void PlayExplosionSFX_2()
    {
        if (gameSession.isSfxOn == false)
        {
            return;
        }
        expAudioSource_2.Play();
    }
    public void PlayBossDeathFinalSound()
    {
        if (gameSession.isSfxOn == false)
        {
            return;
        }
        bossDeathFinalSoundAudioSource.Play();
    }
    public void PlayCollectionSoundOne()
    {
        if (gameSession.isSfxOn == false)
        {
            return;
        }
        collectionAudioSource_1.Play();
    }
    public void PlayCollectionSoundTwo()
    {
        if (gameSession.isSfxOn == false)
        {
            return;
        }
        collectionAudioSource_2.Play();
    }
    public void PlayCollectionSoundThree()
    {
        if (gameSession.isSfxOn == false)
        {
            return;
        }
        collectionAudioSource_3.Play();
    }
    public void PlayCollectionSoundFour()
    {
        if (gameSession.isSfxOn == false)
        {
            return;
        }
        collectionAudioSource_4.Play();
    }
    public void StopAllPlayerSounds()
    {
        if (gameSession.isSfxOn == false)
        {
            return;
        }
        ambientSoundAudioSource.Stop();
        playerShipMainWeaponAudioSource.Stop();
        playerShipSecondaryWeaponAudioSource.Stop();
        playerShieldAudioSource.Stop();
        playerShipSpecialAudioSource.Stop();
        upgradeAudioSource.Stop();
    }
    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetSFXVolume(float volume)
    {
        //set this later
        //sFXAudioSource.volume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        musicAudioSource.volume = volume;
    }
    public void SetMusicPitch(float pitchValue)
    {
        if(pitchValue > 0.499f && pitchValue < 1.51f)
        {
            musicAudioSource.pitch = pitchValue;
        }
    }
    public void PlayMusic()
    {
        if (gameSession.isMusicOn == false)
        {
            return;
        }
        musicAudioSource.Play();
    }
    public void StopMusic()
    {
        musicAudioSource.Stop();
    }

    public void SetDefaults()
    {
        //SetSFXVolume(defaultSoundSettings.GetDefaultSFXVolume());
        SetMusicVolume(defaultSoundSettings.GetDefaultMusicVolume());


        //PlayerPrefsController.SetMasterSFXVolume(defaultSoundSettings.GetDefaultSFXVolume());
        PlayerPrefsController.SetMasterMusicVolume(defaultSoundSettings.GetDefaultMusicVolume());
        //PlayerPrefsController.SetDifficulty(defaultDifficulty);
    }
}
