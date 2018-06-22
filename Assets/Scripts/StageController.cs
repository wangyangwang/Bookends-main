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
            Debug.Log("Awake: " + this.gameObject);
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
        //SINGING ANIMALS
        SingingAnimalController.Instance.gameObject.SetActive(Config.hasSingingAnimals);


        if (OnStageChange != null) OnStageChange();
        activeSceneData = targetSceneData;
    }

#region str

    [System.Serializable]
    public struct SceneData
    {
        public int stageIndex;
        public int musicianIndex;
    }

    [System.Serializable]
    public struct SceneConfigurationData
    {

        public enum SceneType
        {
            Motion, Composing
        }

        public enum Environment
        {
            Garden, House
        }

        public SceneType sceneType;
        public Environment environment;
        public AudioClip backgroundMusic;
        public AudioClip[] singingMusics;

        public bool hasSingingMusics;
        public bool hasKinectAvatar;
        //public bool hasParticleEffect;
        public bool hasDancer;
        public Animator dancerAnimation;
        public bool hasBird;
        public bool hasSingingAnimals;
        public int singingAnimalNumber;

    }
#endregion

}
