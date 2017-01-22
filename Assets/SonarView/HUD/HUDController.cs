using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUDController : MonoBehaviourSingleton<HUDController> {

	public enum CannonMode {
		Ballistic,
		Depth
	}

	public static CannonMode cannonMode { get; private set; }

	public Camera hudCamera;
	public void Start()
	{
		var cameraGO = GameObject.Find("SonarCamera");
		if(cameraGO == null)
		{
			Debug.LogWarning("Did not find the expected camera. attemping to fall back to main camera as the 2d camera");
			cameraGO = Camera.main.gameObject;
		}
		hudCamera = cameraGO.GetComponent<Camera>();
	}
	public void OnAttack()
	{
		Debug.Log("HUD attacked");
	}
	public void OnMovePlayer() {
		Debug.Log("HUD MovedPlayer");
	}

	public void OnReload() {
		//Debug.Log("OnMoveToggle: " + value);
	}
	public void OnCannonToggle(bool value) {
		if(value)			
			cannonMode = CannonMode.Ballistic;
		Debug.Log("OnCannonToggle: " + value);
	}
	public void OnDepthChargeToggle(bool value) {
		if(value)
			cannonMode = CannonMode.Depth;
		Debug.Log("OnDepthChargeToggle: " + value);
	}

	public void OnPanelClick(PointerEventData ped)
	{
		var ray = hudCamera.ScreenPointToRay(ped.position);
		Debug.Log (" ped.position = " + ped.position  + " ray = " + ray);

		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 100)) //, LayerMask.NameToLayer("UI")))
		{
			if(hit.collider.tag == "Tower")
			{
				// assume parent to Click collider is our object.
				var turret = hit.collider.transform.parent.GetComponent<Turret>();
				if (turret != null)
				{
					VRPlayer.TeleportTo(turret.teleportPoint);
				}
			}
		}
	}
}
