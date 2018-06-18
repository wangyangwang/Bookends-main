using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class KinectController : MonoBehaviour, KinectGestures.GestureListenerInterface
{


    public delegate void GestureEvent();
    public static event GestureEvent OnWave;

    public static KinectController instance = null;


    //objects
    private KinectManager kManager;
    //private List<KinectUser> allUsers;

    //parameters
    [SerializeField]
    private float skeletonSize = 5f;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.LogError("Found more than 1 kinectControler instance");
            Destroy(gameObject);
        }
    }


    // Use this for initialization
    void Start()
    {
        kManager = KinectManager.Instance;
        //allUsers = new List<KinectUser>();

        kManager.SetKinectToWorldMatrix(Vector3.zero, Quaternion.identity, new Vector3(skeletonSize, skeletonSize, skeletonSize));
        if (!kManager.IsInitialized())
        {
            Debug.LogError("Kinect Manager is not initialized!");
        }

    }

    public Vector3 GetRightHandPos(){
        //TODO
        return Vector3.zero;
    }
    public Vector3 GetLeftHandPos(){
        //TODO
        return Vector3.zero;
    }


    public void ClearUsers()
    {
        kManager.ClearKinectUsers();
    }


    public void UserDetected(long userId, int userIndex)
    {
        if (!kManager) return;
        //list of gestures to be detected
        kManager.DetectGesture(userId, KinectGestures.Gestures.Wave);
        kManager.DetectGesture(userId, KinectGestures.Gestures.Jump);
    }

    public void UserLost(long userId, int userIndex)
    {
        //...
    }

    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {
        //...
    }

    public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint, Vector3 screenPos)
    {

        if (gesture == KinectGestures.Gestures.Jump)
        {
            //do stuff
        }

        if (gesture == KinectGestures.Gestures.Wave)
        {
            if (OnWave != null) OnWave();
        }
        return true;
    }

    public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint)
    {
        return true;
    }
}
