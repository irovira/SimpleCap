using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System;
using System.Text;

public class SendStringTest : MonoBehaviour {
	string msg;
	List<recVals> data;
	// Use this for initialization
	void Start () {
		//msg = "Lobby has not been contacted by player";
	}
	[PunRPC]
	public void receiveString(string message){
		Debug.Log ("message was received lol");

		msg = message;

		Debug.Log (msg);

		decode ();
	}

	void decode(){
		// Read back serialized string

		MemoryStream stream = new MemoryStream(Convert.FromBase64String(msg));
		BinaryFormatter bf = new BinaryFormatter();
		SurrogateSelector surrogateSelector = new SurrogateSelector();

		//make sure vector is serializable
		Vector3SerializationSurrogate vector3SS = new Vector3SerializationSurrogate();

		surrogateSelector.AddSurrogate(typeof(Vector3),new StreamingContext(StreamingContextStates.All),vector3SS);

		//make sure quaternion is serializable
		QuaternionSerializationSurrogate quatSS = new QuaternionSerializationSurrogate();

		surrogateSelector.AddSurrogate(typeof(Quaternion),new StreamingContext(StreamingContextStates.All),quatSS);
		bf.SurrogateSelector = surrogateSelector;
		data = (List<recVals>)bf.Deserialize(stream);
		Debug.Log ("message was decoded");
	}



	public string getString(){
		return msg;
	}

	public List<recVals> getData(){
		return data;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
