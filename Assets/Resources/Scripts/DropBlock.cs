using UnityEngine;
using System.Collections;

public class DropBlock : MonoBehaviour {
		
	private float startingX;
	private Color blockColor;
	private Color[] colorChoices = {Color.red, Color.blue, Color.green, Color.yellow};

	void Start() {
		startingX = transform.position.x;
		renderer.material.color = colorChoices[Random.Range (0, colorChoices.Length)];
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.y < -10) 
		{
			transform.position = new Vector2 (startingX, float.Parse(Random.Range (10, 30).ToString()));
			rigidbody2D.velocity = new Vector2(0f,0f);
			renderer.material.color = colorChoices[Random.Range (0, colorChoices.Length)];
		}
	}
}
