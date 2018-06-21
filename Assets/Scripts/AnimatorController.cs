using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO
public class AnimatorController : MonoBehaviour
{

    public static AnimatorController instance = null;

    public StageController.SceneConfigurationData.SceneType stageType { get; private set; }


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


    public void ChangeStageType(StageController.SceneConfigurationData.SceneType newtype)
    {
        stageType = newtype;
    }


}
