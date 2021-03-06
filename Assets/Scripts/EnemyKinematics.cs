﻿using UnityEngine;
using System.Collections;

public class EnemyKinematics : MonoBehaviour {
	
	// Use this for initialization
	public float speed = 1;
	private Sprite initSpriteFrame;
	public static EnemyKinematics main;
    private GameController Game_Ctrl;

	public bool isHit; 


	void Start () 
	{
//		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
//		GetComponent<Rigidbody2D>().velocity = transform.up * speed;
		isHit = false;
        Game_Ctrl = FindObjectOfType(typeof(GameController)) as GameController;
	}

	void Awake () 
	{
		main = this;    
	}


	void Update () 
	{
		if ( !isHit )
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
			GetComponent<Rigidbody2D>().velocity = transform.up * speed;
		}
	}



}
