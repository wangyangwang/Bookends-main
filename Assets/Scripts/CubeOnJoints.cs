using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeOnJoints : MonoBehaviour
{

    //objects
    private KinectManager kManager;

    //parameters
    [SerializeField]
    private long jointSize = 10;


    // Use this for initialization
    void Start()
    {

        kManager = KinectManager.Instance;

        if (!kManager.IsInitialized())
        {
            Debug.LogError("Kinect Manager is not initialized!");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnDrawGizmos()
    {
        if (!kManager) return;
        for (int i = 0; i < kManager.GetUsersCount(); i++)
        {
            long userid = kManager.GetUserIdByIndex(i);
            Vector3 headVec = kManager.GetJointKinectPosition(userid, (int)KinectInterop.JointType.Head);
            Gizmos.DrawCube(headVec, new Vector3(jointSize, jointSize, jointSize));

            Vector3 leftHandVec = kManager.GetJointKinectPosition(userid, (int)KinectInterop.JointType.HandLeft);
            Gizmos.DrawCube(leftHandVec, new Vector3(jointSize, jointSize, jointSize));
        }
    }

    private void OnNewUser()
    {

    }
}
