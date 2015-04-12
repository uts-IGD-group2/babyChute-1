using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	float speed = 8.0f;
	public float moveSpeed;
	private Vector3 moveDirection;
	
	void Start () {
		moveDirection = Vector3.down;
	}

	void Update() {
		Vector3 currentPosition = transform.position;

		// Get mouse location
		if( Input.GetButton("Fire1") ) {
			Vector3 moveToward = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			moveDirection = moveToward - currentPosition;
			moveDirection.z = 0; 
			moveDirection.Normalize();
		}

		// move from current position to either mouse position or the previous vector
		Vector3 target = moveDirection * moveSpeed + currentPosition;
		transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );
		
		Vector3 ypos = new Vector3(Input.GetAxis("Vertical"), 0, 0);
		transform.position += ypos * speed * Time.deltaTime;

		// Don't alloy charactor to go out of bounds.
		EnforceBounds ();
	}
	
	private void EnforceBounds() {
		Vector3 newPosition = transform.position; 
		Camera mainCamera = Camera.main;
		Vector3 cameraPosition = mainCamera.transform.position;
		
		// get the vertical bounds
		float yDist = mainCamera.aspect * mainCamera.orthographicSize; 
		float yMax = cameraPosition.y + yDist;
		float yMin = cameraPosition.y - yDist;
		
		// check if player has gone out of bounds and invert the vecot direction on the axis 
		if ( newPosition.y < yMin || newPosition.y > yMax ) {
			newPosition.y = Mathf.Clamp( newPosition.y, yMin, yMax );
			moveDirection.y = -moveDirection.y;
		}
		// get the horisontal bounds
		float xDist = mainCamera.orthographicSize;
		float xMax = cameraPosition.y + xDist;
		float xMin = cameraPosition.y - xDist;

		// check if player has gone out of bounds and invert the vecot direction on the axis 
		if (newPosition.x < xMax || newPosition.x > xMax) {
			newPosition.x = Mathf.Clamp( newPosition.x, xMax, xMax );
			moveDirection.x = -moveDirection.x;
		}

		transform.position = newPosition;
	}
}

