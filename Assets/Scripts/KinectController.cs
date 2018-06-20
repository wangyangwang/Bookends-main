using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class KinectController : MonoBehaviour, KinectGestures.GestureListenerInterface
{


    public delegate void GestureEvent();
    public static event GestureEvent OnWave;
    public static KinectController instance = null;


    private KinectManager kinectManager;

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
        kinectManager = KinectManager.Instance;
        //allUsers = new List<KinectUser>();

        kinectManager.SetKinectToWorldMatrix(Vector3.zero, Quaternion.identity, new Vector3(skeletonSize, skeletonSize, skeletonSize));
        if (!kinectManager.IsInitialized())
        {
            Debug.LogError("Kinect Manager is not initialized!");
        }

    }

    public void EnableRedPanda(bool state)
    {
        //TODO
        //StageController.instance.avatarRedPanda.SetActive(state);
    }

    public Vector3 GetRightHandPos()
    {
        //TODO
        return Vector3.zero;
    }
    public Vector3 GetLeftHandPos()
    {
        //TODO
        return Vector3.zero;
    }


    public void ClearUsers()
    {
        kinectManager.ClearKinectUsers();
    }


    public void UserDetected(long userId, int userIndex)
    {
        if (!kinectManager) return;
        //list of gestures to be detected
        kinectManager.DetectGesture(userId, KinectGestures.Gestures.Wave);
        kinectManager.DetectGesture(userId, KinectGestures.Gestures.Jump);
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
