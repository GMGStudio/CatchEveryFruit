using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;

    private readonly string highscoreKey = "Highscore";

    private void Awake()
    {
        SingletonPattern();
    }

    public void SetHighScorePerhabs()
    {
        int score = ScoreManager.instance.GetScore();

        if(score > GetHighscore())
        {
            PlayerPrefs.SetInt(highscoreKey, score);
        }
    }

    public int GetHighscore()
    {
        return PlayerPrefs.GetInt(highscoreKey);
    }

    private void SingletonPattern()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
