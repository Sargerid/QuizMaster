using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu Canvas Handler")]
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject mainMenuCanvas;
    [Header("Scenes")]
    [SerializeField] string nameT1;
    
    private void Start()
    {
        settingsCanvas.SetActive(false);
        
    }

    public void ToPlay()
    {
        AudioManager.instance.PlaySFX(0);
        mainMenuCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
    }
    public void ToSettings()
    {
        AudioManager.instance.PlaySFX(0);
        settingsCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
        
    }
    public void ToExit()
    {
        AudioManager.instance.PlaySFX(0);
        Application.Quit();
    }

    public void ToMainMenu()
    {
        AudioManager.instance.PlaySFX(0);
        settingsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
        
    }

    public void ToT1()
    {
        SceneManager.LoadScene(nameT1, LoadSceneMode.Single);
    }
}
