using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {

	public float selfDestroyDuration = 1;
	float selfDestroyTimeout;
	// Use this for initialization
	void Start () {
		this.selfDestroyTimeout = Time.time + this.selfDestroyDuration;

	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > this.selfDestroyTimeout)
		{
			GameObject.Destroy(this.gameObject);
			return;
		}
	}
}
