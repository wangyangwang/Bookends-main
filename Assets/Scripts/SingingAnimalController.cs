using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        foreach (AudioUnit s in units)
        {
            s.Play();
        }
    }

    public void Pause()
    {
        foreach (AudioUnit s in units)
        {
            s.Pause();
        }
    }

    public void FastForward()
    {
        foreach (AudioUnit s in units)
        {
            s.FastForward();
        }
    }

    public void Reverse()
    {
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
