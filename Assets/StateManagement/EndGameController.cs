using UnityEngine;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{

	//does not report when done, the game controller manually times i tout

	[SerializeField]
	private CrossGameState.ScoreInfo _debugScoreInfo;

	[SerializeField] private Text timeAliveText;


	public void Start()
	{
		var crossGameState = GameObject.FindObjectOfType<CrossGameState>();
		CrossGameState.ScoreInfo toShow = new CrossGameState.ScoreInfo();
		if(crossGameState != null)
		{
			toShow = crossGameState.LastScore;
		} else
		{
			Debug.LogWarning("Did not find cross game state");
			toShow = _debugScoreInfo;
		}
		setScore(toShow);
	}

	[ContextMenu("SetScoreManually")]
	private void SetHighScoreDebug()
	{
		setScore(_debugScoreInfo);
	}

	private void setScore(CrossGameState.ScoreInfo toShow)
	{
		timeAliveText.text = toShow.TimeAlive.ToString();
	}


}
