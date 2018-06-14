using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleSystemController : MonoBehaviour
{
    //fileds
    GameObject[] psChildren;
    Kvant.Spray[] sprays;

    Slider amountSlider;
    Toggle[] enableToggles;

    public static ParticleSystemController instance = null;


    //properties
    public int ParticleSystemNumer
    {

        get
        {
            return sprays.Length;
        }
    }


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //enableToggles = 
    }


    void Start()
    {
        if (!amountSlider)
        {
            Debug.LogError("amoutn slider is not linked.");
            return;
        }


        sprays = GetComponentsInChildren<Kvant.Spray>();
        print(sprays.Length);


        for (int i = 0; i < enableToggles.Length; ++i)
        {
            //add the method to the event "ToggleValueChanged"
            enableToggles[i].onValueChanged.AddListener(delegate { CheckAllParticles(); });
        }
        
        amountSlider.onValueChanged.AddListener((arg0) => ChangeParicleAmount(arg0));

        Init();
    }


    void Init()
    {
        CheckAllParticles();
        ChangeParicleAmount(amountSlider.value);
    }



    //public methods

    public void ChangeParicleAmount(float n)
    {
        for (int i = 0; i < sprays.Length; i++)
        {
            sprays[i].throttle = n;
        }

    }

    public void CheckAllParticles()
    {
        for (int i = 0; i < sprays.Length; i++)
        {
            EnableParticle(i, enableToggles[i].isOn);
        }
    }

    public void EnableParticle(int index, bool state)
    {
        sprays[index].enabled = state;
    }



}
