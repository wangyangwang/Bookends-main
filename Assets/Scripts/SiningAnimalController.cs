using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiningAnimalController : MonoBehaviour
{

    public static SiningAnimalController Instance = null;


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

    void Start()
    {

    }

}
