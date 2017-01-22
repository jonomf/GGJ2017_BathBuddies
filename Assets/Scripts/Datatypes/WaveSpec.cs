using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveSpec {

	// start time in seconds.
	[Header("Start time of this wave")]
	public float StartTime_s; 
	

	[Header("Types and spawn weight of enemies to spawn")]
	public List<EnemySpec> EnemyWeights;

	public float DurationOfWave;
	
}

[System.Serializable]
public class EnemySpec {
	public enum SpawnCorner
	{
		NORTHEAST,
		NORTHWEST,
		SOUTHEAST,
		SOUTHWEST
	}

	public SpawnCorner spawnCorner;
	public GameObject prefab;
	[Header("Number of enemies to spawn")]
	public int NumEnemies;
	public AnimationCurve rampUpOfEnemiesInCurve;

	public bool underwater = false;
}
