using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewParticleSystemController : MonoBehaviour
{
    //TODO: move particle position to hands.

    //fileds
    GameObject[] psChildren;
    ParticleSystem[] particleSystems;

    //Slider amountSlider;
    //Toggle[] enableToggles;

    public static NewParticleSystemController instance = null;


    public int defaultParticleSystemAmount = 50;
    [Range(0, 2)]
    public int defaultParticleSystemIndex = 0;

    //properties
    public int ParticleSystemNumer
    {
        get
        {
            return particleSystems.Length;
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

    }


    void Start()
    {

        particleSystems = GetComponentsInChildren<ParticleSystem>();
        //for (int i = 0; i < enableToggles.Length; ++i)
        //{
        //    //add the method to the event "ToggleValueChanged"
        //    enableToggles[i].onValueChanged.AddListener(delegate { CheckAllParticles(); });
        //}

        ////UNDONE: for testing
        //if (amountSlider) amountSlider.onValueChanged.AddListener((arg0) => ChangeParicleAmount(arg0));
        ChangeParticleType(defaultParticleSystemIndex);
        ChangeParicleAmount(defaultParticleSystemAmount);
    }


    void EnableParticle(int index, bool state)
    {
        var em = particleSystems[index].emission;
        em.enabled = state;
    }



    //public methods

    public void ChangeParicleAmount(float n)
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            var em = particleSystems[i].emission;
            em.rateOverTime = n;
        }

    }


    public void ChangeParticleType(int index)
    {
        for (int i = 0; i < particleSystems.Length; ++i)
        {
            EnableParticle(i, i == index);
        }
    }







}
