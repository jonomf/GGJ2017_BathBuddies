using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class CrossGameState : MonoBehaviour
{
	private const string k_StartGameScene = "StartGame";
	private const string k_AISceneName = "AI";
	private const string k_HudScene = "HUD";
	private const string k_MainGameSceneName = "MainWorld";
	private const string k_GameOverSceneName = "EndGame";
	private const string k_GameStartSceneName = "StartGame";

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
	

    [SerializeField] private VRPlayer m_VRPlayerPrefab;
	[SerializeField] private AudioManager m_AudioManagerPrefab;

	[SerializeField] private float _timeToShowGameOverScene;

	[SerializeField] //for debugging sake
	private ScoreInfo _highScoreInfo = new ScoreInfo();
	public ScoreInfo LastScore = new ScoreInfo();
	[NonSerialized]
	public List<string> mainScenes;
	private Action<Object> loadScene; //lol
	private Action<Object> unloadScene;  
	private void Awake()
	{
	    s_Instance = this;
		mainScenes = new List<string>() {k_AISceneName,k_HudScene,k_MainGameSceneName};

		var player = Instantiate(m_VRPlayerPrefab.gameObject).transform;
		Instantiate(m_AudioManagerPrefab.gameObject, player);
	}

	private void Start()
	{
		SceneManager.LoadScene(k_StartGameScene, LoadSceneMode.Additive);
	}

	public ScoreInfo GetHighScoreInfo()
	{
		return _highScoreInfo;
	}
	[ContextMenu("fake kill base")]
	void fakeKillBase() {
		//GameObject.FindObjectOfType<BaseStats>().Attack()
		GameObject.FindObjectOfType<BaseStats>().curHealth = -1;
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
			if (load)
			{
				LoadScene(scene);
			} else
			{
				UnloadScene(scene);
			}
		}
	}

	void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
	}

	void UnloadScene(string sceneName)
	{
		SceneManager.UnloadScene(sceneName);
	}
	IEnumerator showScoreAfterwards(ScoreInfo scoreAfterPlaying)
	{
		MainSceneLoadOrUnload(load: false);
		LoadScene(k_GameOverSceneName);
		yield return null; //I don't think this is needed anymore as a delay before getting gameoverscreen reference
//#warning "re-enable communication to scoreToShow"
//NOTE: the Endgamecontroller polls this, not the other way around
		//GameObject.FindObjectOfType<EndGameController>().ScoreToShow(scoreAfterPlaying);
		//The scene should read from this, I suppose
		yield return new WaitForSeconds(_timeToShowGameOverScene);
		UnloadScene(k_GameOverSceneName);
		LoadScene(k_GameStartSceneName);
	}
	[ContextMenu("Start game")]
	public void OnStartNewGame()
	{
		SceneManager.UnloadSceneAsync(k_StartGameScene);
		MainSceneLoadOrUnload(load:true);  //NOTE: push/pop sets
	}

}
