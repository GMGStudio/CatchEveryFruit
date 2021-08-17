using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;
    private IGMGHighscoreService highscoreService;

    private void Awake()
    {
        SingletonPattern();
        highscoreService = new GMGHighscoreService();
    }

    public bool CheckHighscoreAndUpdateIfNeeded(int score)
    {
        return highscoreService.CheckHighscoreAndUpdateIfNeeded(score);
    }

    public int highscore()
    {
        return highscoreService.highscore;
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
