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
    public static ParticleSystemController Instance = null;
    public Transform rightHand;
    public Transform leftHand;

    private GameObject clonedPS;
    private PSUnit[] particleSystems;
    private Vector3 oldLeftHandPos;
    private Vector3 oldRightHandPos;
    private float leftHandSpeed = 0;
    private float rightHandSpeed = 0;
    private float leftParticleMultiplier;
    private float rightParticleMultiplier;
    private bool inited = false;


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
        oldLeftHandPos = new Vector3();
        oldRightHandPos = new Vector3();
    }

    private void Start()
    {
        if (!leftHand || !rightHand)
        {
            Debug.LogError("ParticleSystemController's hand transform[s] is/are not assigned, particlea will not be able to follow hands!!");
        }
        ResetSettings();
        inited = true;
    }

    private void ResetSettings()
    {
        //  leftAmount = DATA.DEFAULT_PARTICLE_AMOUNT;
        // rightAmount = DATA.DEFAULT_PARTICLE_AMOUNT;
        ChangeParticleType(DATA.DEFAULT_PARTICLE_TYPE);
        ChangeParicleAmount(DATA.DEFAULT_PARTICLE_AMOUNT);
    }

    private void OnEnable()
    {
        //note: 
        //OnEnable() will get called earlier before psunit's  particle system [ps] gets its value assigned;
        //and return null so don't run ResetSettings() on 1st run instead we put ResetSettings() into start. 
        if (inited) ResetSettings();
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

        if (rightHand == null || leftHand == null)
        {
            return;
        }

        transform.position = rightHand.transform.position;
        if (clonedPS != null)
        {
            clonedPS.transform.position = leftHand.transform.position;
        }

        //calculate speeds
        leftHandSpeed = (leftHand.position - oldLeftHandPos).magnitude * 100;
        rightHandSpeed = (rightHand.position - oldRightHandPos).magnitude * 100;

        //normalize speeds
        //manually tested:  speed range 1 - 20
        leftHandSpeed /= 20;
        leftHandSpeed = Mathf.Clamp01(leftHandSpeed);

        rightHandSpeed /= 20;
        rightHandSpeed = Mathf.Clamp01(rightHandSpeed);

        //assign amounts
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Amount = leftParticleMultiplier * leftHandSpeed;
            clonedPS.GetComponent<PSUnit>().Amount = rightParticleMultiplier * rightHandSpeed;
        }

        //save values for next calculation
        oldLeftHandPos = leftHand.position;
        oldRightHandPos = rightHand.position;
    }

    private void ChangeParicleAmount(float n)
    {
        leftParticleMultiplier = n;
        rightParticleMultiplier = n;
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
