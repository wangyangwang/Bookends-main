using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(SceneConfigData))]
public class StageController : MonoBehaviour
{

    public static StageController instance = null;

    public const int STAGE_COUNT_EACH_MUSICIAN = 5;
    public const int MUSICIAN_COUNT = 2;

    //scene data
    private SceneData startupSceneData;
    private SceneData activeSceneData;
    private SceneData targetSceneData;

    //dontdestroyonload
    private static bool created = false;

    public SceneData GetActiveSceneData
    {
        get
        {
            return activeSceneData;
        }
    }


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

        startupSceneData = new SceneData();
        activeSceneData = new SceneData();
        targetSceneData = new SceneData();
    }

    private void Start()
    {
        targetSceneData = startupSceneData;
        InitScene();
    }

    public void GoToStage(int gotoStageIndex)
    {
        targetSceneData = activeSceneData;
        targetSceneData.stageIndex = gotoStageIndex;
        InitScene();
    }

    public void GoToMusician(int gotoMusicianIndex)
    {
        targetSceneData = activeSceneData;
        targetSceneData.musicianIndex = gotoMusicianIndex;
        InitScene();
    }


    internal void InitScene()
    {

        SceneConfigurationData config = SceneConfigData.GetConfig(targetSceneData);

        //=======================
        //BACKGROUND
        EnvironmentController.Instance.ChangeToScene(config.environment);
        //=======================
        //KINECT
        KinectController.Instance.gameObject.SetActive(config.hasKinectAvatar);
        //=======================
        //BIRD
        BirdController.Instance.gameObject.SetActive(config.hasBird);
        //=======================
        //DANCER
        DancerController.Instance.gameObject.SetActive(config.hasDancer);
        //=======================
        //PARTICLE SYSTEM
        ParticleSystemController.Instance.gameObject.SetActive(config.hasParticleEffect);
        //=======================
        //SINGING ANIMALS
        SingingAnimalController.Instance.gameObject.SetActive(config.hasSingingAnimals);
        if (config.hasSingingAnimals)
        {
            SingingAnimalController.Instance.FillAudioClips(config.singingMusics);
        }



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
        public bool hasParticleEffect;
        public bool hasDancer;
        public bool hasBird;
        public bool hasSingingAnimals;
        public int singingAnimalNumber;

    }


}
