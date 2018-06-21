using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class SceneConfigData : MonoBehaviour
{

    const int STAGE_COUNT_EACH_MUSICIAN = 5;
    const int MUSICIAN_COUNT = 2;


    [System.Serializable]
    public class MusicianConfig
    {
        public StageController.SceneConfigurationData[] data;
    }

    [SerializeField]
    [Header("0 means vivaldi, 1 means mozart")]
    public MusicianConfig[] AllMusicianConfig;



    private void Start()
    {
        foreach (var m in AllMusicianConfig)
        {
            if (m.data.Length == 0)
            {
                Debug.LogError("Scenes Settings are not configured, quitting.");
                Application.Quit();
            }
        }

    }

    public static StageController.SceneConfigurationData GetConfig(StageController.SceneData sceneData)
    {
        StageController.SceneConfigurationData data = new StageController.SceneConfigurationData();
        return data;
    }

    public void InitDataStructure()
    {

        AllMusicianConfig = new MusicianConfig[MUSICIAN_COUNT];

        for (int i = 0; i < AllMusicianConfig.Length; i++)
        {
            AllMusicianConfig[i] = new MusicianConfig();
            AllMusicianConfig[i].data = new StageController.SceneConfigurationData[STAGE_COUNT_EACH_MUSICIAN];
            for (int y = 0; y < AllMusicianConfig[i].data.Length; y++)
            {
                AllMusicianConfig[i].data[y] = new StageController.SceneConfigurationData();
            }

        }


        foreach (var mc in AllMusicianConfig)
        {
            for (int i = 0; i < STAGE_COUNT_EACH_MUSICIAN; i++)
            {

                //based on id
                if (i == 0 || i == 1 || i == 2)
                {
                    mc.data[i].singingMusics = new AudioClip[0];
                    mc.data[i].sceneType = StageController.SceneConfigurationData.SceneType.Motion;

                }
                else if (i == 3)
                {
                    mc.data[i].singingMusics = new AudioClip[3];
                    mc.data[i].singingAnimalNumber = 3;
                    mc.data[i].sceneType = StageController.SceneConfigurationData.SceneType.Composing;
                }
                else if (i == 4)
                {
                    mc.data[i].singingMusics = new AudioClip[4];
                    mc.data[i].singingAnimalNumber = 4;
                    mc.data[i].sceneType = StageController.SceneConfigurationData.SceneType.Composing;
                }

                //based on scene type
                if (mc.data[i].sceneType == StageController.SceneConfigurationData.SceneType.Composing)
                {
                    mc.data[i].hasSingingMusics = true;
                    if (i == 4) mc.data[i].hasBird = true;
                    mc.data[i].hasSingingAnimals = true;
                    mc.data[i].environment = StageController.SceneConfigurationData.Environment.House;

                }
                else if (mc.data[i].sceneType == StageController.SceneConfigurationData.SceneType.Motion)
                {

                    mc.data[i].environment = StageController.SceneConfigurationData.Environment.House;
                    mc.data[i].hasKinectManager = true;

                    if (i == 2)
                    {
                        mc.data[i].hasDancer = true;
                    }

                    mc.data[i].hasRedPandaAvatar = true;

                    mc.data[i].environment = StageController.SceneConfigurationData.Environment.Garden;

                }

            }

        }
    }



}

