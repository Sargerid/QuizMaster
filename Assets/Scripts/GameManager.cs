using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Quiz quiz;
    [SerializeField] EndScreen endScreen;
    [SerializeField] private GameObject leveListScreen;
    [SerializeField] private GameObject endScreenCanvas;

    
    private Timer timer;
    
    
    
    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //quiz.gameObject.SetActive(true);
        //endScreen.gameObject.SetActive(false);
        endScreenCanvas.SetActive(false);
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
        }
    }

    public void ToNextLevel()
    {
        
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToLevelSelect()
    {
        AudioManager.instance.PlaySFX(0);
        timer.ResetTimer();
        timer.enabled = false;
        quiz.isComplete = false;
        endScreenCanvas.SetActive(false);
        leveListScreen.SetActive(true);
    }

    
}
