using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the controller for all stem audios 
/// </summary>
public class StemsController : MonoBehaviour
{
    public static StemsController Instance = null;
    private AudioUnit[] units;

    //[SerializeField]
    //private AudioUnit birdUnit;

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


    void Start()
    {
        units = GetComponentsInChildren<AudioUnit>();
        //if (birdUnit == null) Debug.LogError("please assign birdunit to SingingAnimalController's slot.");
    }


    private void Play()
    {
        foreach (AudioUnit s in units)
        {
            s.Play();
        }
    }


    private void Pause()
    {
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
        foreach (AudioUnit s in units)
        {
            s.FastForward();
        }
    }


    private void Reverse()
    {
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
        AudioClip[] newclips = StageController.Config.stemTracks;

        //TODO: does stem number change?


        for (int i = 0; i < newclips.Length; i++)
        {
            units[i].Clip = newclips[i];
        }

    }


}
