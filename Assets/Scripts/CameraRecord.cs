using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class recVals
{
	public Vector3 position;
	public Quaternion rotation;

	public recVals(Vector3 position, Quaternion rotation)
	{
		this.position = position;
		this.rotation = rotation;
	}
}

public class CameraRecord : MonoBehaviour {


	List<recVals> vals = new List<recVals>();

	bool recording = false;

	bool replaying = false;

	int replayFrame = 0;

	Transform tf;


	// Use this for initialization
	void Start () {
		tf = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Record ();
		Replay ();
	}

	public void setRecord(){
		recording = true;
		replaying = false;
		GetComponent<GyroController> ().enable ();
	}

	public void setStop(){
		recording = false;
		replaying = false;
		GetComponent<GyroController> ().enable ();
	}

	public void setReplay(){
		recording = false;
		replaying = true;
		GetComponent<GyroController> ().disable ();
	}
	void Record()
	{
		if (!recording)
			return;
		vals.Add (new recVals (tf.position, tf.rotation));
	}
	public void Replay(){
		if (!replaying) {
			GameObject camObj = GameObject.FindGameObjectWithTag("MainCamera");
			GyroController gyro = camObj.GetComponent<GyroController> ();
			if(gyro)
				gyro.enable ();
			return;
		}
			
		if (replayFrame >= vals.Count) {
			replayFrame = 0;
			replaying = false;
			//comment this line out to clear the recording and start again
			//vals = new List<recVals>();
			return;
		}

		tf.position = vals [replayFrame].position;
		tf.rotation = vals [replayFrame].rotation;

		replayFrame++;
	}

	void Stop(){
		if (recording)
			recording = false;
		if (replaying)
			replaying = false;
	}

	List<recVals> getVals(){
		return vals;
	}

	void OnGUI() {

		if (!replaying) {
			if (GUILayout.Button (recording ? "Stop Recording" : "Record"))
				recording = !recording;
		}

		if (!recording) {
			if (GUILayout.Button (replaying ? "Stop Replay" : "Replay"))
				replaying = !replaying;
		}

	}
}
