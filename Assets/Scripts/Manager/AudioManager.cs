using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public List<AudioController> audioControllers;

    public bool SoundActive { get; private set; }
    private const string AudioEnableKey = "AudioEnabled";

    private void Awake()
    {
        SingletonPattern();
        SoundActive = PlayerPrefs.GetInt(AudioEnableKey, 1) == 1;
        foreach(AudioController controller in audioControllers)
        {
            controller.Source = gameObject.AddComponent<AudioSource>();
            controller.SetSoundActive(SoundActive);
        }
        PlaySound(SoundEffects.Theme);
    }

    public void ToogleSoundActive()
    {
        SoundActive = !SoundActive;
        PlayerPrefs.SetInt(AudioEnableKey, SoundActive ? 1 : 0);
        PausedMenuUIManager.instance.ChangeAudioIcon();
        if (SoundActive)
        {
            SetActiveAllSounds();
            PlaySound(SoundEffects.Theme);
        }
        else
        {
            StopAllSounds();
        }
    }

    public void PlaySound(SoundEffects effect)
    {
        if (!SoundActive) { return; }
        AudioController controller = GetControllerByEffect(effect);
        controller.PlaySound();
    }

    public void StopAllSounds()
    {
        foreach(AudioController controller in audioControllers)
        {
            controller.StopSound();
        }
    }

    public void SetActiveAllSounds()
    {
        foreach (AudioController controller in audioControllers)
        {
            controller.SetSoundActive(true);
        }
    }

    private AudioController GetControllerByEffect(SoundEffects effect)
    {
        return audioControllers.Find(controller => controller.Effect == effect);
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
