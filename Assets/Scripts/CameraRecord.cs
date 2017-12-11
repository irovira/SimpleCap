using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System;
using System.Text;

[System.Serializable]
public class recVals
{
	[SerializeField]
	public Vector3 position;
	[SerializeField]
	public Quaternion rotation;

	public recVals(Vector3 position, Quaternion rotation)
	{
		this.position = position;
		this.rotation = rotation;
	}
}

public class CameraRecord : Photon.MonoBehaviour {


	List<recVals> vals = new List<recVals>();
	MemoryStream stream;

	bool recording = false;

	bool replaying = false;

	int replayFrame = 0;

	Transform tf;


	// Use this for initialization
	void Start () {
		tf = this.transform;
		stream = new MemoryStream ();
	}
	
	// Update is called once per frame
	void Update () {
		Record ();
		Replay ();
	}

	public void setRecord(){
		recording = true;
		replaying = false;
		//GetComponent<GyroController> ().enable ();
	}

	public void setStop(){
		recording = false;
		replaying = false;
		//GetComponent<GyroController> ().enable ();
	}

	public void setReplay(){
		recording = false;
		replaying = true;
		//GetComponent<GyroController> ().disable ();
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
			//GyroController gyro = camObj.GetComponent<GyroController> ();
//			if(gyro)
//				gyro.enable ();
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

	public List<recVals> getVals(){
		return vals;
	}

	public string getValsString(){

		//testing code from https://forum.unity.com/threads/vector3-is-not-marked-serializable.435303/
		BinaryFormatter bf = new BinaryFormatter();
		SurrogateSelector surrogateSelector = new SurrogateSelector();

		//make sure vector is serializable
		Vector3SerializationSurrogate vector3SS = new Vector3SerializationSurrogate();

		surrogateSelector.AddSurrogate(typeof(Vector3),new StreamingContext(StreamingContextStates.All),vector3SS);

		//make sure quaternion is serializable
		QuaternionSerializationSurrogate quatSS = new QuaternionSerializationSurrogate();

		surrogateSelector.AddSurrogate(typeof(Quaternion),new StreamingContext(StreamingContextStates.All),quatSS);

		//writing out to buffer based on https://answers.unity.com/questions/318593/using-rpc-to-send-a-list.html
		bf.SurrogateSelector = surrogateSelector;
		bf.Serialize (stream, vals);
		string data = Convert.ToBase64String (stream.GetBuffer ());
		return data;
	}

//	public void sendOutVals(){
//		photonView.RPC ("getData", PhotonTargets.MasterClient, vals);
//	}

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






	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
//		Debug.Log ("Serializing");
//		if (stream.isWriting) {
//			stream.SendNext (2);
//			Debug.Log ("writing to server");
//		} else {
//			Debug.Log ("reading from server");
//			int x = (int)stream.ReceiveNext ();
//		}
	}
}
