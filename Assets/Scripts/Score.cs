using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
    public static void Start()
    {
        //resetHighscore();
        jooti.returnJooti().onDied += jootiScore_onDied;
    }

    private static void jootiScore_onDied(object sender, System.EventArgs e)
    {
        setHighscore(level.getInstance().getObstaclespassed());
    }

    public static int returnHighscore()
    {
        return PlayerPrefs.GetInt("highscore");
    }
    public static bool setHighscore(int score)
    {
        int currentHS = returnHighscore();
        if (currentHS < score)
        {
            PlayerPrefs.SetInt("highscore", score);
            PlayerPrefs.Save();
            return true;
        }
        else
        {
            return false;
        }
    }
    private static void resetHighscore()
    {
        PlayerPrefs.SetInt("highscore", 0);
    }
}
