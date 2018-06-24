using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[ExecuteInEditMode]
public class SceneConfigData : MonoBehaviour
{

    public static SceneConfigData Instance = null;

    [System.Serializable]
    public class MusicianConfig
    {
        public StageController.SceneConfigurationData[] stage;
    }

    [SerializeField]
    [Header("0 means vivaldi, 1 means mozart")]
    public MusicianConfig[] GConfig;


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
    }

    private void Start()
    {

        foreach (var m in GConfig)
        {
            if (m.stage.Length == 0)
            {
                Debug.LogError("Scenes Settings are not configured, quitting.");
                Application.Quit();
            }
        }

    }

    public static StageController.SceneConfigurationData GetConfig(StageController.SceneData sceneData)
    {
        StageController.SceneConfigurationData data = new StageController.SceneConfigurationData();
        data = SceneConfigData.Instance.GConfig[sceneData.musicianIndex].stage[sceneData.stageIndex];
        return data;
    }

    /*
    public void InitDataStructure()
    {

        GConfig = new MusicianConfig[DATA.MUSICIAN_COUNT];

        for (int i = 0; i < GConfig.Length; i++)
        {
            GConfig[i] = new MusicianConfig();
            GConfig[i].stage = new StageController.SceneConfigurationData[DATA.STAGE_COUNT_EACH_MUSICIAN];
            for (int y = 0; y < GConfig[i].stage.Length; y++)
            {
                GConfig[i].stage[y] = new StageController.SceneConfigurationData();
            }

        }


        foreach (var mc in GConfig)
        {
            for (int i = 0; i < DATA.STAGE_COUNT_EACH_MUSICIAN; i++)
            {

                //audio count and scene type
                if (i == 0 || i == 1 || i == 2)
                {
                    mc.stage[i].singingMusics = new AudioClip[0];
                    mc.stage[i].sceneType = StageController.SceneConfigurationData.SceneType.Motion;

                }
                else if (i == 3)
                {
                    mc.stage[i].singingMusics = new AudioClip[3];
                    mc.stage[i].singingAnimalNumber = 3;
                    mc.stage[i].sceneType = StageController.SceneConfigurationData.SceneType.Composing;
                }
                else if (i == 4)
                {
                    mc.stage[i].singingMusics = new AudioClip[4];
                    mc.stage[i].singingAnimalNumber = 4;
                    mc.stage[i].sceneType = StageController.SceneConfigurationData.SceneType.Composing;
                }

                if (i > 1)
                {
                    mc.stage[i].hasBird = true;
                }

                //the rest
                if (mc.stage[i].sceneType == StageController.SceneConfigurationData.SceneType.Composing)
                {
                    mc.stage[i].hasSingingMusics = true;
                    mc.stage[i].hasSingingAnimals = true;
                    mc.stage[i].environment = StageController.SceneConfigurationData.Environment.House;
                }
                else if (mc.stage[i].sceneType == StageController.SceneConfigurationData.SceneType.Motion)
                {
                    mc.stage[i].environment = StageController.SceneConfigurationData.Environment.House;
                    mc.stage[i].hasKinectAvatar = true;
                    //mc.stage[i].hasParticleEffect = true;

                    if (i == 2)
                    {
                        mc.stage[i].hasDancer = true;
                    }

                    mc.stage[i].hasKinectAvatar = true;
                    mc.stage[i].environment = StageController.SceneConfigurationData.Environment.Garden;

                }

            }

        }
    }
    */


}

