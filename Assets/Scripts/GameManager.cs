using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //our league of controllers
    OSCController oscContr;
    AudioController audioContr;
    ParticleSystemController particleSystemContr;
    KinectController kinectContr;
    StageController stageContr;
    PostProcessingController postProcessingContr;


    private void Awake()
    {
        oscContr = OSCController.instance;
        particleSystemContr = ParticleSystemController.instance;
        audioContr = AudioController.instance;
        kinectContr = KinectController.instance;
    }

    // Use this for initialization
    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {

    }
}
