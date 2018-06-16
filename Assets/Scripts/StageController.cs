using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour {

    public static StageController instance = null;
    public Button nextButton;
    public Button preButton;

    int index = 0;
    int sceneCount = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(gameObject);
        }

        index = SceneManager.GetActiveScene().buildIndex;
        sceneCount = SceneManager.sceneCountInBuildSettings;
    }

    // Use this for initialization
    void Start () {
        if (!nextButton || !preButton)
            return;

        nextButton.onClick.AddListener(LoadNextStage);
        preButton.onClick.AddListener(LoadPreStage);

    }


    public void LoadNextStage()
    {
        if (index < sceneCount)
        {
            index += 1;
        }else if (index == sceneCount)
        {
            index = sceneCount;
        }
        StartCoroutine(LoadingStage(index));
    }



    public void LoadPreStage()
    {
        if(index > 0)
        {
            index -= 1;   
        } else if(index == 0)
        {
            index = 0;
        }
        StartCoroutine(LoadingStage(index));

    }



    public IEnumerator LoadingStage(int sceneNum)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNum, LoadSceneMode.Single);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }


	
	// Update is called once per frame
	void Update () {
		
	}
}
