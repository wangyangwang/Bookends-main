using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(SceneConfigData))]
public class StageController : MonoBehaviour
{

    public static StageController Instance = null;
    public static System.Action OnStageChange;
    private static bool created = false;
    public static SceneConfigurationData Config { get; private set; }

    private SceneData startupSceneData;
    private SceneData activeSceneData;
    private SceneData targetSceneData;


    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            //Debug.Log("Awake: " + this.gameObject);
        }

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        startupSceneData = new SceneData();
        activeSceneData = new SceneData();
        targetSceneData = new SceneData();
    }

    private void Start()
    {
        startupSceneData.musicianIndex = DATA.STARTUP_SCENE_MUSICIAN;
        startupSceneData.stageIndex = DATA.STARTUP_SCENE_STAGE;

        targetSceneData = startupSceneData;
        InitScene();
    }



    private void OnEnable()
    {
        OSCController.OnStageChange += GoToStage;
        OSCController.OnMusicianChange += GoToMusician;
    }

    private void OnDisable()
    {
        OSCController.OnStageChange -= GoToStage;
        OSCController.OnMusicianChange -= GoToMusician;
    }

    private void GoToStage(int gotoStageIndex)
    {
        targetSceneData = activeSceneData;
        targetSceneData.stageIndex = gotoStageIndex;
        InitScene();
    }

    private void GoToMusician(int gotoMusicianIndex)
    {
        targetSceneData = activeSceneData;
        targetSceneData.musicianIndex = gotoMusicianIndex;
        InitScene();
    }


    private void InitScene()
    {
        Config = SceneConfigData.GetConfig(targetSceneData);

        //KINECT
        KinectController.Instance.gameObject.SetActive(Config.hasKinectAvatar);

        //BIRD
        BirdController.Instance.gameObject.SetActive(Config.hasBird);

        //DANCER
        DancerController.Instance.gameObject.SetActive(Config.hasDancer);

        //COUNT OFF
        CountoffAudio.Instance.gameObject.SetActive(Config.countOffAudioClip != null);

        //SINGING ANIMALS
        SingingAnimalController.Instance.gameObject.SetActive(Config.showSingingAnimals);

        //STEMS
        StemsController.Instance.gameObject.SetActive(Config.stemTracks.Length == 0 ? false : true);




        if (OnStageChange != null) OnStageChange();
        activeSceneData = targetSceneData;
    }


    [System.Serializable]
    public struct SceneData
    {
        public int stageIndex;
        public int musicianIndex;
    }

    [System.Serializable]
    public struct SceneConfigurationData
    {

        public enum Environment
        {
            Garden, House
        }


        //---------------------------------------------
        public Environment environment;

        [Header("SOUND")]
        public AudioClip backgroundMusic;
        [Tooltip("stem audio tracks, has 4 always")]
        public AudioClip[] stemTracks;
        public AudioClip countOffAudioClip;

        [Header("ANIMATION")]
        public bool showSingingAnimals;
        public UnityEngine.Timeline.TimelineAsset dancerTimelineAsset;
        [Tooltip("bird flying path")]
        public MGCurve birdPath;
        public UnityEngine.Timeline.TimelineAsset[] singingAnimalAnimationTimelineAssets;

        [Header("TOGGLE ON/OFF")]
        [Tooltip("if use kinect + redpanda + particle system")]
        public bool hasKinectAvatar;
        [Tooltip("if show dancer")]
        public bool hasDancer;
        [Tooltip("if show bird gameobject, only affect visual aspect")]
        public bool hasBird;


    }


}
