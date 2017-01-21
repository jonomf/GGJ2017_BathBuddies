using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MainGame : MonoBehaviourSingleton<MainGame> {

	public enum State {
		Playing = 0,
		Won = 1,
		Lost = 2,
	}

	public List<WaveSpec> Waves = new List<WaveSpec>();
	public int WaveNumber = 0;
	public State GameState = State.Playing;

	public static float GameTime()
	{
		return Time.time;
	}

	// Use this for initialization
	void Start () {
		
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

	void SpawnEnemy(GameObject g)
	{
		GameObject.Instantiate(g);
	}

	void TriggerNextWave()
	{
		Debug.Log("TriggerNextWave");
		var thisWave = Waves[WaveNumber];
		WaveNumber++;

		//todo: display Wave number to user.

		for(var i = 0; i < thisWave.NumEnemies; i++)
		{
			int totalWeight = thisWave.EnemyWeights.Sum(w => w.weight);
			int randomSelect = Random.Range(0, totalWeight);

			foreach(var e in thisWave.EnemyWeights)
			{
				if(randomSelect < e.weight)
				{
					SpawnEnemy(e.prefab);
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
		// todo;
	}



}
