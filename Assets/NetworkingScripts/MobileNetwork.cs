using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileNetwork : Photon.PunBehaviour
{
	void Start(){
		PhotonNetwork.ConnectUsingSettings("0.1");
	}
	void OnGUI()
	{
		if (PhotonNetwork.lobby != null) {
			GUILayout.Label (PhotonNetwork.lobby.ToString ());
		} else {
			GUILayout.Label("we're fuckt");
		}

		if (PhotonNetwork.room != null) {
			GUILayout.Label (PhotonNetwork.room.ToString ());
		} else {
			GUILayout.Label("pls help me god");
		}

		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	// TODO-2.a: the same as 1.b
	//   and join a room

	public override void OnJoinedLobby()
	{
		PhotonNetwork.JoinRoom("room1");
	}

	public override void OnJoinedRoom()
	{
		//GetComponent<MobileShooter>().Activate();
		//GUILayout.Label (PhotonNetwork.room.ToString () + " Room Name: " + "myRoom");
		GetComponent<GyroController>().ControlledObject = GameObject.FindGameObjectWithTag("MainCamera");

	}


}

