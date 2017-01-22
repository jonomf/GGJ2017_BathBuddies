using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HealthWorldView : MonoBehaviour {

	[System.Serializable]
	public struct HealthState {
		public float healthRatioMax;
		public Material material;
	}

	public List<HealthState> healthStates = new  List<HealthState>();

	private float lastHealth = 1;

	public GameObject bar;

	public void SetHealth(float healthRatio)
	{
		// find the highest state that applies.
		HealthState currentState;
		currentState.material = null;
		foreach(var s in healthStates)
		{
			if(healthRatio <= s.healthRatioMax)
			{
				currentState = s;
				break;
			}
		}

		GetComponent<MeshRenderer>().material = currentState.material;


		bar.transform.localScale = Vector3.one;
		bar.transform.localPosition = new Vector3(0, -healthRatio/2, -0.001f);
		bar.transform.localScale = new Vector3(1, 1- healthRatio, 1);

		lastHealth = healthRatio;

	}

	public void Update()
	{
		var baseStats = GameObject.FindObjectOfType<BaseStats>();

		if(baseStats != null)
		{
			var normalizedHealth = baseStats.curHealth / (float)baseStats.health;
			var newHealth = Mathf.Clamp01(normalizedHealth);
			if(newHealth != this.lastHealth)
			{
				SetHealth(newHealth);
			}

		}
	}

}
