﻿using System;
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

	public GameObject teleportMarkerPrefab;

	public Camera hudCamera;

	private enum State
	{
		MOVE_PLAYER,
		ATTACK,
		CANNON,
		DEPTH_CHARGE,
	}

	private State state = State.MOVE_PLAYER;

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
		
		switch (state)
		{
				case State.MOVE_PLAYER:
					MovePlayer(ray);
					break;
				case State.ATTACK:
					Attack(ray);
					break;
				case State.CANNON:
					Cannon(ray);
					break;
				case State.DEPTH_CHARGE:
					DepthCharge(ray);
					break;
			default:
				throw new Exception("Unknown state transition:"+state);
		}
		


	}

	private void DepthCharge(Ray ray)
	{	
	}

	private void Cannon(Ray ray)
	{
	}

	private void Attack(Ray ray)
	{
	}

	private void MovePlayer(Ray ray)
	{
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 100)) //, LayerMask.NameToLayer("UI")))
		{
			if(hit.collider.tag == "Tower") {
				Debug.Log("Found object: " + hit.collider);
			}
			if(hit.collider.tag == "Tower")
			{
				var sourceTransform = VRPlayer.Instance.transform;

				var targetTransform = hit.collider.transform.parent;
				var targetTeleportMarker = GameObject.Instantiate(this.teleportMarkerPrefab, targetTransform.position, Quaternion.identity);
				var sourceTeleportMarker = GameObject.Instantiate(this.teleportMarkerPrefab, sourceTransform.position, Quaternion.identity);
				// assume parent to Click collider is our object.
				var turret = targetTransform.GetComponent<Turret>();
				if (turret != null)
				{
					VRPlayer.TeleportTo(turret.teleportPoint, () => {
						GameObject.Destroy(targetTeleportMarker);
						GameObject.Destroy(sourceTeleportMarker);
					});
				}
			}
		}
	}
}
