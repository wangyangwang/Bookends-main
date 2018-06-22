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

    [SerializeField]
    private AudioUnit birdUnit;


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
        if (birdUnit == null) Debug.LogError("please assign birdunit to SingingAnimalController's slot.");
    }

    private void Play()
    {
        //todo: melody: start play animations
        foreach (AudioUnit s in units)
        {
            s.Play();
        }
    }

    private void Update()
    {

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
        UpdateAudioUnitCount();
        FillAudioClips();
    }

    private void UpdateAudioUnitCount()
    {
        var config = StageController.Config;
        if (config.singingAnimalNumber < units.Length)
        {
            birdUnit.gameObject.SetActive(false);
        }
        else
        {
            birdUnit.gameObject.SetActive(true);
        }
        units = GetComponentsInChildren<AudioUnit>();
    }

    private void FillAudioClips()
    {
        //update clips
        var config = StageController.Config;
        AudioClip[] newclips = StageController.Config.singingMusics;


        if (newclips.Length != units.Length)
        {
            //FIXME: sometimes this error appear, trying to reproduce it and seems kinda random now.
            Debug.LogError("new clips count > " + newclips.Length + ",       i have audio units  >  " + units.Length);
        }

        for (int i = 0; i < newclips.Length; i++)
        {
            units[i].Clip = newclips[i];
        }
    }

}
