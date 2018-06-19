using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{

    public static AudioController instance;
    public PlayController.StageType stageType { get; private set; }

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
    }


    void Start()
    {
        units = GetComponentsInChildren<AudioUnit>();
        if (units.Length != 2) Debug.LogWarning("audio source count is wrong, please check all audio sources.");
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

    public void ChangeStageType(PlayController.StageType newtype)
    {
        stageType = newtype;
        foreach (AudioUnit u in units)
        {
            u.Active = (stageType == u.playControlStageType);
            Debug.Log("AudioController's stage type is changed to " + newtype.ToString());
        }
    }

}
