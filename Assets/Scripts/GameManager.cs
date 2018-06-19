using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// main game manager, the palce to get all event call and invoke functions in all sub controllers
/// </summary>
public class GameManager : MonoBehaviour
{


    private static bool created = false;


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
        OSCController.OnStageChange += StageChange;
        OSCController.OnMusicianChange += MusicianChange;


        KinectController.OnWave += OnUserWave;


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
        OSCController.OnStageChange -= StageChange;
        OSCController.OnMusicianChange -= MusicianChange;

        KinectController.OnWave -= OnUserWave;
    }

    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }

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
    private void Start()
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
    private void Update()
    {

    }

    //callback methods
    private void Play()
    {
        Debug.Log("play");
        PlayController.instance.Play();
    }

    private void StopPlay()
    {
        Debug.Log("stop");
        PlayController.instance.StopPlay();
    }

    private void PausePlay()
    {
        Debug.Log("pause");
        PlayController.instance.PausePlay();
    }

    private void JumpTo(float p)
    {
        Debug.Log("Jumping to percentage " + p);
        PlayController.instance.JumpTo(p);
    }

    private void ParticleAmountChange(float newAmount)
    {
        Debug.Log("Changing Partile Amount to  " + newAmount);
        ParticleSystemController.instance.ChangeParicleAmount(newAmount);
    }

    private void ParticleTypeChange(int typeIndex)
    {
        Debug.Log("Changing to Particle Type " + typeIndex);
        ParticleSystemController.instance.ChangeParticleType(typeIndex);
    }

    private void VolumnChange(int which, float newVol)
    {
        AudioController.instance.ChangeVolumn(which, newVol);
        Debug.Log("Changing sound track " + which + " to volumn " + newVol);
    }

    private void FilterTypeChange(int newTypeIndex)
    {
        Debug.Log("Changing filter type to  " + newTypeIndex);
        //TODO
    }

    private void ResetUserTracking()
    {
        Debug.Log("Cleared all kinect users.");
        //TODO
    }

    private void StageChange(int targetStage)
    {
        Debug.Log("Chang stage to " + targetStage);
        StageController.instance.GoToStage(targetStage);
    }

    private void MusicianChange(int targetMusician)
    {
        Debug.Log("Change musician to " + targetMusician);
        StageController.instance.GoToMusician(targetMusician);
    }

    private void OnUserWave()
    {

    }

}

