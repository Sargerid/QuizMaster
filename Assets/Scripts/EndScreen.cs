using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScore;

    private ScoreKeeper scoreKeeper;
    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    
    public void ShowFinalScore()
    {
        finalScore.text = "Congratulations!\nYour score is:" + scoreKeeper.GetCorrectAnswers() + "/5";
    }
    
}
