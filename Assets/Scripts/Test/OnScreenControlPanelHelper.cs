using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenControlPanelHelper : MonoBehaviour
{

    [SerializeField]
    Slider particleAmountSlider;
    [SerializeField]
    Slider particleTypeSlider;
    [SerializeField]
    Slider changeMusicianSlider;
    [SerializeField]
    Slider changeStageSlider;


    private static bool created = false;


    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

    // Use this for initialization
    void Start()
    {
        //particleAmountSlider.onValueChanged.AddListener(ChangeParticleAmount);
        //particleTypeSlider.onValueChanged.AddListener(ChangeParticleType);
        //changeMusicianSlider.onValueChanged.AddListener((arg0) => ChangeMusician((int)arg0));
        //changeStageSlider.onValueChanged.AddListener((arg0) => ChangeStage((int)arg0));
    }

    // Update is called once per frame
    void Update()
    {

    }

    //void ChangeMusician(int i)
    //{
    //    OSCController.OnStageChange(i);
    //    //StageController.Instance.GoToMusician(i);
    //}

    //void ChangeStage(int i)
    //{
    //    StageController.Instance.GoToStage(i);
    //}

    //void ChangeParticleAmount(float amount)
    //{
    //    ParticleSystemController.Instance.ChangeParicleAmount(amount);
    //}

    //void ChangeParticleType(float type)
    //{
    //    ParticleSystemController.Instance.ChangeParticleType((int)type);
    //}
}
