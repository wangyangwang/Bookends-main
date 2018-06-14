using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// main game manager, the palce to get all event call and invoke functions in all sub controllers
/// </summary>
public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    private void OnEnable()
    {
        //link all events
        OSCController.OnPlay += Play;
        OSCController.OnStopPlay += StopPlay;
        OSCController.OnPausePlay += PausePlay;
        OSCController.OnJumpTo += JumpTo;
        OSCController.OnVolumnChange += VolumnChange;
        OSCController.OnFilterTypeChange += FilterTypeChange;
        OSCController.OnParticleAmountChange += ParticleAmountChange;
        OSCController.OnParticleTypeChange += ParticleTypeChange;
        OSCController.OnKinectUserReset += ResetUserTracking;
        OSCController.OnNextStage += ToNextStage;
        OSCController.OnPreStage += ToPreStage;
    }

    private void OnDisable()
    {
        //unlink all events
        OSCController.OnPlay -= Play;
        OSCController.OnStopPlay -= StopPlay;
        OSCController.OnPausePlay -= PausePlay;
        OSCController.OnJumpTo -= JumpTo;
        OSCController.OnVolumnChange -= VolumnChange;
        OSCController.OnFilterTypeChange -= FilterTypeChange;
        OSCController.OnParticleAmountChange -= ParticleAmountChange;
        OSCController.OnParticleTypeChange -= ParticleTypeChange;
        OSCController.OnKinectUserReset -= ResetUserTracking;
        OSCController.OnNextStage -= ToNextStage;
        OSCController.OnPreStage -= ToPreStage;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.LogError("found more than 1 GameManager, destroying " + gameObject.name);
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {

        print("----Checking all controllers...");

        if (!PlayController.instance)
        {
            Debug.LogError("cannot find PlayController");
        }

        if (!OSCController.instance)
        {
            Debug.LogError("cannot find OSCController");
        }

        if (!KinectController.instance)
        {
            Debug.LogError("cannot find KinectController");
        }

        if (!StageController.instance)
        {
            Debug.LogError("cannot find StageController");
        }
        if (!ParticleSystemController.instance)
        {
            Debug.LogError("cannot find ParticleSystemController");
        }
        if (!PostProcessingController.instance)
        {
            Debug.LogError("cannot find PostProcessingController");
        }
        print("----checking done.");

    }

    // Update is called once per frame
    void Update()
    {

    }

    //callback methods
    void Play()
    {
        PlayController.instance.Play();
    }

    void StopPlay()
    {
        PlayController.instance.StopPlay();
    }

    void PausePlay()
    {
        PlayController.instance.PausePlay();
    }

    void JumpTo(float p)
    {
        PlayController.instance.JumpTo(p);
    }

    void ParticleAmountChange(float newAmount)
    {
        ParticleSystemController.instance.ChangeParicleAmount(newAmount);
    }

    void ParticleTypeChange(int typeIndex)
    {
        //FIXME: change to EnableParticle
    }

    void VolumnChange(int which, float newVol)
    {
        //TODO
    }

    void FilterTypeChange(int newTypeIndex)
    {
        //TODO
    }

    void ResetUserTracking()
    {
        //TODO
    }


    void ToNextStage()
    {
        StageController.instance.LoadNextStage();
    }

    void ToPreStage()
    {
        StageController.instance.LoadPreStage();
    }

}

