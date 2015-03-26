using UnityEngine;
using System.Collections;

public class ScoreUpdate : MonoBehaviour {
	public GameObject mainPlayer;
	// Use this for initialization
	void Start () {
		MoveBlock block = mainPlayer.GetComponent<MoveBlock> ();

	}
	
	// Update is called once per frame
	void Update () {
		MoveBlock block = mainPlayer.GetComponent<MoveBlock> ();

	}
}
