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

    private void OnEnable()
    {
        OSCController.OnPlay += Play;
        OSCController.OnPausePlay += Pause;
        OSCController.OnStopPlay += Stop;
        OSCController.OnReverse += Reverse;
        OSCController.OnFastforward += FastForward;
        OSCController.OnVolumnChange += ChangeVolumn;
        OSCController.OnSingingAnimalToggle += SetAudibleState;

        StageController.OnStageChange += OnStageChange;
    }

    private void OnDisable()
    {
        OSCController.OnPlay -= Play;
        OSCController.OnPausePlay -= Pause;
        OSCController.OnStopPlay -= Stop;
        OSCController.OnReverse -= Reverse;
        OSCController.OnFastforward -= FastForward;
        OSCController.OnVolumnChange -= ChangeVolumn;
        OSCController.OnSingingAnimalToggle -= SetAudibleState;

        StageController.OnStageChange -= OnStageChange;
    }

    private void Start()
    {
        units = GetComponentsInChildren<AudioUnit>();
    }

    private void Play()
    {
        //todo: melody: start play animations
        foreach (AudioUnit s in units)
        {
            s.Play();
        }
    }

    private void Pause()
    {
        //TODO: melody: pause playing animations
        foreach (AudioUnit s in units)
        {
            s.Pause();
        }
    }

    private void Stop()
    {
        foreach (AudioUnit s in units)
        {
            s.Stop();
        }
    }

    private void FastForward()
    {
        //TODO: MELODY: not sure, maybe nothing is needed here
        foreach (AudioUnit s in units)
        {
            s.FastForward();
        }
    }

    private void Reverse()
    {
        //TODO: MELODY: not sure, maybe nothing is needed here
        foreach (AudioUnit s in units)
        {
            s.Reverse();
        }
    }

    private void ChangeVolumn(int which, float newVol)
    {
        units[which].Vol = newVol;
    }

    private void SetAudibleState(int which)
    {
        units[which].Audible = !(units[which].Audible);
    }

    private void OnStageChange()
    {
        FillAudioClips();
    }

    private void FillAudioClips()
    {
        //update clips
        var config = StageController.Config;
        AudioClip[] newclips = StageController.Config.singingMusics;
        if (newclips.Length != units.Length)
        {
            Debug.LogError("audio source number != audioclips length passed in.");
        }
        for (int i = 0; i < newclips.Length; i++)
        {
            units[i].Clip = newclips[i];
        }
    }

}
