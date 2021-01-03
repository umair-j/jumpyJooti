using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    private Text scoreText;
    private Text highscoreText;
    private void Awake()
    {

        scoreText = transform.Find("scoreText").GetComponent<Text>();
        highscoreText = transform.Find("highscoreText").GetComponent<Text>();
    }
    public void restartGame()
    {
        //SceneManager.LoadScene("gameScene");
        gameLoader.loadScene(gameLoader.scene.gameScene);
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void mainMenu()
    {
        gameLoader.loadScene(gameLoader.scene.startMenu);
    }
    private void Start()
    {
        hide();
        jooti.returnJooti().onDied += jooti_onDead;
    }

    private void jooti_onDead(object sender, System.EventArgs e)
    {
        scoreText.text = level.getInstance().getObstaclespassed().ToString();
        if (Score.returnHighscore() < level.getInstance().getObstaclespassed())
        {
            highscoreText.text = "NEW HIGHSCORE : " + level.getInstance().getObstaclespassed();
        }
        else
        {
            highscoreText.text = "HIGHSCORE : " + Score.returnHighscore();
        }
        
        show();
        Debug.Log("restartMenuShow");
    }
    private void show()
    {
        gameObject.SetActive(true);
    }
    private void hide()
    {
        gameObject.SetActive(false);
        Debug.Log("restartMenuHide");
    }
}
