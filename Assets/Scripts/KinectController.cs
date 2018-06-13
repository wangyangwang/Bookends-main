using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class KinectController : MonoBehaviour, KinectGestures.GestureListenerInterface
{


    public static KinectController instance = null;

    /// <summary>
    /// nested class for storing kinect user id and more.
    /// </summary>
    private class KinectUser
    {
        public readonly long userId;

        public KinectUser(long id)
        {
            userId = id;
        }
    }


    //objects
    private KinectManager kManager;
    private List<KinectUser> allUsers;

    //parameters
    [SerializeField]
    private float skeletonSize = 5f;
    [SerializeField]
    private Material pandaMat;

    //setup event listeners
    private void OnEnable()
    {
        KinectManager.OnNewUser += OnNewUser;
        KinectManager.OnLostUser += OnLostUser;
    }

    private void OnDisable()
    {
        KinectManager.OnNewUser -= OnNewUser;
        KinectManager.OnLostUser -= OnLostUser;
    }


    void Awake()
    {
        if (instance == null) instance = this;
        if (instance != null)
        {
            Debug.LogError("Found more than 1 kinectControler instance");
            Destroy(gameObject);
        }
    }


    // Use this for initialization
    void Start()
    {
        kManager = KinectManager.Instance;
        allUsers = new List<KinectUser>();

        kManager.SetKinectToWorldMatrix(Vector3.zero, Quaternion.identity, new Vector3(skeletonSize, skeletonSize, skeletonSize));
        if (!kManager.IsInitialized())
        {
            Debug.LogError("Kinect Manager is not initialized!");
        }

    }


    private void OnNewUser(long userId)
    {
        KinectUser newUser = new KinectUser(userId);
        allUsers.Add(newUser);
    }

    private void OnLostUser(long userId)
    {
        foreach (KinectUser ku in allUsers)
        {
            if (ku.userId == userId)
            {
                allUsers.Remove(ku);
            }
        }
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
            //do stuff
        }
        return true;
    }

    public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint)
    {
        return true;
    }
}
