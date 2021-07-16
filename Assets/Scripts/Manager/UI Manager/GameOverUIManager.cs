using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUIManager : UIManager
{
    private const string highScorepreText = " Your Highscore: ";
    private const string newhighScoreText = "New Highscore";
    public static GameOverUIManager instance;

    [SerializeField]
    TextMeshProUGUI highscoreText;
    [SerializeField]
    private GameObject PauseButton;
    [SerializeField]
    private GameObject firework;
    [SerializeField]
    private GameObject ScoreUI;
    [SerializeField]
    TextMeshProUGUI scoreText;


    private void Awake()
    {
        SingletonPattern();
    }

    public override void Enable(bool active)
    {
        if (active)
        {
            base.Enable(active);
            highscoreText.SetText(GetHighscoreText());
            scoreText.SetText(ScoreManager.instance.GetScore().ToString("D4"));
            GetComponent<UIElementScaler>().Show();
            PauseButton.GetComponent<UIElementScaler>().Hide();
            ScoreUI.GetComponent<UIElementScaler>().Hide();
        }
        else
        {
            GetComponent<UIElementScaler>().Hide();
            PauseButton.GetComponent<UIElementScaler>().Show();
            ScoreUI.GetComponent<UIElementScaler>().Show();
            firework.SetActive(false);
        }
    }


    private string GetHighscoreText()
    {
        int highscore = HighScoreManager.instance.GetHighscore();
        int score = ScoreManager.instance.GetScore();

        if(score > highscore)
        {
            firework.SetActive(true);
            return newhighScoreText;
        }
        else
        {
            return highScorepreText + highscore.ToString();
        }
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
