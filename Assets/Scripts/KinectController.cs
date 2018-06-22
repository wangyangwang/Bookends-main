using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class KinectController : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    public delegate void GestureEvent();
    public static event GestureEvent OnWave;
    public static KinectController Instance = null;

    private KinectManager kinectManager;
    private float skeletonSize = 5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Debug.LogWarning("Found more than 1 kinectControler instance");
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        OSCController.OnKinectUserReset += ClearUsers;
    }

    private void OnDisable()
    {
        OSCController.OnKinectUserReset -= ClearUsers;
    }

    private void Start()
    {
        kinectManager = KinectManager.Instance;
        kinectManager.SetKinectToWorldMatrix(Vector3.zero, Quaternion.identity, new Vector3(skeletonSize, skeletonSize, skeletonSize));
        if (!kinectManager.IsInitialized()) Debug.LogError("Kinect Manager is not initialized!");
    }

    public Vector3 GetRightHandPos()
    {
        //HASTODO
        return Vector3.zero;
    }

    public Vector3 GetLeftHandPos()
    {
        //HASTODO
        return Vector3.zero;
    }




    //INTERFACE FORCES METHODS
    //................................
    //................................
    private void ClearUsers()
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
