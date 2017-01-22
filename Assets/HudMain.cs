using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnAttackToggle(bool value) {
		Debug.Log("OnAttackToggle: " + value);
	}
}
