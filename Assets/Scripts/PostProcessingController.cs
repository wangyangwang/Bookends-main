using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessingController : MonoBehaviour
{
    
    public static PostProcessingController instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
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

    public void UseEffect(int i)
    {

    }



}
