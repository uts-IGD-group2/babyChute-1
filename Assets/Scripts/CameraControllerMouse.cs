using UnityEngine;
using System.Collections;

public class CameraControllerMouse : MonoBehaviour {

	public float speed = 1f;
	private Vector3 newPosition;

	// Use this for initialization
	void Start () {
		newPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// move camera down
		newPosition.y = transform.position.y - Time.deltaTime * speed;
		//print ("cam-pos" + newPosition);
		transform.position = newPosition;
	}
}
