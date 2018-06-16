using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSUnit : MonoBehaviour {

  

    private bool state;
    public bool State
    {
        set
        {
            state = value;
            switch (Type)
            {
                case ParticleSystemController.ParticleType.KVANTSPRAY:
                    GetComponent<Kvant.Spray>().enabled = state;
                    break;
                case ParticleSystemController.ParticleType.UNITYBUILDIN:
                    if (state)
                    {
                        GetComponent<ParticleSystem>().emissionRate = Amount;//FIXME: use modern API
                    }
                    else
                    {
                        GetComponent<ParticleSystem>().emissionRate = 0;//FIXME: use modern API
                    }
                   
                    break;
                default:
                    Debug.LogWarning("Type is not set yet!");
                    break;
            }
        }

        get
        {
            return state;
        }
    }


    private float amount;
    public float Amount
    {
        set
        {
            amount = value;
            switch (Type)
            {
                case ParticleSystemController.ParticleType.KVANTSPRAY:
                    GetComponent<Kvant.Spray>().throttle = amount;
                    break;
                case ParticleSystemController.ParticleType.UNITYBUILDIN:
                    float newRate = amount * 50f;
                    GetComponent<ParticleSystem>().emissionRate = newRate; //FIXME: user modern API // TODO: change 50 to variable
                    break;
                default:
                    Debug.LogWarning("Type is not set yet!");
                    break;
            }
        }

        get
        {
            return amount;
        }
    }



    public ParticleSystemController.ParticleType Type { get; private set; }
    

	// Use this for initialization
	void Awake () {

		if(GetComponent<Kvant.Spray>()!=null)
        {
            Type = ParticleSystemController.ParticleType.KVANTSPRAY;
        }else if(GetComponent<ParticleSystem>()!=null)
        {
            Type = ParticleSystemController.ParticleType.UNITYBUILDIN;
        }
        else
        {
            Debug.LogError("PSUnit couldn't find any matching component. Deleting PSUnit on this gameobject...");
            Destroy(this);
        }
	}


    
}
