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
        public StageController.SceneConfigurationData[] main;
    }

    [SerializeField]
    [Header("0 means vivaldi, 1 means mozart")]
    public MusicianConfig[] AllMusicianConfig;



    private void Start()
    {
        foreach (var m in AllMusicianConfig)
        {
            if (m.main.Length == 0)
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
            AllMusicianConfig[i].main = new StageController.SceneConfigurationData[STAGE_COUNT_EACH_MUSICIAN];
            for (int y = 0; y < AllMusicianConfig[i].main.Length; y++)
            {
                AllMusicianConfig[i].main[y] = new StageController.SceneConfigurationData();
            }

        }



        //foreach (var musicianConfig in AllMusicianConfig)
        //{
        //    for (int i = 0; i < STAGE_COUNT_EACH_MUSICIAN; i++)
        //    {
        //        musicianConfig.main = new StageController.SceneConfigurationData[STAGE_COUNT_EACH_MUSICIAN];
        //    }
        //}

        foreach (var musicianConfig in AllMusicianConfig)
        {
            for (int i = 0; i < STAGE_COUNT_EACH_MUSICIAN; i++)
            {
                //setup music slot count
                if (i == 0 || i == 1 || i == 2)
                {
                    musicianConfig.main[i].singingMusics = new AudioClip[0];
                    musicianConfig.main[i].sceneType = StageController.SceneConfigurationData.SceneType.Motion;

                }
                else if (i == 3)
                {
                    musicianConfig.main[i].singingMusics = new AudioClip[3];
                    musicianConfig.main[i].sceneType = StageController.SceneConfigurationData.SceneType.Composing;

                }
                else if (i == 4)
                {
                    musicianConfig.main[i].singingMusics = new AudioClip[4];
                    musicianConfig.main[i].sceneType = StageController.SceneConfigurationData.SceneType.Composing;
                }


                if (musicianConfig.main[i].sceneType == StageController.SceneConfigurationData.SceneType.Composing)
                {
                    musicianConfig.main[i].hasDancer = false;
                    musicianConfig.main[i].hasRedPandaAvatar = false;
                    musicianConfig.main[i].environment = StageController.SceneConfigurationData.Environment.House;
                }
                else
                {
                    musicianConfig.main[i].hasDancer = true;
                    musicianConfig.main[i].hasRedPandaAvatar = true;
                    musicianConfig.main[i].environment = StageController.SceneConfigurationData.Environment.Garden;

                }
            }

        }
    }



}

