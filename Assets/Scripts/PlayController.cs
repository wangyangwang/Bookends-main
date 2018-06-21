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

    public const float FAST_FORWARD_STEP = 10;//seconds

    public StageController.SceneConfigurationData.SceneType CurrentStageType { get; private set; }


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
        if (!AudioController.Instance)
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
        AudioController.Instance.Play();
        AnimatorController.instance.Play();
    }

    public void PausePlay()
    {
        AudioController.Instance.PausePlay();
        AnimatorController.instance.PausePlay();
    }

    public void StopPlay()
    {
        AudioController.Instance.StopPlay();
        AnimatorController.instance.StopPlay();
    }

    public void FastForward()
    {
        AudioController.Instance.FastForward();
        AnimatorController.instance.FastForward();
    }

    public void Reverse(){
        AudioController.Instance.Reverse();
        AnimatorController.instance.Reverse();
    }

    public void ChangeStageType(StageController.SceneConfigurationData.SceneType newType)
    {
        CurrentStageType = newType;
        AudioController.Instance.ChangeStageType(newType);
        AnimatorController.instance.ChangeStageType(newType);
    }

    internal void ChangeAudioClips(AudioClip[] soundTracks)
    {
        AudioController.Instance.ChangeAudioClips(soundTracks);
    }

    //internal void EnableDancer(bool dancerStatus)
    //{
    //    AnimatorController.instance.EnableDancer(dancerStatus);
    //}

    //internal void EnableBird(bool birdStatus)
    //{
    //    //AnimatorController.instance.EnableBird(birdStatus);
    //}

    internal void ChangeAnimators()
    {
        //TODO
    }
}
