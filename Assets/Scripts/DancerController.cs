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

    public void Stop()//remind change back to private 
    {
        //todo
        danceAnim.speed = 0f;
        danceAnim.Rebind();
        danceAnim.ResetTrigger("play");
        danceAnim.ResetTrigger("pause");
    }

    public void Play()
    {
        //todo
        danceAnim.speed = 1f;
        danceAnim.ResetTrigger("pause");
        danceAnim.SetTrigger("play"); 
    }

    public void Pause()
    {
        //todo
        danceAnim.ResetTrigger("play");
        danceAnim.SetTrigger("pause");
    }

    public void FastForward()
    {
        //todo
    }
    public void Reverse()
    {
        //todo
    }

    private void OnStageChange()
    {
        var animation = StageController.Config.dancerAnimation;
        //TODO: use the new animation to replace the current one.
    }

}
