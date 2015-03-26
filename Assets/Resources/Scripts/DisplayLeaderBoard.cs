using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AssemblyCSharp;

public class DisplayLeaderBoard : MonoBehaviour {

	public Text score1;
	public Text score2;
	public Text score3;
	public Text score4;
	public Text score5;

	private HighScores.ScoreRecord[] scores;


	// Use this for initialization
	void Start () {
		scores = HighScores.GetHighScores ();

		if (scores[0] != null)
			score1.text = scores[0].getScoredBy() + ": " + scores[0].getScore();
		else
			score1.text = "";
		if (scores[1] != null)
			score2.text = scores[1].getScoredBy() + ": " + scores[1].getScore();
		else
			score2.text = "";
		if (scores[3] != null)
			score3.text = scores[2].getScoredBy() + ": " + scores[2].getScore();
		else
			score3.text = "";
		if (scores[3] != null)
			score4.text = scores[3].getScoredBy() + ": " + scores[3].getScore();
		else
			score4.text = "";
		if (scores[4] != null)
			score5.text = scores[4].getScoredBy() + ": " + scores[4].getScore();
		else
			score5.text = "";
	}

}
