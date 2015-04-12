using UnityEngine;
using System.Collections;

public class BackgroundRepeater : MonoBehaviour {

	private Transform cameraTransform;
	private float backGroundHeight;

	// Use this for initialization
	void Start () {
		cameraTransform = Camera.main.transform;

		SpriteRenderer spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
		backGroundHeight = spriteRenderer.sprite.bounds.size.y;
	
	}
	
	// Update is called once per frame
	void Update () {
		if( (transform.position.y + backGroundHeight) < cameraTransform.position.y) {
			Vector3 newPos = transform.position;
			newPos.y += 2.0f * backGroundHeight; 
			transform.position = newPos;
		}
	}
}
