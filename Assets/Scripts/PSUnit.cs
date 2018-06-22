using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ParticleSystem))]
public class PSUnit : MonoBehaviour
{
    
    public bool State
    {
        set
        {
            state = value;
            var em = ps.emission;
            em.enabled = state;
        }

        get
        {
            return state;
        }
    }


    public float Amount
    {
        set
        {
            amount = value;
            float newRate = amount * DATA.PARTICLE_AMOUNT_MULTIPLIER;
            var em = ps.emission;
            em.rateOverTime = newRate;
        }

        get
        {
            return amount;
        }
    }


    private float amount;
    private bool state;
    private ParticleSystem ps;


    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }



}
