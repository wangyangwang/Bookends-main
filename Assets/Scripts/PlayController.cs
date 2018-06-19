using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wrapper class for AnimationController and AudioController
/// </summary>
public class PlayController : MonoBehaviour
{

    public static PlayController instance = null;


    public StageSettings.StageType CurrentStageType { get; private set; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.LogError("found more than 1 playcontroller, destroying the gameobject...");
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        if (!AudioController.instance)
        {
            Debug.LogError("cannot find audiocontroller");
        }
        else if (!AnimatorController.instance)
        {
            Debug.LogError("cannot find animatorcontroller");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        AudioController.instance.Play();
        AnimatorController.instance.Play();
    }

    public void PausePlay()
    {
        AudioController.instance.PausePlay();
        AnimatorController.instance.PausePlay();
    }

    public void StopPlay()
    {
        AudioController.instance.StopPlay();
        AnimatorController.instance.StopPlay();
    }

    public void JumpTo(float p)
    {
        AudioController.instance.JumpTo(p);
        AnimatorController.instance.JumpTo(p);
    }

    public void ChangeStageType(StageSettings.StageType newType)
    {
        CurrentStageType = newType;
        AudioController.instance.ChangeStageType(newType);
        AnimatorController.instance.ChangeStageType(newType);
    }

    internal void ChangeAudioClips(AudioClip[] soundTracks)
    {
        AudioController.instance.ChangeAudioClips(soundTracks);
    }

    internal void EnableDancer(bool dancerStatus)
    {
        AnimatorController.instance.EnableDancer(dancerStatus);
    }

    internal void EnableBird(bool birdStatus)
    {
        AnimatorController.instance.EnableBird(birdStatus);
    }

    internal void ChangeAnimators()
    {
        //TODO
    }
}
