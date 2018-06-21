using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public static AudioController Instance;
    public StageController.SceneConfigurationData.SceneType sceneType { get; private set; }

    AudioUnit[] units;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
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

    public void FastForward()
    {
        //todo
    }

    public void Reverse()
    {
        //todo
    }

    public void ChangeStageType(StageController.SceneConfigurationData.SceneType newtype)
    {
        sceneType = newtype;
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
            u.Active = (sceneType == u.stageType);
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