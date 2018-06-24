using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Receive and send OSC data to the iPad control panel
/// </summary>
[RequireComponent(typeof(OSC))]
public class OSCController : MonoBehaviour
{
    private static bool created = false;

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
    public static event BoolMsg OnFastforward;
    public static event BoolMsg OnReverse;
    public static event IntPlusValueMsg OnVolumnChange;
    public static event IntMsg OnSingingAnimalToggle;
    public static event IntMsg OnFilterTypeChange;
    public static event BoolMsg OnKinectUserReset;

    public static event IntMsg OnStageChange;
    public static event IntMsg OnMusicianChange;

    public static OSCController instance = null;


    private OSC oscObject;

    private bool showIP = true;
    private string ip = "";


    struct Paths
    {
        //particle
        public const string particleAmount = "/particle/amount"; //float:  0 - 1
        public const string particleType = "/particle/type"; //int: 0 - 3
        //player
        public const string play = "/player/play"; //int: 0-1
        public const string pause = "/player/pause"; //int: 0-1
        public const string stop = "/player/stop"; //int: 0-1
        public const string fastForward = "/player/fastforward"; //int 0-1
        public const string reverse = "/player/reverse"; //int 0-1
        //sound
        public const string volumn1 = "/volumn/one"; //float: 0 - 1
        public const string volumn2 = "/volumn/two"; //float: 0 - 1
        public const string volumn3 = "/volumn/three"; //float: 0 - 1
        public const string volumn4 = "/volumn/four"; //float: 0 - 1
        public const string toggleAnimal = "/toggle/animal"; // int 0-3 (4 animal max) 
        //filter
        public const string filterType = "/filter/type"; //int: 0 - 5 (?)  TODO:DECISION: nail down the number of filter types

        //kinect
        public const string kinectUserTracking = "/kinect/resetUserTracking"; //int: 0-1 (1 for active, 0 triggers nothign)

        //stage control
        public const string changeStage = "/stage"; //int: 0-5 (6 stages in total)

        //musician
        public const string changeMusician = "/musician"; //int: 0-1 (2 musicians in total)
    }


    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.LogWarning("More than 1 instance of OSCController is detected, deleting...");
            Destroy(gameObject);
        }

        ip = LocalIPAddress();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            showIP = !showIP;
        }
    }

    private string LocalIPAddress()
    {
        System.Net.IPHostEntry host;
        string localIP = "";
        host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        foreach (System.Net.IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }

    private void OnGUI()
    {
        if(showIP)
        {
            GUI.color = Color.black;
            GUI.Label(new Rect(10, 10, 300, 50), "ip: " + ip + "     port:   " + oscObject.inPort);
        }
    }

    void Start()
    {
        oscObject = GetComponent<OSC>();
        oscObject.SetAllMessageHandler(ProcessMessage);
    }

    void ProcessMessage(OscMessage msg)
    {

        Debug.Log(msg);

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

            case Paths.fastForward:
                if (OnFastforward != null) OnFastforward();
                break;

            case Paths.reverse:
                if (OnReverse != null) OnReverse();
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

            case Paths.toggleAnimal:
                if (OnSingingAnimalToggle != null) OnSingingAnimalToggle(msg.GetInt(0));
                break;

            case Paths.filterType:
                if (OnFilterTypeChange != null) OnFilterTypeChange(msg.GetInt(0));
                break;

            case Paths.kinectUserTracking:
                if (OnKinectUserReset != null) OnKinectUserReset();
                break;

            case Paths.changeStage:
                if (OnStageChange != null) OnStageChange(msg.GetInt(0));
                break;

            case Paths.changeMusician:
                if (OnMusicianChange != null) OnMusicianChange(msg.GetInt(0));
                break;

            default:
                Debug.LogWarning("Incoming message has no matched handler");
                break;
        }
    }
}
