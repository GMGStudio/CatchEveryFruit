using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager instance;

    private AdmobManager admobManager;

    private string runsKey = "Runs";


    public void Awake()
    {
        SingletonPattern();
        admobManager = new AdmobManager();
        admobManager.Initialize();
    }

    public void ShowBanner() => admobManager.ShowBanner();
    public void HideBanner() => admobManager.HideBanner();

    public void ShowInterstitialIfNeeded()
    {
        int runs = PlayerPrefs.GetInt(runsKey, 1);
        if (runs == 5)
        {
            admobManager.ShowInterstitial();
            PlayerPrefs.SetInt(runsKey, 0);
        }
        else
        {
            admobManager.ShowBanner();
            PlayerPrefs.SetInt(runsKey, runs + 1);
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
