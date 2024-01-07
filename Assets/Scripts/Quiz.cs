using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    public  List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;
    
    
    [Header("Answers")]
    [SerializeField] private GameObject[] answerButtons; 
    private int correctAnswerIndex;
    private bool hasAnsweredEarly = true;
    
    [Header("Button Colors")]
    [SerializeField] private Sprite defaultAnswerSprite;
    [SerializeField] private Sprite correctAnswerSprite;
    
    [Header("Timer")]
    [SerializeField] Image timerImage;
    private Timer timer;

    [Header("Progress Bar")]
    public Slider progressBar;
    public bool isComplete;
    [SerializeField] private ParticleSystem[] confettiEffect;

    [Header("Level List Button Status")] 
    public Button currentButton;

    [SerializeField] Sprite zeroStars;
    [SerializeField] Sprite oneStar;
    [SerializeField] Sprite twoStars;
    [SerializeField] Sprite threeStars;
    [SerializeField] Sprite fourStars;
    [SerializeField] Sprite fiveStars;
    
    [Header("Scoring")] [SerializeField] private TextMeshProUGUI scoreText;
    private ScoreKeeper scoreKeeper;
    private int totalScore ;
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        timer = FindObjectOfType<Timer>();
        //progressBar.maxValue = questions.Count;
        progressBar.maxValue = 5;
        progressBar.value = 0;
    }

    

    private void Update()
    {
        totalScore = scoreKeeper.GetCorrectAnswers();
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                AudioManager.instance.PlaySFX(2);
                for (int i = 0; i < confettiEffect.Length; i++)
                {
                    confettiEffect[i].Play();
                }
                Image currentButtonImageElement = currentButton.GetComponent<Image>();
                //change button color/sprite
                if (totalScore == 0)
                {
                    currentButtonImageElement.sprite = zeroStars;
                }
                else if (totalScore == 1)
                {
                    currentButtonImageElement.sprite = oneStar;
                }
                else if (totalScore == 2)
                {
                    currentButtonImageElement.sprite = twoStars;
                }
                else if (totalScore == 3)
                {
                    currentButtonImageElement.sprite = threeStars;
                }
                else if (totalScore == 4)
                {
                    currentButtonImageElement.sprite = fourStars;
                }
                else if (totalScore == 5)
                {
                    currentButtonImageElement.sprite = fiveStars;
                }
                //currentButton.image.color = Color.green;
                return;
            }
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = scoreKeeper.GetCorrectAnswers().ToString()+"/5";
        
    }


    void DisplayAnswer(int index)
    {
        Image buttonImage;
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            AudioManager.instance.PlaySFX(4);
            questionText.text = "CORRECT!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            AudioManager.instance.PlaySFX(3);
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "WRONG ANSWER";
            //questionText.text = "Sorry, the correct answer was: \n" + correctAnswer;
            //buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            //buttonImage.sprite = correctAnswerSprite;

        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();    
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
        
    }
    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    
}
