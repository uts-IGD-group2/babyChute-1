	using UnityEngine;
using System.Collections;

public class ForceWind : MonoBehaviour {
	public float amplitude = 0.1f;

	public enum Dir{Up, Right, Down, Left};
	public Dir direction_;

	Vector2 vec;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		print("Object entered trigger");
	}


	void OnTriggerStay2D(Collider2D other)
	{
        Debug.Log("other: " + other.tag);
		;

		if ( direction_ == Dir.Up )
			vec = Vector2.up;
		else if ( direction_ == Dir.Right )
			vec = Vector2.right;
		else if ( direction_ == Dir.Down )
			vec = -Vector2.up;
		else if ( direction_ == Dir.Left )
			vec = -Vector2.right;
		other.GetComponent<Rigidbody2D>().AddForce(vec * amplitude * Time.time);

		Vector2 position = transform.position;
		Vector2 targetPosition = position -  other.GetComponent<Rigidbody2D>().position;
		Vector2 direction = targetPosition - position;
		direction.Normalize();
		int moveSpeed = 10;
		other.GetComponent<Rigidbody2D>().position += direction * moveSpeed * Time.deltaTime;
	}




	void OnTriggerExit(Collider other)
	{
		print("Object left the trigger");
	}
}
