using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{


    //TODO: MELODY
    //We need to figure out what should bird do

    public static BirdController Instance = null;

    private bool _loop;
    public bool Loop
    {
        get
        {
            return _loop;
        }
        set
        {
            //TODO
            _loop = value;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }


    private void Update()
    {
        //Does it need to be traslated in space?
    }

    private void PlayAnimation(int index)
    {
        //TODO: MELODY
        //PLAY ANIMATION 1
        //PLAY ANIMATION 2
        //PLAY ANIMATION 3
    }

}
