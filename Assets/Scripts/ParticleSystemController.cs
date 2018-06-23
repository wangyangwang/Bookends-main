using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Control particle system type and amount, and place them on left & right hands of redpanda
/// </summary>
public class ParticleSystemController : MonoBehaviour
{
    //FIXME: two hands particle system not working properly.

    public static ParticleSystemController Instance = null;
    public Transform rightHand; //HACK!!!
    public Transform leftHand;  //HACK!!!


    private GameObject clonedPS;
    private PSUnit[] particleSystems;


    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        particleSystems = GetComponentsInChildren<PSUnit>();

    }

    private void ResetSettings()
    {
        ChangeParticleType(DATA.DEFAULT_PARTICLE_TYPE);
        ChangeParicleAmount(DATA.DEFAULT_PARTICLE_AMOUNT);
    }

    private void OnEnable()
    {
        ResetSettings();

        OSCController.OnParticleTypeChange += ChangeParticleType;
        OSCController.OnParticleAmountChange += ChangeParicleAmount;
    }

    private void OnDisable()
    {
        OSCController.OnParticleTypeChange -= ChangeParticleType;
        OSCController.OnParticleAmountChange -= ChangeParicleAmount;

        if (clonedPS != null)
        {
            Destroy(clonedPS);
        }
    }

    private void Update()
    {
        //HACK
        if (rightHand == null || leftHand == null) return;
        transform.position = rightHand.transform.position;
        if (clonedPS != null)
        {
            clonedPS.transform.position = leftHand.transform.position;
        }

    }


    private void ChangeParicleAmount(float n)
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Amount = n;
            clonedPS.GetComponent<PSUnit>().Amount = n;
        }
    }

    private void ChangeParticleType(int index)
    {
        for (int i = 0; i < particleSystems.Length; ++i)
        {
            particleSystems[i].State = i == index;
        }

        if (clonedPS != null)
        {
            Destroy(clonedPS);
        }
        clonedPS = Instantiate<GameObject>(particleSystems[index].gameObject);

    }

}
