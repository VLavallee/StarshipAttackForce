using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Slider sFXVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;

    [SerializeField] Slider difficultySlider;
    [SerializeField] float defaultSFXVolume = 0.75f;
    [SerializeField] float defaultMusicVolume = 0.75f;
    [SerializeField] float defaultDifficulty = 1f;
    void Start()
    {
        sFXVolumeSlider.value = PlayerPrefsController.GetMasterSFXVolume();
        musicVolumeSlider.value = PlayerPrefsController.GetMasterMusicVolume();
    }

    void Update()
    {
        var soundPlayer = FindObjectOfType<SoundPlayer>();

        if (soundPlayer)
        {
            soundPlayer.SetSFXVolume(sFXVolumeSlider.value);
            soundPlayer.SetMusicVolume(musicVolumeSlider.value);
        }
        else
        {
            Debug.LogWarning("No music player found.. did you start from splash screen?");
        }
    }
    public void SaveAndExit()
    {
        PlayerPrefsController.SetMasterSFXVolume(sFXVolumeSlider.value);
        PlayerPrefsController.SetMasterMusicVolume(musicVolumeSlider.value);
        //FindObjectOfType<SoundPlayer>().GetVolumeSettings();
        FindObjectOfType<StageManager>().LoadMainMenu();
    }

    public void SetDefaults()
    {
        var soundPlayer = FindObjectOfType<SoundPlayer>();

        soundPlayer.SetSFXVolume(defaultSFXVolume);
        soundPlayer.SetMusicVolume(defaultMusicVolume);

        sFXVolumeSlider.value = defaultSFXVolume;
        musicVolumeSlider.value = defaultSFXVolume;

        

        PlayerPrefsController.SetMasterSFXVolume(defaultSFXVolume);
        PlayerPrefsController.SetMasterMusicVolume(defaultMusicVolume);
        PlayerPrefsController.SetDifficulty(defaultDifficulty);
    }
}
