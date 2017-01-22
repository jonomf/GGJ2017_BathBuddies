using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpasher : MonoBehaviour {

	public GameObject splashEffectPrefab;

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Splash");

		var otherPos = other.transform.position;
		otherPos.y = 0;

		if(this.splashEffectPrefab != null)
		{
			GameObject.Instantiate(this.splashEffectPrefab, otherPos, Quaternion.identity);

		}
	}
}
