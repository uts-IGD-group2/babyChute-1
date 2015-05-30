using UnityEngine;
using System.Collections;

public class wind : MonoBehaviour {

	
	public float speed = 5.0f; 
	Vector3 vec;
	// Use this for initialization
	void Start () 
	{
		
		
		vec = new Vector3 (Random.Range (-4, 4), 5, 1);

		
	}
	

	
	void Update () 
	{
		transform.position += transform.forward *speed *Time.deltaTime;
	}
}