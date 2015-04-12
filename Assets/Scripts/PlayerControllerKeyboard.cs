using UnityEngine;
using System.Collections;


public class PlayerControllerKeyboard : MonoBehaviour {
	
	public float speed = 5f;
	public GameObject Character;
	private Vector2 moveDirection = Vector2.zero;
	public float gravity = 0;
	
	void Start () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(gravity, 1);


	}

	void Update() {
		float h = Input.GetAxis("Horizontal") * speed;
		
		Character.transform.Translate(h*Time.deltaTime,0,0);
		moveDirection.y -= gravity * Time.deltaTime;
		
		EnforceBounds();
	}
	
	
	private void EnforceBounds()
	{
		Vector3 newPosition = transform.position; 
		Camera mainCamera = Camera.main;
		Vector3 cameraPosition = mainCamera.transform.position;

		// check if player has gone out of bounds and invert the vector direction on the axis 
		float xDist = mainCamera.aspect * mainCamera.orthographicSize; 
		float xMax = cameraPosition.x + xDist;
		float xMin = cameraPosition.x - xDist;

		if ( newPosition.x < xMin || newPosition.x > xMax ) {
			newPosition.x = Mathf.Clamp( newPosition.x, xMin, xMax );
			moveDirection.x = -moveDirection.x;
		}

		transform.position = newPosition;
	}
	
}