using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// For testings without an external OSC sender
/// </summary>
public class VirtualLocalControlPanel : MonoBehaviour
{

    [SerializeField]
    Slider particleAmountSlider;
    [SerializeField]
    Dropdown particleTypeDropdown;
    [SerializeField]
    Button playButton;
    [SerializeField]
    Button replayButton;
    [SerializeField]
    Button nextStageButton;
    [SerializeField]
    Button preStageButton;
    [SerializeField]
    Button resetUserTrackingButton;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


}
