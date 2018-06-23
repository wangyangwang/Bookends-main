using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{


    //TODO: MELODY
    //We need to figure out what should bird do
    //bird motion path
    public MGCurve mgcurves;
    public float duration;
    private float progress;
    private bool move;
    //bird animation
    public static BirdController Instance = null;
    private Animator birdAnim;

    private bool _loop;
    public bool Loop
    {
        get
        {
            return _loop;
        }
        set
        {
            //TODO
            _loop = value;
        }
    } 

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
        birdAnim = GetComponent<Animator>();
        
    }


    private void OnEnable()
    {
        OSCController.OnPlay += Play;
        OSCController.OnStopPlay += Stop;
        OSCController.OnPausePlay += Pause;
       // StageController.OnStageChange += OnStageChange;

    }

    private void OnDisable()
    {
        OSCController.OnPlay -= Play;
        OSCController.OnStopPlay -= Stop;
        OSCController.OnPausePlay -= Pause;
       // StageController.OnStageChange -= OnStageChange;

    }


    private void Start()
    {
        birdAnim.speed = 0f;
        birdAnim.Rebind();
        move = false;
    }


    private void Update()
    {
        //Does it need to be traslated in space?
        if (move)
        {
            progress += Time.deltaTime / duration;
            if (progress > 1f)
            {
                progress = 0;
            }
            Vector3 position = mgcurves.GetPoint(progress);
            transform.localPosition = position;

            Vector3 dir = position + mgcurves.GetDirection(progress);
            transform.LookAt(dir);
        }
        
    }


    private void Stop()//real stop, start from beginning
    {
        birdAnim.speed = 0f;
        birdAnim.Rebind();
        birdAnim.ResetTrigger("play");
        birdAnim.ResetTrigger("pause");
        move = false;
    }


    private void Play()
    {
        birdAnim.speed = 1f;
        birdAnim.ResetTrigger("pause");
        birdAnim.SetTrigger("play");
        move = true;
    }


    private void Pause()//stay at the idle state
    {
        birdAnim.ResetTrigger("play");
        birdAnim.SetTrigger("pause");
        move = false;
    }



    private void OnStageChange()
    {

    }



    private void PlayAnimation(int index)
    {
        //TODO: MELODY
        //PLAY ANIMATION 1
        //PLAY ANIMATION 2
        //PLAY ANIMATION 3
    }

}
