using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageSettings
{

    public enum StageType
    {
        MOTION, COMPOSING
    }
    [Header("Stage")]
    public StageType stageType;

    [Header("Interactivity")]
    public bool useKinectControledRedPanda;

    [Header("Sound")]
    public AudioClip[] soundTracks;

    [Header("Visual")]
    public bool dancerStatus;
    public bool useParticles;
    public bool flyingBirdStatus;


}
