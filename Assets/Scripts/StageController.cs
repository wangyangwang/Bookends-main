using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class StageController : MonoBehaviour
{

    //===============================================


    public const int STAGE_COUNT = 5;
    public const int stageWithBackgroundTrack = 3;
    public const int backdropCount = 2;
    public const int MUSICIAN_STAGE_COUNT = 2;


    [Header("Links")]
    public GameObject bird;
    public GameObject dancer;
    public GameObject motionSceneStaging;
    public GameObject composingSceneStaging;

    [Header("Settings for each stage")]



    public StageSettings[] vivaldi = new StageSettings[STAGE_COUNT];
    public StageSettings[] mozart = new StageSettings[STAGE_COUNT];


    //===============================================

    private static bool created = false;
    public static StageController instance = null;


    public int CurrentStageIndex { get; private set; }
    public int CurrentMusicianIndex { get; private set; }
    public StageSettings.StageType CurrentStageType { get; private set; }


    private void Awake()
    {

        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        CurrentStageIndex = SceneManager.GetActiveScene().buildIndex;
        CurrentMusicianIndex = SceneManager.sceneCountInBuildSettings;
    }

    private void Start()
    {
        ReInitScene();
    }

    public void GoToStage(int stageIndex)
    {
        CurrentStageIndex = stageIndex;
        ReInitScene();
    }

    public void GoToMusician(int musicianIndex)
    {
        CurrentMusicianIndex = musicianIndex;
        ReInitScene();
    }

    public void ReInitScene()
    {
        StageSettings targetSettings = GetSettings(CurrentMusicianIndex, CurrentStageIndex);

        //Set Backdrop 
        motionSceneStaging.SetActive(targetSettings.stageType == StageSettings.StageType.MOTION);
        composingSceneStaging.SetActive(targetSettings.stageType == StageSettings.StageType.COMPOSING);

        //enable/disable
        PlayController.instance.EnableDancer(targetSettings.dancerStatus);
        PlayController.instance.EnableBird(targetSettings.flyingBirdStatus);
        ParticleSystemController.instance.EnableParticles(targetSettings.useParticles);
        KinectController.instance.EnableRedPanda(targetSettings.useKinectControledRedPanda);

        //swap
        PlayController.instance.ChangeStageType(targetSettings.stageType);
        PlayController.instance.ChangeAudioClips(targetSettings.soundTracks);
        PlayController.instance.ChangeAnimators();//TODO

        //Setup finished, reload
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private StageSettings GetSettings(int currentMusicianIndex, int currentStageIndex)
    {
        StageSettings[] settings;

        switch (currentStageIndex)
        {
            case 0:
                settings = vivaldi;
                break;
            case 1:
                settings = mozart;
                break;
            default:
                Debug.LogError("Cannot find matching musician index!");
                settings = null;
                break;
        }

        return settings[currentStageIndex];
    }
}
