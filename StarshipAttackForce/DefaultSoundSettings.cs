using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSoundSettings : MonoBehaviour
{
    [SerializeField] float defaultSFXVolume = 1f;
    [SerializeField] float defaultMusicVolume = 0.60f;

    public float GetDefaultSFXVolume()
    {
        return defaultSFXVolume;
    }

    public float GetDefaultMusicVolume()
    {
        return defaultMusicVolume;
    }
}
