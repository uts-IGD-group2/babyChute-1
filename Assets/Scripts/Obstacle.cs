using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
	private int speed = 4;

	// Use this for initialization
	void Start()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
		GetComponent<Rigidbody2D>().velocity = transform.up * speed;


	
	}
	

	void update()
	{
		// This line added to remove error displayed by Unity when you stop playing the scene

	}


}
