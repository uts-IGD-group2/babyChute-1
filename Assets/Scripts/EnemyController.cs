using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	
	// Use this for initialization
	public float speed = 1;
	private Sprite initSpriteFrame;


	void Start () 
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
		GetComponent<Rigidbody2D>().velocity = transform.up * speed;
	}


	void Update () 
	{
		
	}

}
