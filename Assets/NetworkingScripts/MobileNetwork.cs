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
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	// TODO-2.a: the same as 1.b
	//   and join a room

	public override void OnJoinedLobby()
	{
		PhotonNetwork.JoinRoom("myRoom");
	}

	public override void OnJoinedRoom()
	{
		//GetComponent<MobileShooter>().Activate();
		//GetComponent<GyroController>().ControlledObject = GameObject.FindGameObjectWithTag("ARCamera");
	}


}

