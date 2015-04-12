using UnityEngine;
using System.Collections;

public class CameraControllerKeyboard : MonoBehaviour {
	
	public float speed = 1f;
	private Vector3 newPosition;
	
	// Use this for initialization
	void Start () {
		newPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		newPosition.y = transform.position.y - Time.deltaTime * speed;
		transform.position = newPosition;
	}
}
