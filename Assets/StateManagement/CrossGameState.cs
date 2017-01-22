using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class CrossGameState : MonoBehaviour
{
    private static CrossGameState s_Instance;
    public static bool skipIntro { get { return s_Instance.m_SkipIntro; } }

    [SerializeField] private bool m_SkipIntro;
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

    [SerializeField] private VRPlayer m_VRPlayerPrefab;

	[SerializeField] private float _timeToShowGameOverScene;

	[SerializeField] //for debugging sake
	private ScoreInfo _highScoreInfo = new ScoreInfo();
	public ScoreInfo LastScore = new ScoreInfo();
	[NonSerialized]
	public List<Object> mainScenes;
	private Action<Object> loadScene; //lol
	private Action<Object> unloadScene;  
	private void Awake()
	{
	    s_Instance = this;
		mainScenes = new List<Object>() {_aiScene,_hudScene,_mainGameScene};
	    Instantiate(m_VRPlayerPrefab.gameObject);
		loadScene = (Object scene) => SceneManager.LoadScene(scene.name,LoadSceneMode.Additive);
		unloadScene = (Object scene) => SceneManager.UnloadSceneAsync(scene.name);
	}

	private void Start()
	{
		loadScene(_gameStartScene);
	}

	public ScoreInfo GetHighScoreInfo()
	{
		return _highScoreInfo;
	}

	[ContextMenu("End game fake")]
	void fakeGameOver()
	{
		OnGameOver(new ScoreInfo()
		{
			ScoreThisRun =  Random.Range(1,100),
			TimeAlive = Random.Range(0f,100f)
		});
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

	private void MainSceneLoadOrUnload(bool load)
	{
		foreach (var scene in mainScenes)
		{
			if(load)
			{
				loadScene(scene);
			} else
			{
				unloadScene(scene);
			}
		}
	}
	IEnumerator showScoreAfterwards(ScoreInfo scoreAfterPlaying)
	{
		MainSceneLoadOrUnload(load: false);
		loadScene(_gameOverScene);
		yield return null; //I don't think this is needed anymore as a delay before getting gameoverscreen reference
//#warning "re-enable communication to scoreToShow"
//NOTE: the Endgamecontroller polls this, not the other way around
		//GameObject.FindObjectOfType<EndGameController>().ScoreToShow(scoreAfterPlaying);
		//The scene should read from this, I suppose
		yield return new WaitForSeconds(_timeToShowGameOverScene);
		unloadScene(_gameOverScene);
		loadScene(_gameStartScene);
	}
	[ContextMenu("Start game")]
	public void OnStartNewGame()
	{
		unloadScene(_gameStartScene);
		MainSceneLoadOrUnload(load:true);  //NOTE: push/pop sets
	}

}
