using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancerController : MonoBehaviour
{
    //TODO: MELODY: All the TODOs on this page is for you :)

    public static DancerController Instance = null;
    private Animator danceAnim;


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

        danceAnim = GetComponent<Animator>();
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
        danceAnim.speed = 0f;
        danceAnim.Rebind();

    }

    private void Stop()//remind change back to private 
    {
        //todo
        danceAnim.speed = 0f;
        danceAnim.Rebind();
        danceAnim.ResetTrigger("play");
        danceAnim.ResetTrigger("pause");
    }

    private void Play()
    {
        //todo
        danceAnim.speed = 1f;
        danceAnim.ResetTrigger("pause");
        danceAnim.SetTrigger("play"); 
    }

    private void Pause()
    {
        //todo
        danceAnim.ResetTrigger("play");
        danceAnim.SetTrigger("pause");
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
        var animator = StageController.Config.dancerAnimator;
        //TODO: use the new animator to replace the current one.
    }

}
