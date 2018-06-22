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

    private void OnEnable()
    {
        StageController.OnStageChange += OnStageChange;
    }

    private void OnDisable()
    {
        StageController.OnStageChange -= OnStageChange;
    }


    private void OnStageChange()
    {
        UpdateEnvironment();
    }


    private void UpdateEnvironment()
    {
        bool isGarden = (StageController.Config.environment == StageController.SceneConfigurationData.Environment.Garden);
        garden.SetActive(isGarden);
        house.SetActive(!isGarden);
    }
}
