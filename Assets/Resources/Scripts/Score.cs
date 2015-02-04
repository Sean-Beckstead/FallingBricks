using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	private int currentScore; 
	// Use this for initialization
	void Start () {
		currentScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		currentScore += 2;
	}
}
