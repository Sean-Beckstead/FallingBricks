using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonScripts : MonoBehaviour {

	public void StartTheGame()
	{
		Application.LoadLevel (1);
	}

	public void ExitTheGame()
	{		
		Application.Quit ();
	}

	public void StartLeaderBoard()
	{		
		Application.LoadLevel (2);
	}

	public void ExitLeaderBoard()
	{		
		Application.LoadLevel (0);
	}

	public void SubmitScore()
	{		
		Text score = GameObject.FindGameObjectWithTag("HighScoreText").GetComponent<Text>();
		InputField name = GameObject.FindGameObjectWithTag ("HighScoreName").GetComponent<InputField>();
		int newScore = int.Parse (score.text.ToString ());

		HighScores.SaveScore(newScore, name.text);
		HighScores.LoadScores();
		GameObject screen = GameObject.FindGameObjectWithTag ("HighScoreScreen");

		if (screen != null)
			Destroy (screen);

		Instantiate(Resources.Load ("Prefabs/GameScreen"));
		Text finalScore = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
		finalScore.text = "Score: " + newScore;
	}
}
