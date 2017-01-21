using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalState : MonoBehaviour
{
	[Serializable]
	public class TrackedMaxInfo
	{
		public int MaxScoreThisRun = 0;
		public float MaxTimeAlive = 0f;
	}

	//FIXME: I think you can drag scenes as assets now...
	private string _gameOverSceneName = "GameOverScene";
	private string _gameStartSceneName = "GameStartScene";
	private string _mainGameScene = "GameScene";

	[SerializeField] private float _timeToShowGameOverScene;

	[SerializeField] //for debugging sake
	private TrackedMaxInfo maxPlayInfo = new TrackedMaxInfo();

	public void OnGameOver(TrackedMaxInfo lastPlayInfo)
	{
		if(maxPlayInfo.MaxScoreThisRun < lastPlayInfo.MaxScoreThisRun)
		{
			maxPlayInfo = lastPlayInfo;
		}
		StartCoroutine(showScoreAfterwards(lastPlayInfo));
	}

	IEnumerator showScoreAfterwards(TrackedMaxInfo scoreAfterPlaying)
	{
		SceneManager.UnloadSceneAsync(_mainGameScene);
		SceneManager.LoadScene(_gameOverSceneName, LoadSceneMode.Additive);
		yield return null; //I don't think this is needed anymore
#pragma warning "re-enable communication to scoreToShow"
		//GameObject.FindObjectOfType<GameOverScreen>().ScoreToShow(scoreAfterPlaying);
		//The scene should read from this, I suppose
		yield return new WaitForSeconds(_timeToShowGameOverScene);
		SceneManager.UnloadSceneAsync(_gameOverSceneName);
		SceneManager.LoadScene(_gameStartSceneName,LoadSceneMode.Additive);
	}

	public void OnStartNewGame()
	{
		SceneManager.UnloadSceneAsync(_gameStartSceneName);
		SceneManager.LoadScene(_mainGameScene);
	}

}
