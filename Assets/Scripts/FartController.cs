using UnityEngine;
using System.Collections;

public class FartController : MonoBehaviour {

	private Transform spawnPoint;

	// Use this for initialization
	void Start () {
	
		spawnPoint = GameObject.Find("Baby").transform;
		transform.position = new Vector3( 
		                                 spawnPoint.position.x,
		                                 spawnPoint.position.y,  
		                                 transform.position.z );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
