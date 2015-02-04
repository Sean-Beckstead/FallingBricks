using UnityEngine;
using System.Collections;

public class DropCar : MonoBehaviour {
		
	private float startingX;

	void Start() {
		startingX = transform.position.x;
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.y < -10) 
		{
			transform.position = new Vector2 (startingX, float.Parse(Random.Range (10, 30).ToString()));
			rigidbody2D.velocity = new Vector2(0f,0f);
		}
	}
}
