using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(SceneConfigData))]
public class StageController : MonoBehaviour
{
    public const int STAGE_COUNT = 5;
    public const int MUSICIAN_STAGE_COUNT = 2;

    public static StageController instance = null;

    //scene data
    private SceneData startupSceneData;
    private SceneData activeSceneData;
    private SceneData targetSceneData;
    //scene settings
    private SceneConfigurationData sceneConfig;


    //dontdestroyonload
    private static bool created = false;

    public SceneData GetActiveSceneData
    {
        get
        {
            return activeSceneData;
        }
    }

    public SceneConfigurationData GetSceneConfiguration
    {
        get
        {
            return sceneConfig;
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


    public void InitScene()
    {

        SceneConfigurationData config = SceneConfigData.GetConfig(targetSceneData);


        EnvironmentController.Instance.ChangeToScene(config.environment);
        KinectController.Instance.gameObject.SetActive(config.hasKinectAvatar);
        BirdController.Instance.gameObject.SetActive(config.hasBird);
        DancerController.Instance.gameObject.SetActive(config.hasDancer);
        ParticleSystemController.Instance.gameObject.SetActive(config.hasParticleEffect);
        //SiningAnimalController.Instance.gameObject.SetActive(config.hasSingingAnimals);


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
