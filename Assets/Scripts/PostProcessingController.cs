using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PostProcessingController : MonoBehaviour
{

    public static PostProcessingController instance = null;


    List<MonoBehaviour> filterList;

    public int FilterCount
    {
        get
        {
            return filterList.Count();
        }
    }

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


    void Start()
    {
        //This is customized for <Camera Filter Pack>, if changing to a different filter pack, this will need to be re-written
        //var mainCam = Camera.main.transform;
        var filters = from filter in GetComponents<MonoBehaviour>()
                      where ((MonoBehaviour)filter).GetType().ToString().Contains("CameraFilter")
                      select filter;

        filterList = filters.ToList();

    }


    void Update()
    {

    }


    public void UseEffect(int index)
    {
        for (int i = 0; i < FilterCount; ++i)
        {
            filterList[i].enabled = index == i;
        }
    }



}
