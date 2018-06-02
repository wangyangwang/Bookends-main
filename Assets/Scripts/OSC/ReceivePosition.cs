using UnityEngine;
using System.Collections;

public class ReceivePosition : MonoBehaviour {
    
   	public OSC osc;


	// Use this for initialization
	void Start () {
	   //osc.SetAddressHandler( "/CubeXYZ" , OnReceiveXYZ);
        osc.SetAddressHandler("/1/faderA", OnReceivePosY);
        osc.SetAddressHandler("/1/rotaryA", OnReceiveRY);
        osc.SetAddressHandler("/message", OnReceive);
       //osc.SetAddressHandler("/CubeZ", OnReceiveZ);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnReceive(OscMessage message)
    {
        print(message.GetFloat(0));
    }


    void OnReceiveXYZ(OscMessage message){
		float x = message.GetFloat(0);
         float y = message.GetFloat(1);
		float z = message.GetFloat(2);

		transform.position = new Vector3(x,y,z);
	}

    void OnReceivePosY(OscMessage message) {
        float y = message.GetFloat(0);

        Vector3 position = transform.position;

        position.y = y;

        transform.position = position;
        Debug.Log("Y");
    }

    void OnReceiveRY(OscMessage message) {
        float rY = message.GetFloat(0);

        print(rY);

        //Quaternion cubeRotation = transform.rotation;

        transform.Rotate(new Vector3(rY,rY,rY));

       // cubeRotation.y = rY;

       // transform.rotation = cubeRotation;
    }

    void OnReceiveZ(OscMessage message) {
        float z = message.GetFloat(0);

        Vector3 position = transform.position;

        position.z = z;

        transform.position = position;
    }


}
