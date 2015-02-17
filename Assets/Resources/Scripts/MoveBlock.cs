using UnityEngine;
using System.Collections;

public class MoveBlock : MonoBehaviour {

	private float speedPC = 10.0F;
	private float speedHandheld = 50.0F;
	private Vector2 moveDirection = Vector2.zero;
	private int currentScore, score;
	private float timer = 0.0f;
	private Color[] colorChoices = {Color.red, Color.blue, Color.green, Color.yellow};

	public GUIText scoreText;

	void Start()
	{
		currentScore = 0;
		score = 0;
		renderer.material.color = colorChoices[Random.Range (0, colorChoices.Length)];
	}

	void Update() {
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
		timer += Time.deltaTime;
		if (timer >= 1.0f) 
		{
			if ( currentScore > 1)
			{
				score += 1;
			} 
			else
			{
				score -= 1;
			}
			currentScore = 0;
			scoreText.text = "Score: " + score;
			timer = 0.0f;
		}

	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Enemy") 
		{
			if (col.gameObject.renderer.material.color == renderer.material.color)
			{
				score += 50;
				col.gameObject.transform.position = new Vector2 (col.gameObject.transform.position.x, float.Parse(Random.Range (10, 30).ToString()));
				col.gameObject.rigidbody2D.velocity = new Vector2(0f,0f);
				col.gameObject.renderer.material.color = colorChoices[Random.Range (0, colorChoices.Length)];
			}
			else
			{
				Debug.Log ("Scored: " + currentScore);
				GameWait ();
				Application.LoadLevel (0);
			}

		}
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.name == "ScoringArea")
			currentScore += 1;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.name == "ScoringArea")
			currentScore -= 100;
	}

	public int GetScore()
	{
		return currentScore;
	}

	IEnumerator GameWait(){
		yield return new WaitForSeconds(1);
		yield break;
	}

}
