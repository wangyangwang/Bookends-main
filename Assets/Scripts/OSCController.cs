using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Receive and send OSC data to the iPad control panel
/// </summary>
[RequireComponent(typeof(OSC))]
public class OSCController : MonoBehaviour
{
    //event types
    public delegate void FloatMsg(float m);
    public delegate void IntMsg(int m);
    public delegate void IntPlusValueMsg(int i, float v);
    public delegate void BoolMsg();

    //all events
    public static event FloatMsg OnParticleAmountChange;
    public static event IntMsg OnParticleTypeChange;
    public static event BoolMsg OnPlay;
    public static event BoolMsg OnPausePlay;
    public static event BoolMsg OnStopPlay;
    public static event FloatMsg OnJumpTo;
    public static event IntPlusValueMsg OnVolumnChange;
    public static event IntMsg OnFilterTypeChange;
    public static event BoolMsg OnKinectUserReset;
    public static event BoolMsg OnNextStage;
    public static event BoolMsg OnPreStage;


    OSC oscObject;

    public static OSCController instance = null;


    struct Paths
    {
        //particle
        public const string particleAmount = "/particle/amount"; //float:  0 - 1
        public const string particleType = "/particle/type"; //int: 0 - 3
        //player
        public const string play = "/player/play"; //int: 0-1
        public const string pause = "/player/pause"; //int: 0-1
        public const string stop = "/player/stop"; //int: 0-1
        public const string jumpTo = "player/jumpTo"; // float: 0 - 1
        //sound
        public const string volumn1 = "/volumn/one"; //float: 0 - 1
        public const string volumn2 = "/volumn/two"; //float: 0 - 1
        public const string volumn3 = "/volumn/three"; //float: 0 - 1
        public const string volumn4 = "/volumn/four"; //float: 0 - 1
        //filter
        public const string filterType = "/filter/type"; //int: 0 - 3
        //public string filterSetting;
        //kinect
        public const string kinectUserTracking = "/kinect/resetUserTracking"; //int 0-1
        //stage control
        public const string nextStage = "/stage/next";
        public const string preStage = "/stage/pre";
    }


    private void Awake()
    {
        oscObject = GetComponent<OSC>();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.LogError("More than 1 instance of OSCController is detected, deleting...");
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        oscObject.SetAllMessageHandler(ProcessMessage);
    }

    void ProcessMessage(OscMessage msg)
    {
        Debug.Log("Message from:  " + msg.address + "  Message Content:  " + msg);

        switch (msg.address)
        {
            case Paths.particleAmount:
                if (OnParticleAmountChange != null) OnParticleAmountChange(msg.GetFloat(0));
                break;

            case Paths.particleType:
                if (OnParticleTypeChange != null) OnParticleTypeChange(msg.GetInt(0));
                break;

            case Paths.play:
                if (OnPlay != null) OnPlay();
                break;

            case Paths.pause:
                if (OnPausePlay != null) OnPausePlay();
                break;

            case Paths.stop:
                if (OnStopPlay != null) OnStopPlay();
                break;

            case Paths.jumpTo:
                if (OnJumpTo != null) OnJumpTo(msg.GetFloat(0));
                break;

            case Paths.volumn1:
                if (OnVolumnChange != null) OnVolumnChange(0, msg.GetFloat(0));
                break;
            case Paths.volumn2:
                if (OnVolumnChange != null) OnVolumnChange(1, msg.GetFloat(0));
                break;
            case Paths.volumn3:
                if (OnVolumnChange != null) OnVolumnChange(2, msg.GetFloat(0));
                break;
            case Paths.volumn4:
                if (OnVolumnChange != null) OnVolumnChange(3, msg.GetFloat(0));
                break;

            case Paths.filterType:
                if (OnFilterTypeChange != null) OnFilterTypeChange(msg.GetInt(0));
                break;
               
            case Paths.kinectUserTracking:
                if (OnKinectUserReset != null) OnKinectUserReset();
                break;
            case Paths.nextStage:
                if (OnNextStage != null) OnNextStage();
                break;
            case Paths.preStage:
                if (OnPreStage != null) OnPreStage();
                break;

            default:
                Debug.LogWarning("Incoming message has no matched handler");
                break;
        }
    }
}
