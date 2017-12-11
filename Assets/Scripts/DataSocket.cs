using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSocket : MonoBehaviour {

	int x;
	List<recVals> data;


//	var o = new MemoryStream(); //Create something to hold the data
//
//	var bf = new BinaryFormatter(); //Create a formatter
//	bf.Serialize(o, list); //Save the list
//	var data = Convert.ToBase64String(o.GetBuffer()); //Convert the data to a string
//
//
//	//Reading it back in
//	var ins = new MemoryStream(Convert.FromBase64String(data)); //Create an input stream from the string
//	//Read back the data
//	var x : List.<SomeClass> = bf.Deserialize(ins);


	// Use this for initialization
	void Start () {
		x = 5;
		data = new List<recVals> ();
		//PhotonStream.isReading = false;
		//PhotonStream.isWriting = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//	[PunRPC]
//	public void getData(List<recVals> data){
//		Debug.Log ("getData called");
//		this.data = data;
//	}

	[PunRPC]
	public void getData(string data){
		Debug.Log ("getData called");
		decode (data);
		//this.data = data;
	}

	void decode(string data){
		
	}




//	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//	{
//		Debug.Log ("Serializing");
//		if (stream.isWriting) {
//			stream.SendNext (2);
//			Debug.Log ("writing to server");
//		} else {
//			Debug.Log ("reading from server");
//			x = (int)stream.ReceiveNext ();
//		}
//	}
}
