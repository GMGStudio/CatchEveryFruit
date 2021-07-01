using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;

    private int score;

    private void Awake()
    {
        SingletonPattern();
    }

    public void AddScore()
    {
        score++;
        UpdateScoreText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    public int GetScore()
    {
        return score;
    }

    private void UpdateScoreText()
    {
        scoreText.SetText(score.ToString());
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
