using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenControlPanelHelper : MonoBehaviour
{

    [SerializeField]
    Slider particleAmountSlider;
    [SerializeField]
    Dropdown particleTypeDropdown;


    // Use this for initialization
    void Start()
    {
        particleAmountSlider.onValueChanged.AddListener(ChangeParticleAmount);
        particleTypeDropdown.onValueChanged.AddListener(ChangeParticleType);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChangeParticleAmount(float amount)
    {
        ParticleSystemController.instance.ChangeParicleAmount(amount);
    }

    void ChangeParticleType(int type)
    {
        ParticleSystemController.instance.ChangeParticleType(type);
    }
}
