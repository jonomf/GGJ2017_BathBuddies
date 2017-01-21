using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveSpec {

	// start time in seconds.
	[Header("Start time of this wave")]
	public float StartTime_s; 
	[Header("Number of enemies to spawn")]
	public int NumEnemies;

	[Header("Types and spawn weight of enemies to spawn")]
	public List<EnemySpec> EnemyPrefabs;
}

[System.Serializable]
public class EnemySpec {
	public GameObject prefab;
	public int weight = 1;
}
