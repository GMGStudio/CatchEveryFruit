using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioController
{
    public SoundEffects Effect;

    [Range(0f,1f)]
    public float volume;
    public AudioClip Clip;
    public bool Loop;
    public bool Pitchvariation;

    public bool SoundActive { get; private set; }
    [HideInInspector]
    public AudioSource Source;

    private readonly float LowPitchRange = .95f;
    private readonly float HighPitchRange = 1.05f;

    public void SetSoundActive(bool active)
    {
        SoundActive = active;
    }

    public void PlaySound()
    {
        if (!SoundActive) { return; }
        Source.clip = Clip;
        if (Pitchvariation)
        {
            Source.pitch = GetRandomPitch();
        }
        Source.loop = Loop;
        Source.Play();
    }

    public void StopSound()
    {
        Source.Stop();
    }

    private float GetRandomPitch()
    {
        return Random.Range(LowPitchRange, HighPitchRange);
    }
}
