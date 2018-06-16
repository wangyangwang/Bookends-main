﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleSystemController : MonoBehaviour
{
    //TODO: move particle position to hands.

    //fileds
    GameObject[] psChildren;
    Kvant.Spray[] particleSystems;

    //Slider amountSlider;
    //Toggle[] enableToggles;

    public static ParticleSystemController instance = null;


    public float defaultParticleSystemAmount = 1;
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


    public bool FollowHand { get; set; }


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

        particleSystems = GetComponentsInChildren<Kvant.Spray>();

        ChangeParticleType(defaultParticleSystemIndex);
        ChangeParicleAmount(defaultParticleSystemAmount);

        FollowHand = true;
        //TODO: particle follow hands
    }

    private void Update()
    {
        
    }


    void EnableParticle(int index, bool state)
    {
        particleSystems[index].enabled = state;
    }


    //public methods

    public void ChangeParicleAmount(float n)
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].throttle = n;
        }

    }

    //public void CheckAllParticles()
    //{
    //    for (int i = 0; i < sprays.Length; i++)
    //    {
    //        EnableParticle(i, enableToggles[i].isOn);
    //    }
    //}

    public void ChangeParticleType(int index)
    {
        for (int i = 0; i < particleSystems.Length; ++i)
        {
            EnableParticle(i, i == index);
        }
    }




}
