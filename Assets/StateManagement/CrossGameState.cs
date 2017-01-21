using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class CrossGameState : MonoBehaviour
{
	[Serializable]
	public class ScoreInfo
	{
		public int ScoreThisRun = 0;
		public float TimeAlive = 0f;
	}

	//FIXME: I think you can drag scenes as assets now...

	[SerializeField]
	private Object  _gameOverScene;
	[SerializeField]
	private Object _gameStartScene;

	[SerializeField] private Object _aiScene;
	[SerializeField] private Object _hudScene;
	[SerializeField]
	private Object _mainGameScene;

	[SerializeField] private float _timeToShowGameOverScene;

	[SerializeField] //for debugging sake
	private ScoreInfo _highScoreInfo = new ScoreInfo();
	public ScoreInfo LastScore = new ScoreInfo();
	public List<Object> mainScenes;
	private Action<Object> loadScene; //lol
	private Action<Object> unloadScene;  
	private void Awake()
	{
		mainScenes = new List<Object>() {_aiScene,_hudScene,_mainGameScene};
		loadScene = (Object scene) => SceneManager.LoadScene(scene.name,LoadSceneMode.Additive);
		unloadScene = (Object scene) => SceneManager.UnloadSceneAsync(scene.name);
	}

	public ScoreInfo GetHighScoreInfo()
	{
		return _highScoreInfo;
	}

	public void OnGameOver(ScoreInfo lastPlayInfo)
	{
		LastScore = lastPlayInfo;
		if(_highScoreInfo.ScoreThisRun < lastPlayInfo.ScoreThisRun)
		{
			_highScoreInfo = lastPlayInfo;
		}
		
		StartCoroutine(showScoreAfterwards(lastPlayInfo));
	}

	private void unloadAndLoadScene(Object toUnload, Object toLoad)
	{
		SceneManager.UnloadSceneAsync(toUnload.name);
		SceneManager.LoadScene(toLoad.name, LoadSceneMode.Additive);
	}

	private void MainSceneLoadOrUnload(bool load)
	{
		foreach (var scene in mainScenes)
		{
			if(load)
			{
				SceneManager.LoadScene(scene.name);
			} else
			{
				SceneManager.UnloadSceneAsync(scene.name);
			}
		}
	}
	IEnumerator showScoreAfterwards(ScoreInfo scoreAfterPlaying)
	{
		MainSceneLoadOrUnload(load: false);
		loadScene(_gameOverScene);
		yield return null; //I don't think this is needed anymore as a delay before getting gameoverscreen reference
#warning "re-enable communication to scoreToShow"
		//GameObject.FindObjectOfType<GameOverScreen>().ScoreToShow(scoreAfterPlaying);
		//The scene should read from this, I suppose
		yield return new WaitForSeconds(_timeToShowGameOverScene);
		unloadAndLoadScene(_gameOverScene,_gameStartScene);
	}

	public void OnStartNewGame()
	{
		unloadScene(_gameStartScene);
		MainSceneLoadOrUnload(load:true);  //NOTE: push/pop sets
	}

}
