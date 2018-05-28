using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CubeOnJoints : MonoBehaviour
{
    private class KinectUser : IDisposable
    {
        private GameObject container;
        private List<GameObject> jointIndicators;
        public readonly long userId;
        public float jointSize = 2f;


        public KinectUser(long id)
        {
            userId = id;
            jointIndicators = new List<GameObject>();

            container = new GameObject("User: " + id);
            for (int i = 0; i < KinectManager.Instance.GetJointCount(); i++)
            {
                GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                g.transform.parent = container.transform;
                g.transform.localScale = new Vector3(jointSize, jointSize, jointSize);
                jointIndicators.Add(g);
            }
        }

        public void SetScale(float scale)
        {
            container.transform.localScale = new Vector3(scale, scale, scale);
        }

        public void UpdatePositions()
        {
            for (int i = 0; i < KinectManager.Instance.GetJointCount(); i++)
            {
                jointIndicators[i].transform.position = KinectManager.Instance.GetJointPosition(userId, i);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Destroy(container);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~KinectUser() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
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
                ku.Dispose();
            }
        }
    }
}
