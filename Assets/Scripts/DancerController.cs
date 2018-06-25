using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DancerController : MonoBehaviour
{
    //This is the dancer controller, will control the dancer timeline 
    public static DancerController Instance = null;
    //private Animator danceAnim;
    private PlayableDirector timeline;

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
        //FIXME
        timeline = GetComponentInChildren<PlayableDirector>();
    }

    private void OnEnable()
    {
        OSCController.OnPlay += Play;
        OSCController.OnStopPlay += Stop;
        OSCController.OnPausePlay += Pause;
        OSCController.OnReverse += Reverse;
        OSCController.OnFastforward += FastForward;
        StageController.OnStageChange += OnStageChange;
    }

    private void OnDisable()
    {
        OSCController.OnPlay -= Play;
        OSCController.OnStopPlay -= Stop;
        OSCController.OnPausePlay -= Pause;
        OSCController.OnReverse -= Reverse;
        OSCController.OnFastforward -= FastForward;
        StageController.OnStageChange -= OnStageChange;
    }

    void Start()
    {
        /*danceAnim = GetComponent<Animator>();
          danceAnim.speed = 0f;
          danceAnim.Rebind();
          */

    }


    private void Stop()
    {
        /*
        danceAnim.speed = 0f;
        danceAnim.Rebind();
        danceAnim.ResetTrigger("play");
        danceAnim.ResetTrigger("pause");
        */
        timeline.Stop();
    }


    private void Play()
    {
        /*
        danceAnim.speed = 1f;
        danceAnim.ResetTrigger("pause");
        danceAnim.SetTrigger("play"); */
        timeline.Play();
    }


    private void Pause()
    {
        /*
        danceAnim.ResetTrigger("play");
        danceAnim.SetTrigger("pause");*/
        timeline.Pause();
    }


    private void FastForward()
    {
        double currentTime = timeline.time;
        if (currentTime <= (timeline.duration - DATA.FAST_FORWARD_STEP))
        {
            timeline.time = currentTime + DATA.FAST_FORWARD_STEP;
        }
        else
        {
            timeline.time = 0f;
        }
    }


    private void Reverse()
    {
        double currentTime = timeline.time;
        if (currentTime >= (DATA.FAST_FORWARD_STEP))
        {
            timeline.time = currentTime - DATA.FAST_FORWARD_STEP;
        }
        else
        {
            timeline.time = 0f;
        }
    }


    private void OnStageChange()
    {
        // var animator = StageController.Config.dancerAnimator;
        //update if has dancer in the stage
        bool hasDancer = StageController.Config.hasDancer;
        gameObject.SetActive(hasDancer);
        //update the dancer timeline
        timeline.playableAsset = StageController.Config.dancerTimelineAsset;
    }

}
