using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreText : MonoBehaviour
{
    private Text highScore;
    private Text score;
    private void Awake()
    {
        score = transform.Find("scoreText").GetComponent<Text>();
        highScore = transform.Find("highscoreText").GetComponent<Text>();
        
    }
    private void Start()
    {
        highScore.text = "HIGHSCORE : " + Score.returnHighscore().ToString();    
    }
    private void Update()
    {
        score.text = level.getInstance().getObstaclespassed().ToString();
    }
}
