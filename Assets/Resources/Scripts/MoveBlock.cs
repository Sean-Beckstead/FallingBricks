using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveBlock : MonoBehaviour {

	private float speedPC = 10.0F;
	private float speedHandheld = 50.0F;
	private Vector2 moveDirection = Vector2.zero;
	private int currentScore, score;
	private float timer = 0.0f;
	private float colorTimer = 0.0f;
	private Color[] colorChoices = {Color.red, Color.blue, Color.green, Color.yellow};
	private bool canPlay;
	private ParticleSelector particleSystem;
	private Transform myTransform;
	private Color myColor;
	private bool colorChanging;
	
	private int pulseCount = 0;
	private int dir = -1;
	private float progress = 0.0f;
    
    public GameObject mainParticleSystem;
	public Text scoreText;
	public Light light1;
	public Light light2;
	public Light light3;

	void Start()
	{
		currentScore = 0;
		score = 0;
		renderer.material.color = colorChoices[Random.Range (0, colorChoices.Length)];
		canPlay = true;
		myTransform = transform;
		myColor = renderer.material.color;
		particleSystem = mainParticleSystem.GetComponent<ParticleSelector>();
		colorTimer = Random.Range (15.0f, 45.0f);
		colorChanging = false;
	}

	void Update() {
		Rigidbody2D rigid = GetComponent<Rigidbody2D>();

		if (canPlay) 
		{
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
				moveDirection = transform.TransformDirection (moveDirection);
				moveDirection *= speedPC;
				rigid.velocity = Vector2.Lerp (rigid.velocity, moveDirection, speedPC * Time.deltaTime);
			}
			timer += Time.deltaTime;
			if (timer >= 1.0f) 
			{
				if (currentScore > 1) 
				{
					score += 1;
				} 
				else 
				{
					score -= 5;
				}
					currentScore = 0;
					scoreText.text = "Score: " + score;
					timer = 0.0f;
			}			
			scoreText.text = "Score: " + score;

			if (colorTimer <= 0.0f)
			{
				colorChanging = true;
				StartChangeColor();
			}
			else
			{
				colorTimer -= Time.deltaTime;
			}
		}

	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Enemy" && canPlay) 
		{
			if (col.gameObject.renderer.material.color == myColor)
			{
				score += 50;
				col.gameObject.transform.position = new Vector2 (col.gameObject.transform.position.x, -9);
				particleSystem.PlayColor(myColor, myTransform.position);
			}
			else
			{
				canPlay = false;

				if (Application.isWebPlayer) 
				{
					Instantiate(Resources.Load ("Prefabs/GameScreen"));
					Text finalScore = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
					finalScore.text = "Score: " + score;
					return;
				}
				else if (HighScores.GetLowestScore() < score)
				{
					Instantiate(Resources.Load ("Prefabs/HighScoreScreen"));
					Text finalScore = GameObject.FindGameObjectWithTag("HighScoreText").GetComponent<Text>();
					finalScore.text = "" + score;
				}
				else
				{
					Instantiate(Resources.Load ("Prefabs/GameScreen"));
					Text finalScore = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
					finalScore.text = "Score: " + score;
				}
			}

		}
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.name == "ScoringArea")
			currentScore += 1;
	}

	private void ChangeColor()
	{
		Color newColor = colorChoices [Random.Range (0, colorChoices.Length)];
		while (newColor == myColor)
			newColor = colorChoices [Random.Range (0, colorChoices.Length)];

		myColor = newColor;
		renderer.material.color = myColor;
		colorChanging = false;
		colorTimer = Random.Range (15.0f, 45.0f);
	}

	private void StartChangeColor()
	{
		if (pulseCount >= 4)
		{
			pulseCount = 0;
			ChangeColor();
			return;
		}

		if (dir == -1)
		{
			Debug.Log("changing Light up:");
			progress += 0.05f;
			light1.intensity = progress;
			light2.intensity = progress;
			light3.intensity = progress;
		}
		else
		{
			Debug.Log("changing Light down:");
			progress -= 0.05f;
			light1.intensity = progress;
			light2.intensity = progress;
			light3.intensity = progress;
		}

		if (progress <= 0.0f)
		{
			Debug.Log("LightDirection changed to up:");
			dir *= -1;
			pulseCount += 1;
			Debug.Log("PulseCount: " + pulseCount);
		}

		if (progress >= 2.0f)
		{
			Debug.Log("LightDirection changed to down:");
			dir *= -1;
		}

	}
}
