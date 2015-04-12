
using UnityEngine;
using System.Collections;


public class EnemyController : MonoBehaviour {

	public float speed = -1;
	private Transform spawnPoint;


	void Start () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
		spawnPoint = GameObject.Find("spawnPoint").transform;
	}


	void Update () {
		// Update is called once per frame
	}

	void OnBecameInvisible() {
		// added to remove error displayed by Unity when you stop playing the scene
		if (Camera.main == null)
			return;

		float yMax = Camera.main.orthographicSize - 0.5f;
		transform.position = new Vector3( spawnPoint.position.x, 
		                                  Random.Range(-yMax, yMax), 
		                                  transform.position.z );
	}
}
