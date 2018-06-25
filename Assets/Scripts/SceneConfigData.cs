using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

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



    public void ConvertToJson()
    {

        string jsonVivaldi = JsonUtility.ToJson(GConfig[0]);

        string path = "Assets/Resources/data.json";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(jsonVivaldi);
        writer.Close();

        ////Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(path);
        //TextAsset asset = Resources.Load("test");

    }

    public void LoadJson(){
        
    }


}

