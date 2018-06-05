using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yang
{
    public class GestureListener : MonoBehaviour
    {

        KinectManager kmanager;

        // Use this for initialization
        void Start()
        {
            kmanager = GetComponent<KinectManager>();                 
        }

        // Update is called once per frame
        void Update()
        {
            //kmanager.GetGestureAtIndex( );
        }
    }

}
