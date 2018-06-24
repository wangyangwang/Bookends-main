using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioUnit : MonoBehaviour
{
    private AudioSource source;
    private float vol;
    private bool audible;
    private AudioClip clip;

    

    public float Vol
    {
        get
        {
            return vol;
        }
        set
        {
            vol = value;
            if (Audible) source.volume = vol;
        }
    }

    public bool Audible
    {
        get
        {
            return audible;
        }

        set
        {
            audible = value;
            source.volume = audible ? Vol : 0;
        }
    }

    public AudioClip Clip
    {
        get
        {
            return clip;
        }

        set
        {
            clip = value;
            source.clip = clip;
        }
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {


    }



    private void OnEnable()
    {
        source.Stop();
        source.loop = false;
        source.playOnAwake = false;
        Vol = 1;
    }

    public void Play() { source.Play(); }
    public void Pause() { source.Pause(); }
    public void Stop() { source.Stop(); }
    public void FastForward() { source.time = source.time + DATA.FAST_FORWARD_STEP; }
    public void Reverse() { source.time = source.time - DATA.FAST_FORWARD_STEP; }

}
