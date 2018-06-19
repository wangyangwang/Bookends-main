using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO
public class AnimatorController : MonoBehaviour
{

    public static AnimatorController instance = null;

    public StageSettings.StageType stageType { get; private set; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.LogError("Found more than 1 AnimatorController, destroying gameobject...");
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        //TODO
    }
    public void PausePlay()
    {
        //TODO
    }
    public void StopPlay()
    {
        //TODO
    }
    public void JumpTo(float p)
    {
        //TODO
    }

    internal void EnableDancer(bool dancerStatus)
    {
        //FIXME code coupling
        StageController.instance.dancer.SetActive(dancerStatus);
    }

    public void ChangeStageType(StageSettings.StageType newtype)
    {
        stageType = newtype;
    }

    internal void EnableBird(bool birdStatus)
    {
        //FIXME code coupling
        StageController.instance.bird.SetActive(birdStatus);
    }
}
