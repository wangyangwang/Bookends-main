using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointParticle : MonoBehaviour {

    ParticleSystem ps;

    public static JointParticle instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start () {

        ps = GetComponent<ParticleSystem>();
        
	}

    public void MakeParicle(Vector3 position)
    {
        ps.startColor = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
        gameObject.transform.position = position;
        ps.Emit(1);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
