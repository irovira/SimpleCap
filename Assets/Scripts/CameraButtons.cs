using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script should be attached to the mobile network global object

public class CameraButtons : MonoBehaviour {
	bool socketFound; 
	bool dataFound;
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
//		GameObject manObj = GameObject.FindGameObjectWithTag("RecordManager");
//		RecordManager man = manObj.GetComponent<RecordManager> ();
//		if (man)
//			man.Keep ();

		//GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
		//CameraRecord cam = camObj.GetComponent<CameraRecord> ();
		//cam.sendOutVals ();
		GameObject socket = GameObject.FindGameObjectWithTag("Socket");
		GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
		CameraRecord cam = camObj.GetComponent<CameraRecord> ();
		//List<recVals> val = cam.getVals();

		//TODO: Vector3 Data is not serializable FIX THIS
		//string data = cam.getValsString ();
		//Debug.Log (data);

		if (socket == null) {
			socketFound = false;
		} else {
			socketFound = true;
			PhotonView pv = socket.GetComponent<PhotonView> ();
			string data = cam.getValsString ();
			pv.RPC ("receiveString", PhotonTargets.All, data);
			//List<recVals> val = cam.getVals();
			//List<int> fuct = new List<int> ();
			//fuct.Add (1);

			//pv.RPC ("getData", PhotonTargets.All, fuct);
//			if (val != null) {
//				dataFound = true;
//				//string data = cam.getValsString ();
//				//pv.RPC ("getData", PhotonTargets.All, data);
//				//pv.RPC ("getData", PhotonTargets.All, fuct);
//			}
		}
	}

	void OnGUI() {
		GUILayout.Button (socketFound ? "Socket found" : "Socket NOT FOUND");
		GUILayout.Button (dataFound ? "data found" : "data NOT FOUND");

	}
}
