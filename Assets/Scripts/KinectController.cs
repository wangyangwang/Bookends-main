using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class KinectController : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    private class KinectUser
    {
        public readonly long userId;

        public KinectUser(long id)
        {
            userId = id;
        }


        public void UpdatePositions()
        {
            if (JointParticle.instance == null) return;
            for (int i = 0; i < KinectManager.Instance.GetJointCount(); i++)
            {
                JointParticle.instance.MakeParticle(KinectManager.Instance.GetJointPosition(userId, i));
            }
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

    // Update is called once per frame
    void Update()
    {
        
        //foreach(KinectUser ku in allUsers)
        //{
        //    ku.UpdatePositions();
          
        //    //if(kManager.gestu)
        //}
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
        //throw new NotImplementedException();
        if (!kManager) return;
        kManager.DetectGesture(userId, KinectGestures.Gestures.Wave);
        kManager.DetectGesture(userId, KinectGestures.Gestures.Jump);
    }

    public void UserLost(long userId, int userIndex)
    {
        //throw new NotImplementedException();
    }

    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {
        //throw new NotImplementedException();

        //print("gesture in progress");

    }

    public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint, Vector3 screenPos)
    {
        //throw new NotImplementedException();
        //print("gesture completed");

        if (gesture == KinectGestures.Gestures.Jump)
        {
           // JointParticle.instance.MakeParticle(kManager.GetJointPosition(userId, (int)KinectInterop.JointType.FootLeft));
          //  JointParticle.instance.MakeParticle(kManager.GetJointPosition(userId, (int)KinectInterop.JointType.FootRight));
        }

        if (gesture == KinectGestures.Gestures.Wave)
        {
            JointParticle.instance.MakeParticle(kManager.GetJointPosition(userId, (int)KinectInterop.JointType.HandLeft));
            pandaMat.color = Color.HSVToRGB(UnityEngine.Random.Range(0f, 1f), 1, 1);

        }


        return true;
    }

    public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture, KinectInterop.JointType joint)
    {
        //throw new NotImplementedException();
        //print("gesture canceled");
        return true;
    }
}
