using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PScontroller : MonoBehaviour {
    GameObject[] psChildren;
    Kvant.Spray[] sprays;
    int childrenNum;
    public Slider amountSlider;
    public Toggle[] enableToggles;


    // Use this for initialization
    void Awake () {
        childrenNum = transform.childCount;
        if (childrenNum > 0)
        {
            psChildren = new GameObject [childrenNum];
            sprays = new Kvant.Spray[childrenNum];
        }
        
    }


    private void Start()
    {
        for (int i = 0; i < childrenNum; i++)
        {
            psChildren[i] = gameObject.transform.GetChild(i).gameObject;
            sprays[i] = psChildren[i].GetComponent<Kvant.Spray>();
            
        }
    }



    // Update is called once per frame
    void Update () {
        ChangeParicleAmount();
        EnableParticle();
	}


    public void ChangeParicleAmount()
    {
        for (int i = 0; i < childrenNum; i++)
        {
            sprays[i].throttle = amountSlider.value;
        }

    }

    public void EnableParticle()
    {
        for (int i = 0; i < childrenNum; i++)
        {
            sprays[i].enabled = enableToggles[i].isOn;
        } 
    }
}
