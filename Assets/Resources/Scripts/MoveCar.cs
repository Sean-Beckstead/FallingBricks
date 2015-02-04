using UnityEngine;
using System.Collections;

public class MoveCar : MonoBehaviour {

	private float speedPC = 10.0F;
	private float speedHandheld = 50.0F;
	private Vector2 moveDirection = Vector2.zero;
	private int currentScore;

	void Start()
	{
		currentScore = 0;
	}

	void Update() {
		currentScore -= 1;

		Rigidbody2D rigid = GetComponent<Rigidbody2D>();
		if (Application.platform == RuntimePlatform.Android) 
		{
			moveDirection = new Vector2 (Input.acceleration.x, 0);

			if (moveDirection.sqrMagnitude > 1) 
				moveDirection.Normalize ();
		
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= speedHandheld;
			rigid.velocity = Vector2.Lerp (rigid.velocity, moveDirection, speedHandheld * Time.deltaTime);
		}
		else
		{
			moveDirection = new Vector2 (Input.GetAxis ("Horizontal"), 0);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speedPC;
			rigid.velocity = Vector2.Lerp (rigid.velocity, moveDirection, speedPC * Time.deltaTime);
		}
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Enemy") 
		{

			Debug.Log ("Scored: " + currentScore);
			GameWait ();
			Application.LoadLevel (0);
		}


	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.name == "ScoringArea")
			currentScore += 2;
	}

	IEnumerator GameWait(){
		yield return new WaitForSeconds(1);
		yield break;
	}

}
