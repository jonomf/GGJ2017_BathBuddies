using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class MainGame : MonoBehaviourSingleton<MainGame> {
	 
	public enum State {
		Playing = 0,
		Won = 1,
		Lost = 2,
	}

    [SerializeField] private Turret m_TurretPrefab;
	public List<WaveSpec> Waves = new List<WaveSpec>();
	public int WaveNumber = 0;
	public State GameState = State.Playing;

	public List<GameObject> SpawnPointsAir = new List<GameObject>();
	public List<GameObject> SpawnPointsUnderWater = new List<GameObject>();

    private Transform shipsContainer;

    public static Transform bulletsContainer;
    private Transform m_TowersContainer;

	public static float GameTime()
	{
		return Time.time;
	}
	
	private float _gameStartedTime;

	void Start ()
	{
		_gameStartedTime = Time.time;
        bulletsContainer = new GameObject("bullets-container").transform;
        m_TowersContainer = new GameObject("towers-container").transform;
        VRPlayer.TeleportTo(TurretManager.startingTurret.teleportPoint);
		
		FindObjectOfType<BaseStats>().OnDie += OnBaseDied;

        shipsContainer = new GameObject("ships-container").transform;
	}

	private void OnBaseDied(Stats stats)
	{
		TriggerLose();
	}

	public bool IsGameOver()
	{
		return GameState >= State.Won;
	}
	
	// Update is called once per frame
	void Update () {

		if(IsGameOver())
			return;
			
		if(WaveNumber >= Waves.Count)
		{
			TriggerWin();
			return;
		}

		var nextWave = Waves[WaveNumber];
		if(nextWave.StartTime_s <= GameTime())
		{
			TriggerNextWave();
		}
		//if(Waves[WaveNumber]
	}

	void SpawnEnemy(EnemySpec spec, Collider spawnBoundsAir, Collider spawnBoundsWater)
	{
		Collider spawnBounds = null;

		if(!spec.underwater)
		{
			spawnBounds = spawnBoundsAir;
		}
		else
		{
			spawnBounds = spawnBoundsWater;
		}

		var randomPos = RandomEx.InsideBounds(spawnBounds.bounds);
		Instantiate( spec.prefab, randomPos, Quaternion.identity).transform.parent = shipsContainer;
	}

	void TriggerNextWave()
	{
		Debug.Log("TriggerNextWave");
		var thisWave = Waves[WaveNumber];
		WaveNumber++;

		//todo: display Wave number to user.


		// find wave start. both air and water, since wave can be mixed.
		Collider spawnBoundAir = null;
		Collider spawnBoundWater = null;

		{
			var spawnPointGO = this.SpawnPointsAir[Random.Range(0, this.SpawnPointsAir.Count)];
			var allSpawns = spawnPointGO.GetComponentsInChildren<Collider>();
			spawnBoundAir = allSpawns[Random.Range(0, allSpawns.Length)];
		}
		{
			var spawnPointGO = this.SpawnPointsUnderWater[Random.Range(0, this.SpawnPointsUnderWater.Count)];
			var allSpawns = spawnPointGO.GetComponentsInChildren<Collider>();
			spawnBoundWater = allSpawns[Random.Range(0, allSpawns.Length)];
		}


		for(var i = 0; i < thisWave.NumEnemies; i++)
		{
			int totalWeight = thisWave.EnemyWeights.Sum(w => w.weight);
			int randomSelect = Random.Range(0, totalWeight);

			foreach(var e in thisWave.EnemyWeights)
			{
				if(randomSelect < e.weight)
				{
					SpawnEnemy(e, spawnBoundAir, spawnBoundWater);
				}

				randomSelect -= e.weight;

			}

		}

	}

	void TriggerWin()
	{
		GameState = State.Won;
		// todo;		
	}

	void TriggerLose()
	{
		GameState = State.Lost;
		GameObject.FindObjectOfType<CrossGameState>().OnGameOver(new CrossGameState.ScoreInfo() {ScoreThisRun = WaveNumber ,TimeAlive = Time.time - _gameStartedTime});
		// todo;
	}



}
