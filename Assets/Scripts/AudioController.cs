using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{

    public static AudioController instance;

    AudioSource audioSource;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.LogError("Found more than 1 AudioController, destroying...");
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        audioSource.Play();
    }

    public void PausePlay()
    {
        audioSource.Pause();
    }

    public void StopPlay()
    {
        audioSource.Stop();
    }

    public void JumpTo(float p)
    {
        float realSeconds = p / audioSource.clip.length;
        audioSource.time = realSeconds;
    }
}
