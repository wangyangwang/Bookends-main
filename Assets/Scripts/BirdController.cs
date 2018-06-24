using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BirdController : MonoBehaviour
{
    //bird animation
    public static BirdController Instance = null;
    private Animator birdAnim;

    //bird motion path
    private MGCurve curve;
    private float duration;
    private float progress;
    private bool move;


    public MGCurve Curve
    {
        get
        {
            return curve;
        }
        set
        {
            curve = value;
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
        StageController.OnStageChange += OnStageChange;

    }

    private void OnDisable()
    {
        OSCController.OnPlay -= Play;
        OSCController.OnStopPlay -= Stop;
        OSCController.OnPausePlay -= Pause;
        StageController.OnStageChange -= OnStageChange;
    }


    private void Start()
    {
        birdAnim.speed = 0f;
        birdAnim.Rebind();
        duration = 10f;
        move = false;
    }


    private void Update()
    {
        if (move)
        {
            progress += Time.deltaTime / duration;
            if (progress > 1f)
            {
                progress = 0;
            }
            Vector3 position = curve.GetPoint(progress);
            transform.localPosition = position;

            Vector3 dir = position + curve.GetDirection(progress);
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
        //update if has bird in the stage
        bool hasBird = StageController.Config.hasBird;
        gameObject.SetActive(hasBird);
        //update the bird fly curve
        curve = StageController.Config.birdPath;
        Debug.Log("birdDoingStagechanging");
    }

}
