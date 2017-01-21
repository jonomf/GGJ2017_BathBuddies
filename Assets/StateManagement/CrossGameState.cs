using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossGameState : MonoBehaviour
{
	[Serializable]
	public class ScoreInfo
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
	private ScoreInfo _highScoreInfo = new ScoreInfo();
	public ScoreInfo LastScore = new ScoreInfo();

	public ScoreInfo GetHighScoreInfo()
	{
		return _highScoreInfo;
	}

	public void OnGameOver(ScoreInfo lastPlayInfo)
	{
		LastScore = lastPlayInfo;
		if(_highScoreInfo.MaxScoreThisRun < lastPlayInfo.MaxScoreThisRun)
		{
			_highScoreInfo = lastPlayInfo;
		}
		
		StartCoroutine(showScoreAfterwards(lastPlayInfo));
	}

	private void unloadAndLoadScene(string toUnload, string toLoad)
	{
		SceneManager.UnloadSceneAsync(toUnload);
		SceneManager.LoadScene(toLoad, LoadSceneMode.Additive);
	}

	IEnumerator showScoreAfterwards(ScoreInfo scoreAfterPlaying)
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
