using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("FX")]
    public AudioSource[] soundEffects;

    [Header("OST")] 
    public AudioSource[] ost;

    public  bool isTurnedOn = true;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Load  saved sound state
        isTurnedOn = PlayerPrefs.GetInt("IsMusicOn", 1) == 1 ? true : false;
    }

    public void PlaySFX(int sound)
    {
        if (isTurnedOn)
        {
            soundEffects[sound].Stop();
            soundEffects[sound].Play();
        }  
    }

    public void PlaySoundtrack(int sound)
    {
        if (isTurnedOn)
        {
            ost[sound].Stop();
            ost[sound].Play();
        }  
    }
    
    public void StopAllSounds()
    {
        foreach (var fx in soundEffects)
        {
            if (fx.isPlaying)
            {
                fx.Stop();
            }
        }

        foreach (var music in ost)
        {
            if (music.isPlaying)
            {
                music.Stop();
            }
        }
    }

    public void StopSoundtrack()
    {
        foreach (var music in ost)
        {
            if (music.isPlaying)
            {
                music.Stop();
            }
        }
    }

    public void StopMusic()
    {
        isTurnedOn = !isTurnedOn; 

        // Save sound state
        PlayerPrefs.SetInt("IsMusicOn", isTurnedOn ? 1 : 0);
        PlayerPrefs.Save();

        if (!isTurnedOn) 
        {
            StopAllSounds();
        }
    }
}