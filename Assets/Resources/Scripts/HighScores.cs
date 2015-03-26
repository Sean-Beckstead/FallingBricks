using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class HighScores {

	public static ScoreRecord[] highScores = new ScoreRecord[5]; 

	public static void SaveScore(int newScore, string newName)
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file;

		if (File.Exists (Application.persistentDataPath + "/highScores.fbg")) 
		{
			Debug.Log ("Loading File");
			file = File.Open (Application.persistentDataPath + "/highScores.fbg", FileMode.Open);
			highScores = (ScoreRecord[])bf.Deserialize(file);
			file.Close ();

			int highScoresLength = 0;
			while (highScoresLength < 5)
			{
				if (highScores[highScoresLength] != null)
				{
					highScoresLength++;
				}
				else
				{
					break;
				}
			}

			if (highScoresLength >= 5)
			{
				int[] temp = new int[5];
				bool sorted = false;
				int x = 0;
				ScoreRecord c;

				while (!sorted)
				{
					if (highScores[x].getScore() < highScores[x+1].getScore())
					{
						c = highScores[x+1];
						highScores[x+1] = highScores[x];
						highScores[x] = c;
						x = 0;
					} 
					else 
					{
						x++;
					}

					if (x == 4)
						sorted = true;
				}
				if (highScores[x].getScore() < newScore)
				{
					highScores[x] = new ScoreRecord(newScore, newName);
				}
				for (int i = 4; i > 0; i--)
				{
					if (highScores[i].getScore() > highScores[i - 1].getScore())
					{
						c = highScores[i];
						highScores[i] = highScores[i-1];
						highScores[i-1] = c;
					}
				}

			}
			else
			{		
				ScoreRecord c;		
				highScores[highScoresLength] = new ScoreRecord(newScore, newName);
				for (int i = highScoresLength; i > 0; i--)
				{
					if (highScores[i].getScore() > highScores[i - 1].getScore())
					{
						c = highScores[i];
						highScores[i] = highScores[i-1];
						highScores[i-1] = c;
					}
				}
			}

		}
		else
		{
			highScores[0] = new ScoreRecord(newScore, newName);
		}


		file = File.Create(Application.persistentDataPath + "/highScores.fbg");
		bf.Serialize (file, HighScores.highScores);
		file.Close ();
	}

	public static void LoadScores()
	{
		if (File.Exists (Application.persistentDataPath + "/highScores.fbg")) 
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/highScores.fbg", FileMode.Open);
			highScores = (ScoreRecord[])bf.Deserialize (file);
			file.Close ();			


			for (int i = 0; i < highScores.Length; i++)
			{
				if (highScores[i] != null)
					Debug.Log("highscore: " + highScores[i].getScore());
			}

		}
		else
		{
			highScores[0] = null;
			highScores[1] = null;
			highScores[2] = null;
			highScores[3] = null;
			highScores[4] = null;
		}
	}

	public static ScoreRecord[] GetHighScores()
	{
		LoadScores ();

		return highScores;
	}

	public static int GetLowestScore()
	{
		LoadScores ();
		if (highScores[4] == null)
			return 0;
		else
			return highScores [4].getScore ();

	}

	public static int GetHighestScore()
	{
		LoadScores ();
		if (highScores.Length > 0)
			return highScores [0].getScore ();
		
		return 0;
	}

	[Serializable]
	public class ScoreRecord {
		private int score;
		private string scoredBy;
		
		public ScoreRecord(int newScore, string newScoredBy) 
		{
			score = newScore;
			scoredBy = newScoredBy;
		}
		
		public int getScore() 
		{
			return score;
        }
        
        public string getScoredBy() 
        {
            return scoredBy;
        }
	}

}
