using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopCamera : MonoBehaviour {
	List<recVals> data;

	bool replaying = false;

	int replayFrame = 0;

	Transform tf;

	// Use this for initialization
	void Start () {
		tf = this.transform;
		data = new List<recVals> ();
	}

	void writeOut(){
		recVals[] dataWO = data.ToArray ();
		string jsonFile = JsonUtility.ToJson (dataWO, true);
		PlayerPrefs.SetString ("cameraData", jsonFile);
	}

	//GUI FUNCTIONS

	public void getMobileData(){
		GameObject socket = GameObject.FindGameObjectWithTag("Socket");
		data = socket.GetComponent<SendStringTest> ().getData ();
		Debug.Log ("getMobileData called");
		if(data != null){
			Debug.Log ("data not null");
			writeOut ();
		}
	}

	public void playMobileData(){
		replaying = true;
	}

	public void Stop(){
		replaying = false;
	}


	public void Replay(){
		if (!replaying) {
			return;
		}

		if (replayFrame >= data.Count) {
			replayFrame = 0;
			replaying = false;
			//comment this line out to clear the recording and start again
			//vals = new List<recVals>();
			return;
		}

		tf.position = data [replayFrame].position;
		tf.rotation = data [replayFrame].rotation;

		replayFrame++;
	}
		
	void OnGUI() {
		GUILayout.Button (replaying ? "Stop Replaying" : "Replay");
	}

	// Update is called once per frame
	void Update () {
		Replay ();
	}

}
