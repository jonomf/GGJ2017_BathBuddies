using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossGameState : MonoBehaviour
{
	[Serializable]
	public class TrackedMaxInfo
	{
		public int MaxScoreThisRun = 0;
		public float MaxTimeAlive = 0f;
	}

	//FIXME: I think you can drag scenes as assets now...

	[SerializeField]
	private string  _gameOverScene;
	[SerializeField]
	private string _gameStartScene;

	[SerializeField] private string _hudScene;
	[SerializeField]
	private string _mainGameScene;

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

	private void unloadAndLoadScene(string toUnload, string toLoad)
	{
		SceneManager.UnloadSceneAsync(toUnload);
		SceneManager.LoadScene(toLoad, LoadSceneMode.Additive);
	}

	IEnumerator showScoreAfterwards(TrackedMaxInfo scoreAfterPlaying)
	{
		SceneManager.UnloadSceneAsync(_hudScene);
		unloadAndLoadScene(_mainGameScene,_gameOverScene);
		yield return null; //I don't think this is needed anymore
#warning "re-enable communication to scoreToShow"
		//GameObject.FindObjectOfType<GameOverScreen>().ScoreToShow(scoreAfterPlaying);
		//The scene should read from this, I suppose
		yield return new WaitForSeconds(_timeToShowGameOverScene);
		unloadAndLoadScene(_gameOverScene,_gameStartScene);
	}

	public void OnStartNewGame()
	{
		unloadAndLoadScene(_gameStartScene, _mainGameScene);
		SceneManager.LoadScene(_hudScene);
	}

}
