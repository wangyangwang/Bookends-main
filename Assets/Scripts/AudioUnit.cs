using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUnit : MonoBehaviour
{

    AudioSource[] sources;

    private bool active;
    public bool Active
    {
        get
        {
            return active;
        }

        set
        {
            //reset volumn
            foreach (AudioSource a in sources)
            {
                a.volume = 1;
            }

            active = value;
        }
    }

    public StageController.StageType playControlStageType;

    // Use this for initialization
    void Start()
    {
        sources = GetComponentsInChildren<AudioSource>();

        //HACK
        if (sources.Length < 1) Debug.LogError("no audio source found!");
        if (sources.Length == 1) playControlStageType = StageController.StageType.MOTION;
        if (sources.Length == 4) playControlStageType = StageController.StageType.COMPOSING;

        Debug.Log("audio unit" + gameObject.name + " type has changed to " + playControlStageType.ToString());
    }

    public void Play()
    {
        if (!active) return;
        foreach (AudioSource a in sources)
        {
            a.Play();
        }
    }

    public void ChangeVolumn(float v, int index)
    {
        if (!active || playControlStageType == StageController.StageType.MOTION) return;
        sources[index].volume = v;
    }

    public void Stop()
    {
        if (!active) return;
        foreach (AudioSource a in sources)
        {
            a.Stop();
        }
    }
    public void Pause()
    {
        if (!active) return;
        foreach (AudioSource a in sources)
        {
            a.Pause();
        }
    }

    public void JumpTo(float p)
    {
        if (!active) return;
        float realSeconds = p * sources[0].clip.length;
        foreach (AudioSource a in sources)
        {
            a.time = realSeconds;
        }
    }




}
