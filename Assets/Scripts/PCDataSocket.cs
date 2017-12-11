using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCDataSocket : Photon.MonoBehaviour {

	int x;

	// Use this for initialization
	void Start () {
		//PhotonStream.isReading = true;
		//PhotonStream.isWriting = false;

	}

	// Update is called once per frame
	void Update () {

	}



	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		Debug.Log ("Serializing");
		if (stream.isWriting) {
			stream.SendNext (1);
			Debug.Log ("writing to server");
		} else {
			Debug.Log ("reading from client");
			x = (int)stream.ReceiveNext ();

		}
	}
}
