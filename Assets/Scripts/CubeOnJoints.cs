using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeOnJoints : MonoBehaviour
{
    private class KinectUser
    {
        List<GameObject> jointIndicators;
        public readonly long userId;

        public KinectUser(long id)
        {
            userId = id;
            jointIndicators = new List<GameObject>();
        }

        public void UpdatePositions()
        {
            for (int i = 0; i < KinectManager.Instance.GetJointCount(); i++)
            {
                if (jointIndicators[i] == null)
                {
                    jointIndicators.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
                }
                jointIndicators[i].transform.position = KinectManager.Instance.GetJointPosition(userId, i);
            }
        }
    }

    //objects
    private KinectManager kManager;
    private List<KinectUser> allUsers;

    //parameters
    [SerializeField]
    private long jointSize = 10;

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
