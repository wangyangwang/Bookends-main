using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{

    private static bool created = false;
    public static StageController instance = null;

    //===============Settings================================

    [System.Serializable]
    public struct SessionSettings
    {
        public StageSettings[] stageSettings;
    }

    [System.Serializable]
    public struct StageSettings
    {
        public AudioClip backgroundSoundtrack;
        //public GameObject 
        public Animator leadDancerAnimator;
    }

    [SerializeField]
    public SessionSettings sessionOneSetting;
    [SerializeField]
    public SessionSettings sessionTwoSetting;

    //===============================================

    int index = 0;
    int sceneCount = 0;

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

        index = SceneManager.GetActiveScene().buildIndex;
        sceneCount = SceneManager.sceneCountInBuildSettings;
    }

    // Use this for initialization
    void Start()
    {

    }


    public void LoadNextStage()
    {
        if (index < sceneCount)
        {
            index += 1;
        }
        else if (index == sceneCount)
        {
            index = sceneCount;
        }
        StartCoroutine(LoadingStage(index));
    }



    public void LoadPreStage()
    {
        if (index > 0)
        {
            index -= 1;
        }
        else if (index == 0)
        {
            index = 0;
        }
        StartCoroutine(LoadingStage(index));

    }



    IEnumerator LoadingStage(int sceneNum)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNum, LoadSceneMode.Single);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }



    // Update is called once per frame
    void Update()
    {

    }

    public void GoToStage(int stageIndex)
    {

    }

    public void GoToMusician(int musicianIndex)
    {

    }
}
