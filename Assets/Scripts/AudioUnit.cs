using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUnit : MonoBehaviour
{

    //FIXME: probbaly use active to control is not a good idea.
    private AudioSource[] sources;

    [SerializeField]
    private bool active;

    public bool Active
    {
        get
        {
            return active;
        }

        set
        {
            ResetVol();
            active = value;
        }
    }

    //TODO:better way to setup type
    public StageSettings.StageType stageType;

    private void Awake()
    {
        sources = GetComponentsInChildren<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        if (sources.Length < 1) Debug.LogError("no audio source found!");
        Debug.Log("audio unit" + gameObject.name + " type has changed to " + stageType.ToString());
    }

    public void Play()
    {
        if (!active) return;
        foreach (AudioSource a in sources)
        {
            a.Play();
        }
    }

    public void ChangeVolumn(float v, int index)
    {
        if (!active || stageType == StageSettings.StageType.MOTION) return;
        sources[index].volume = v;
    }

    public void Stop()
    {
        if (!active) return;
        foreach (AudioSource a in sources)
        {
            a.Stop();
        }
    }
    public void Pause()
    {
        if (!active) return;
        foreach (AudioSource a in sources)
        {
            a.Pause();
        }
    }

    public void JumpTo(float p)
    {
        if (!active) return;
        float realSeconds = p * sources[0].clip.length;
        foreach (AudioSource a in sources)
        {
            a.time = realSeconds;
        }
    }

    public void ResetVol()
    {
        foreach (AudioSource a in sources)
        {
            a.volume = 1;
        }
    }

    internal void ChangeAudioClips(AudioClip[] soundTracks)
    {

        //HACK
        if (soundTracks.Length == sources.Length)
        {
            for (int i = 0; i < sources.Length; ++i)
            {
                sources[i].clip = soundTracks[i];
            }
        }
        else
        {
            Debug.Log(gameObject.name + "  sources count doesn't math.");
        }



    }
}
