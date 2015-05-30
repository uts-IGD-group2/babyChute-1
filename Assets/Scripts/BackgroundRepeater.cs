using UnityEngine;
using System.Collections;

public class BackgroundRepeater : MonoBehaviour
{
	public float scrollSpeed;
	public float tileSizeY;
	public static BackgroundRepeater main;

	private Vector3 startPosition;
	
	void Start ()
	{
		startPosition = transform.position;
	}

	void Awake () 
	{
		main = this;    
	}
	
	void Update ()
	{
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeY);
		transform.position = startPosition + Vector3.up * newPosition;
	}
}