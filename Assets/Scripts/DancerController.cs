using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancerController : MonoBehaviour
{
    //TODO: MELODY: All the TODOs on this page is for you :)

    public static DancerController Instance = null;


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
        //todo
    }

    private void Stop()
    {
        //todo
    }

    private void Play()
    {
        //todo
    }
    private void Pause()
    {
        //todo
    }
    private void FastForward()
    {
        //todo
    }
    private void Reverse()
    {
        //todo
    }

    private void OnStageChange()
    {
        var animation = StageController.Config.dancerAnimation;
        //TODO: use the new animation to replace the current one.
    }

}
