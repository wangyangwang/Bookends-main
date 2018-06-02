using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MakeParicleFromJoints : MonoBehaviour
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
                JointParticle.instance.MakeParicle(KinectManager.Instance.GetJointPosition(userId, i));
            }
        }

    }

    //objects
    private KinectManager kManager;
    private List<KinectUser> allUsers;

    //parameters
    [SerializeField]
    private float skeletonSize = 5f;

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
        foreach(KinectUser ku in allUsers)
        {
            ku.UpdatePositions();
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
}
