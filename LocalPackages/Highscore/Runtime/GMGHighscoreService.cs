using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGMGHighscoreService
{
    int highscore { get; }
    bool CheckHighscoreAndUpdateIfNeeded(int score);
}

public class GMGHighscoreService : IGMGHighscoreService
{
    private string highscoreKey = "Highscore";

    public int highscore
    {
        get { return PlayerPrefs.GetInt(highscoreKey); }
    }

    public bool CheckHighscoreAndUpdateIfNeeded(int score)
    {
        if (highscore >= score) { return false; }
        PlayerPrefs.SetInt(highscoreKey, score);
        return true;
    }
}
