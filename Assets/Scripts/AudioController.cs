using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public static AudioController instance;
    public StageController.SceneConfigurationData.SceneType stageType { get; private set; }

    AudioUnit[] units;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.LogError("Found more than 1 AudioController, destroying...");
            Destroy(gameObject);
        }
        units = GetComponentsInChildren<AudioUnit>();

    }


    void Start()
    {
        if (units.Length != 2) Debug.LogWarning("audio source count is wrong, please check all audio sources.");

        ResetState();
        ResetVol();
    }

    public void Play()
    {
        foreach (AudioUnit u in units)
        {
            u.Play();
        }
    }

    public void PausePlay()
    {
        foreach (AudioUnit u in units)
        {
            u.Pause();
        }
    }

    public void ChangeVolumn(int index, float v)
    {
        foreach (AudioUnit u in units)
        {
            u.ChangeVolumn(v, index);
        }
    }

    public void StopPlay()
    {
        foreach (AudioUnit u in units)
        {
            u.Stop();
        }
    }

    public void JumpTo(float p)
    {
        foreach (AudioUnit u in units)
        {
            u.JumpTo(p);
        }
    }

    public void ChangeStageType(StageController.SceneConfigurationData.SceneType newtype)
    {
        stageType = newtype;
        ResetState();
        ResetVol();
    }

    internal void ChangeAudioClips(AudioClip[] soundTracks)
    {
        foreach (AudioUnit u in units)
        {
            u.ChangeAudioClips(soundTracks);
        }
    }

    private void ResetState()
    {
        foreach (AudioUnit u in units)
        {
            u.Active = (stageType == u.stageType);
        }
    }

    private void ResetVol()
    {
        foreach (AudioUnit u in units)
        {
            u.ResetVol();
        }
    }

}
