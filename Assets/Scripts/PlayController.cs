﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{

    public static PlayController instance = null;


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


}