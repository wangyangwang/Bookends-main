using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{

    public static EnvironmentController Instance = null;

    public GameObject garden;
    public GameObject house;

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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeToScene(StageController.SceneConfigurationData.Environment environment)
    {
        bool isGarden = environment == StageController.SceneConfigurationData.Environment.Garden;
        garden.SetActive(isGarden);
        house.SetActive(!isGarden);
    }
}
