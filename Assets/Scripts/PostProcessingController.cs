using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PostProcessingController : MonoBehaviour
{

    public static PostProcessingController instance = null;


    private List<MonoBehaviour> filterList;


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
        //This is customized for <Camera Filter Pack>, if changing to a different filter pack, this will need to be re-written
        var filters = from filter in GetComponents<MonoBehaviour>()
                      where ((MonoBehaviour)filter).GetType().ToString().Contains("CameraFilter")
                      select filter;

        filterList = filters.ToList();
    }

    private void OnEnable()
    {
        Reset();
        OSCController.OnFilterTypeChange += UseEffect;
    }

    private void OnDisable()
    {
        OSCController.OnFilterTypeChange -= UseEffect;
    }


    private void Start()
    {

    }

    private void Reset()
    {
        UseEffect(0);
    }

    private void UseEffect(int index)
    {
        //TODO: confirm how many filters we have in total

        //no filter
        if (index == 0)
        {
            foreach (var filter in filterList)
            {
                filter.enabled = false;
            }
        }
        else if (index > 0)
        {
            int filterIndex = index - 1;
            for (int i = 0; i < filterList.Count; ++i)
            {
                filterList[i].enabled = filterIndex == i;
            }
        }


    }



}
