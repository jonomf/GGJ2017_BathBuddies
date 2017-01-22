using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour {

	public void Start()
	{
		var cameraGO = GameObject.Find("Main CameraFor2d");
		if(cameraGO == null)
		{
			Debug.LogWarning("Did not find the expected camera. attemping to fall back to main camera as the 2d camera");
			cameraGO = Camera.main.gameObject;
		}
		GetComponent<Canvas>().worldCamera = cameraGO.GetComponent<Camera>();
	}
	public void OnAttack()
	{
		Debug.Log("HUD attacked");
	}
	public void OnMovePlayer() {
		Debug.Log("HUD MovedPlayer");
	}

	public void OnMoveToggle(bool value) {
		Debug.Log("OnMoveToggle: " + value);
	}
	public void OnCannonToggle(bool value) {
		Debug.Log("OnCannonToggle: " + value);
	}
	public void OnDepthChargeToggle(bool value) {
		Debug.Log("OnDepthChargeToggle: " + value);
	}
}
