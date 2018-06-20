using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleSystemController : MonoBehaviour
{

    public enum ParticleType
    {
        KVANTSPRAY, UNITYBUILDIN
    }

    //TODO: move particle position to hands.

    //fileds
    PSUnit[] particleSystems;

    public GameObject clonePS;
    PSUnit clonePSUnit;

    //Slider amountSlider;
    //Toggle[] enableToggles;

    public static ParticleSystemController instance = null;


    public float defaultParticleSystemAmount = 10;
    [Range(0, 4)]
    public int defaultParticleSystemIndex = 0;



    //HACK!!! FIX ME LATER!
    public Transform rightHand;
    public Transform leftHand;



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

        particleSystems = GetComponentsInChildren<PSUnit>();

    }



    private void OnEnable()
    {
        ChangeParticleType(defaultParticleSystemIndex);
        ChangeParicleAmount(defaultParticleSystemAmount);
    }

    private void OnDisable()
    {

    }

    private void Update()
    {
        //HACK
        if (rightHand == null || leftHand == null) return;
        transform.position = rightHand.transform.position;
        if (clonePS != null)
        {
            clonePS.transform.position = leftHand.transform.position;
        }

    }



    //public methods

    public void ChangeParicleAmount(float n)
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Amount = n;
            //clone the psUnit
            clonePSUnit = clonePS.GetComponent<PSUnit>();
            clonePSUnit.Amount = n;
        }

        //clone the psUnit
        clonePSUnit = clonePS.GetComponent<PSUnit>();


    }

    public void ChangeParticleType(int index)
    {
        for (int i = 0; i < particleSystems.Length; ++i)
        {
            particleSystems[i].State = i == index;
        }

        //clone the activated PS
        var psInstance = clonePS;
        clonePS = Instantiate<GameObject>(transform.GetChild(index).gameObject);
        if (psInstance == null)
        {
            psInstance = clonePS;
        }
        else if (psInstance != null)
        {
            Destroy(psInstance);
        }

    }

    internal void EnableParticles(bool useParticles)
    {
        foreach (PSUnit psunit in particleSystems)
        {
            psunit.gameObject.SetActive(useParticles);
        }
    }
}
