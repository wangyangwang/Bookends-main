using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleSystemController : MonoBehaviour
{

    public enum ParticleType {
        KVANTSPRAY, UNITYBUILDIN
    }

    //TODO: move particle position to hands.
    //TODO: add world space option to Kvant Spray system 

    //fileds
    GameObject[] psChildren;
    PSUnit[] particleSystems;

    //Slider amountSlider;
    //Toggle[] enableToggles;

    public static ParticleSystemController instance = null;


    public float defaultParticleSystemAmount = 1;
    [Range(0, 2)]
    public int defaultParticleSystemIndex = 0;


    //HACK!!! FIX ME LATER!
    public Transform leftHand;
    public Transform rightHand;


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
    }


    void Start()
    {
        particleSystems = GetComponentsInChildren<PSUnit>();

        ChangeParticleType(defaultParticleSystemIndex);
        ChangeParicleAmount(defaultParticleSystemAmount);

        FollowHand = true;
    }

    private void Update()
    {
        if(FollowHand)
        {
            transform.position = rightHand.transform.position;
        }
    }


    void EnableParticle(int index, bool state)
    {
        particleSystems[index].State = state;

    }


    //public methods

    public void ChangeParicleAmount(float n)
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Amount = n;
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
