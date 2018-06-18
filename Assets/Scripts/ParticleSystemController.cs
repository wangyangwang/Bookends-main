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

    GameObject clonePS;
    PSUnit clonePSUnit;
    //Slider amountSlider;
    //Toggle[] enableToggles;

    public static ParticleSystemController instance = null;


    public float defaultParticleSystemAmount = 10;
    [Range(0, 3)]
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
            clonePS.transform.position = leftHand.transform.position;

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
        //clone the psUnit
        clonePSUnit = clonePS.GetComponent<PSUnit>();


    }

    public void ChangeParticleType(int index)
    {
        for (int i = 0; i < particleSystems.Length; ++i)
        {
            EnableParticle(i, i == index);
        }
        //clone the activated PS
        clonePS = Instantiate(transform.GetChild(index).gameObject, leftHand.transform.position,leftHand.transform.rotation);
    }




}
