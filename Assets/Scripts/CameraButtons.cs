using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script should be attached to the mobile network global object

public class CameraButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void pressedRecord(){
		GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
		CameraRecord cam = camObj.GetComponent<CameraRecord> ();
		if (cam)
			cam.setRecord ();
	}

	public void pressedReplay(){
		GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
		CameraRecord cam = camObj.GetComponent<CameraRecord> ();
		if (cam) {
			
			GyroController gyro = camObj.GetComponent<GyroController> ();
			if (gyro)
				gyro.disable ();
			
			cam.setReplay ();
		}
	}

	public void pressedStop(){
		GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
		CameraRecord cam = camObj.GetComponent<CameraRecord> ();
		if (cam)
			cam.setStop ();
	}

	public void pressedKeep(){
		GameObject manObj = GameObject.FindGameObjectWithTag("RecordManager");
		RecordManager man = manObj.GetComponent<RecordManager> ();
		if (man)
			man.Keep ();
	}
}
