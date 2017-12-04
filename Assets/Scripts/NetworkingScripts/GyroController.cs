﻿using UnityEngine;
using System.Collections;

public class GyroController : MonoBehaviour
{
	public GameObject ControlledObject
	{
		get { return controlledObject; }
		set
		{
			controlledObject = value;
			ResetOrientation();
		}
	}

	public bool Paused { get; set; }

	Quaternion qRefObject = Quaternion.identity;
	Quaternion qRefGyro = Quaternion.identity;
	Gyroscope gyro;

	GameObject controlledObject;

	void Awake()
	{
		Paused = false;
	}

	// Use this for initialization
	void Start()
	{
		gyro = Input.gyro;
		gyro.enabled = true;
		gyro.updateInterval = 0.01f;
	}

	void OnGUI()
	{
		/*
        GUILayout.Label("Gyroscope attitude : " + gyro.attitude);
        GUILayout.Label("Gyroscope gravity : " + gyro.gravity);
        GUILayout.Label("Gyroscope rotationRate : " + gyro.rotationRate);
        GUILayout.Label("Gyroscope rotationRateUnbiased : " + gyro.rotationRateUnbiased);
        GUILayout.Label("Gyroscope updateInterval : " + gyro.updateInterval);
        GUILayout.Label("Gyroscope userAcceleration : " + gyro.userAcceleration);
        GUILayout.Label("Ref camera rotation:" + qRefObject);
        GUILayout.Label("Ref gyro attitude:" + qRefGyro);
		GUILayout.Label("Current Rotation:" + controlledObject.transform.rotation);
		*/

	}

	// LOOK-1.d:
	// Converts the data returned from gyro from right-handed base to left-handed base.
	// Your device may require a different conversion
	private static Quaternion ConvertRotation(Quaternion q)
	{
		Quaternion quat = new Quaternion (q.x, q.y, -q.z, -q.w);
		quat = Quaternion.Euler (Vector3.right * 90) * quat;
		return quat;
	}


	// Update is called once per frame
	void Update()
	{
		int lmao = 1;
		//controlledObject = GameObject.FindGameObjectWithTag("cube");
		if (controlledObject != null && !Paused)
		{
			// TODO-1.d & TODO-2.a:
			//   rotate the camera or cube based on qRefObject, qRefGyro and current 
			//   data from gyroscope
			//Quaternion dR = Quaternion.Inverse(qRefGyro) * ConvertRotation(gyro.attitude);
			//Quaternion q = controlledObject.transform.rotation * dR;
			controlledObject.transform.rotation = ConvertRotation(gyro.attitude);
			//controlledObject.transform.rotation = gyro.attitude;
		}
	}

	public void ResetOrientation()
	{
		if (controlledObject == null)
		{
			return;
		}
		qRefObject = controlledObject.transform.rotation;
		qRefGyro = ConvertRotation(Input.gyro.attitude);
	}

	public void disable(){
		if (ControlledObject) {
			ControlledObject = null;
		}
		Paused = true;
		gyro.enabled = false;
	}

	public void enable(){
		if (ControlledObject == null) {
			ControlledObject = GameObject.FindGameObjectWithTag("MainCamera");
		}
		Paused = false;
		gyro.enabled = true;
		ResetOrientation ();
	}
	//// Possible helper function to smooth between gyro and Vuforia
	//public void UpdateOrientation(float deltatime)
	//{
	//        float smooth = 1f;
	//        qRefCam = Quaternion.Slerp(qRefCam, transform.rotation, smooth * deltatime);
	//        qRefGyro = Quaternion.Slerp(qRefGyro, ConvertRotation(gyro.attitude), smooth * deltatime);
	//    }
	//}

}
