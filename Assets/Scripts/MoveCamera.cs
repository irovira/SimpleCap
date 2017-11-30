using System.Collections;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = new Vector3 (CnInputManager.GetAxis ("Horizontal"),
			CnInputManager.GetAxis ("Vertical"), CnInputManager.GetAxis ("Forward"));
		transform.Translate (movement);
	}
}
