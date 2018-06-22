using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control only the main music track
/// Singing animals' audios would be controlled by SingingAnimalControllers
/// </summary>
public class BackgroundAudioController : MonoBehaviour
{

    public static BackgroundAudioController Instance;
    //public StageController.SceneConfigurationData.SceneType sceneType { get; private set; }

    AudioUnit unit;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Debug.LogWarning("Found more than 1 AudioController, destroying...");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        unit = GetComponent<AudioUnit>();
        unit.Audible = true;
    }

    public void Play()
    {
        unit.Play();
    }

    public void Pause()
    {
        unit.Pause();
    }

    public void Stop()
    {
        unit.Stop();
    }

    public void FastForward()
    {
        unit.FastForward();
    }

    public void Reverse()
    {
        unit.Reverse();
    }

    internal void ChangeAudioClip(AudioClip clip)
    {
        unit.Clip = clip;
    }


}