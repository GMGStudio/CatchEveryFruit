using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PausedMenuUIManager : UIManager
{
    public static PausedMenuUIManager instance;

    [SerializeField]
    GameObject SoundButton;
    [SerializeField]
    GameObject TapToContinue;

    [SerializeField]
    Image SpeakerImage;

    [SerializeField]
    Sprite SpeakerImage_enabled;
    [SerializeField]
    Sprite SpeakerImage_disabled;

    private void Awake()
    {
        SingletonPattern();
    }

    public override void Enable(bool active)
    {
        if (active)
        {
            base.Enable(active);
            ChangeAudioIcon();
            TapToContinue.GetComponent<UIElementScaler>().Show();
            SoundButton.GetComponent<UIElementScaler>().Show();
        }
        else
        {
            TapToContinue.GetComponent<UIElementScaler>().Hide();
            SoundButton.GetComponent<UIElementScaler>().Hide();
          
        }
    }

    public void ChangeAudioIcon()
    {
        SpeakerImage.sprite = AudioManager.instance.SoundActive ? SpeakerImage_enabled : SpeakerImage_disabled;
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
