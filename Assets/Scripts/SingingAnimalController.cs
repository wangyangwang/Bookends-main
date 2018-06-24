using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

/// <summary>
/// This is the controller for all four singing animals.
/// this controls the animation.
/// </summary>
public class SingingAnimalController : MonoBehaviour
{
    public static SingingAnimalController Instance = null;

    private PlayableDirector[] timelines;
   
    //[SerializeField]
    //private Animator birdAnim;


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
        //if (birdAnim == null) Debug.LogError("please assign birdAnim to SingingAnimalController");
        timelines = GetComponentsInChildren<PlayableDirector>();
    }


    private void Play()
    {
        foreach (PlayableDirector p in timelines)
        {        
           p.Play();                     
        }
    }


    private void Pause()
    {
        foreach (PlayableDirector p in timelines)
        {
            p.Pause();
        }
    }


    private void Stop()
    {
        foreach (PlayableDirector p in timelines)
        {
            p.Stop();
        }
    }


    private void FastForward()
    {
        foreach (PlayableDirector p in timelines)
        {
            double currentTime = p.time;
            if(currentTime <= (p.duration- DATA.FAST_FORWARD_STEP))
            {
                p.time = currentTime + DATA.FAST_FORWARD_STEP;
            }
            else
            {
                p.time = 0f;
            }         
        }
    }


    private void Reverse()
    {
        foreach (PlayableDirector p in timelines)
        {
            double currentTime = p.time;
            if (currentTime >= (DATA.FAST_FORWARD_STEP))
            {
                p.time = currentTime - DATA.FAST_FORWARD_STEP;
            }
            else
            {
                p.time = 0f;
            }           
        }

    }


    private void OnStageChange()
    {
        FillTimelineClips();
    }


    private void FillTimelineClips()
    {
        //update timeline clips
        TimelineAsset[] newTimelines = StageController.Config.singingAnimalAnimationTimelineAssets;

        //TODO: does timeline amount change from stage to stage?

        //if (newTimelines.Length != timelines.Length)
        //{
        //    //FIXME: sometimes this error appear, trying to reproduce it and seems kinda random now.
        //    Debug.LogError("new timelines count > " + newTimelines.Length + ",       i have timelines  >  " + timelines.Length);
        //}

        for (int i = 0; i < newTimelines.Length; i++)
        {
            timelines[i].playableAsset = newTimelines[i];
        }
    }

}
