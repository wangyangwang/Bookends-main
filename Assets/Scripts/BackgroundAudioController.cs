using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control only the main music track
/// Singing animals' audios would be controlled by SingingAnimalControllers
/// </summary>
[RequireComponent(typeof(AudioSource))]
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

        unit = GetComponent<AudioUnit>();
    }

    private void OnEnable()
    {
        OSCController.OnPlay += Play;
        OSCController.OnPausePlay += Pause;
        OSCController.OnStopPlay += Stop;
        OSCController.OnReverse += Reverse;
        OSCController.OnFastforward += FastForward;

        StageController.OnStageChange += OnStageChange;

    }

    private void OnDisable()
    {
        OSCController.OnPlay -= Play;
        OSCController.OnPausePlay -= Pause;
        OSCController.OnStopPlay -= Stop;
        OSCController.OnReverse -= Reverse;
        OSCController.OnFastforward -= FastForward;

        StageController.OnStageChange -= OnStageChange;
    }


    private void Start()
    {
        unit.Audible = true;
    }

    private void Play()
    {
        unit.Play();
    }

    private void Pause()
    {
        unit.Pause();
    }

    private void Stop()
    {
        unit.Stop();
    }

    private void FastForward()
    {
        unit.FastForward();
    }

    private void Reverse()
    {
        unit.Reverse();
    }

    private void OnStageChange()
    {
        //Update audio clip
        unit.Clip = StageController.Config.backgroundMusic;
    }

}