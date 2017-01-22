using UnityEngine;
using UnityEngine.UI;

public class StartGameScreenController : MonoBehaviour
{
	[SerializeField]
	private CrossGameState.ScoreInfo _debugScoreInfo;

	[SerializeField] private Text highScoreText;


	public void Start()
	{
		var crossGameState = GameObject.FindObjectOfType<CrossGameState>();
		CrossGameState.ScoreInfo toShow = new CrossGameState.ScoreInfo();
		if(crossGameState != null)
		{
			toShow = crossGameState.GetHighScoreInfo();
		} else
		{
			Debug.LogWarning("Did not find cross game state");
			toShow = _debugScoreInfo;
		}
		SetHighScore(toShow);
	}

	[ContextMenu("SetHighScoreManually")]
	private void SetHighScoreDebug()
	{
		SetHighScore(_debugScoreInfo);
	}

	private void SetHighScore(CrossGameState.ScoreInfo toShow)
	{
		highScoreText.text = toShow.ScoreThisRun.ToString();
	}

	public void StartGame()
	{
			
	}

}
