using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{


    public static BirdController Instance = null;

    //TODO:

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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAnimation(int index)
    {
        //todo
    }

}
