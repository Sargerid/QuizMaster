using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [Header("Canvas Handler")]
    [SerializeField] private GameObject quizCanvas;
    [SerializeField] private GameObject leveListCanvas;

    [SerializeField] string menuSceneName;
    
    private GameManager gameManager;
    private Timer timer;
    private Quiz quizComponent;
    private ScoreKeeper scoreKeeper;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        timer = FindObjectOfType<Timer>();
        quizComponent = quizCanvas.GetComponent<Quiz>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();

    }

    // Start is called before the first frame update
    void Start()
    {
        timer.enabled = false;
        quizCanvas.SetActive(false);
        leveListCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(Button clickedButton)
    {
        AudioManager.instance.PlaySFX(1);
        quizComponent.currentButton = clickedButton;
        scoreKeeper.ResetCorrectAnswers();
        quizComponent.progressBar.value = 0;
        quizComponent.isComplete = false;
        timer.enabled = true;
        quizComponent.questions.Clear();
        LevelInfoContainer levelInfo = clickedButton.GetComponent<LevelInfoContainer>();
        quizComponent.questions.AddRange(levelInfo.questionsOnThisLevel);
        quizCanvas.SetActive(true);
        leveListCanvas.SetActive(false);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(menuSceneName,LoadSceneMode.Single);
    }
}
