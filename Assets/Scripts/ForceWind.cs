	using UnityEngine;
using System.Collections;

public class ForceWind : MonoBehaviour {
	public float amplitude = 0.1f;

	public enum Dir{Up, Right, Down, Left};
	public Dir direction;

	Vector2 vec;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other)
	{
		Debug.Log("Object is in trigger");


		if ( direction == Dir.Up )
			vec = Vector2.up;
		else if ( direction == Dir.Right )
			vec = Vector2.right;
		else if ( direction == Dir.Down )
			vec = -Vector2.up;
		else if ( direction == Dir.Left )
			vec = -Vector2.right;
		other.GetComponent<Rigidbody2D>().AddForce(vec * amplitude * Time.time);
		
	}
}
