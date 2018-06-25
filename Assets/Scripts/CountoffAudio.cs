using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioUnit))]
[RequireComponent(typeof(AudioSource))]
public class CountoffAudio : MonoBehaviour
{
    public static CountoffAudio Instance = null;

    private AudioUnit unit;

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

    private void Play()
    {
        unit.Play();
    }

    private void Stop()
    {
        unit.Stop();
    }

    private void Pause()
    {
        unit.Pause();
    }

    private void Reverse()
    {
        unit.Reverse();
    }

    private void FastForward()
    {
        unit.FastForward();
    }

    private void OnStageChange()
    {
        var config = StageController.Config;
        unit.Clip = config.countOffAudioClip;
    }


}
