using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	
	// Use this for initialization
	public float speed = -1;
	private Transform spawnPoint;
	private float xRand = 5;
	private Sprite initSpriteFrame;

//	foreach Camera c in Camera.allCameras {
//		if (c.gameObject.name == "Main_Camera")
//			Camera mainCamera = c;
//	}
	
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
		initSpriteFrame = GetComponent<SpriteRenderer>().sprite;

		spawnPoint = GameObject.Find("SpawnPoint_Right").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D( Collider2D other ) {
		Application.LoadLevel("Lose");
	}

	void OnBecameInvisible() {
		SpawnObject();
		GetComponent<SpriteRenderer>().sprite = initSpriteFrame;
		print ( GetComponent<SpriteRenderer>().sprite.name );
	}


	void SpawnObject() {
		// This line added to remove error displayed by Unity when you stop playing the scene
		if (Camera.main == null)
			return;

		transform.position = new Vector3( spawnPoint.position.x + 2 + Random.Range(-xRand, xRand),
		                                  spawnPoint.position.y,  
		                                  0 );
	}

}
