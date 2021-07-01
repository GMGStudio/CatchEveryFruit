using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartMenuUIManager : UIManager
{
    public static StartMenuUIManager instance;

    [SerializeField]
    GameObject score;
    [SerializeField]
    private GameObject PauseButton;

    private void Awake()
    {
        SingletonPattern();
    }

    public override void Enable(bool active)
    {
        if (active)
        {
            base.Enable(active);
            GetComponent<UIElementScaler>().Show();
            score.GetComponent<UIElementScaler>().Hide();
            PauseButton.GetComponent<UIElementScaler>().Hide();
        }
        else
        {
            GetComponent<UIElementScaler>().Hide();
            score.GetComponent<UIElementScaler>().Show();
            PauseButton.GetComponent<UIElementScaler>().Show();
          
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
