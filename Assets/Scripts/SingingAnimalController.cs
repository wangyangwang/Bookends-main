using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the controller for all four singing animals.
/// this controls the audio and the animation.
/// </summary>
public class SingingAnimalController : MonoBehaviour
{

    public static SingingAnimalController Instance = null;

    private AudioUnit[] units;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        units = GetComponentsInChildren<AudioUnit>();
    }

    public void FillAudioClips(AudioClip[] clips)
    {
        int n = clips.Length;
        if (n != units.Length)
        {
            Debug.LogError("audio source number != audioclips length passed in.");
        }
        for (int i = 0; i < n; i++)
        {
            units[i].Clip = clips[i];
        }
    }

    public void Play()
    {
        //todo: melody: start play animations
        foreach (AudioUnit s in units)
        {
            s.Play();
        }
    }

    public void Pause()
    {
        //TODO: melody: pause playing animations
        foreach (AudioUnit s in units)
        {
            s.Pause();
        }
    }

    public void FastForward()
    {
        //TODO: MELODY: not sure, maybe nothing is needed here
        foreach (AudioUnit s in units)
        {
            s.FastForward();
        }
    }

    public void Reverse()
    {
        //TODO: MELODY: not sure, maybe nothing is needed here
        foreach (AudioUnit s in units)
        {
            s.Reverse();
        }
    }

    public void ChangeVolumn(int which, float newVol)
    {
        units[which].Vol = newVol;
    }

    public void SetAudibleState(int which, bool state)
    {
        units[which].Audible = state;
    }

}
