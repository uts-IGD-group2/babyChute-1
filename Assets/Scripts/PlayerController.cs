using UnityEngine;
using System.Collections;



[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, yMin, yMax;
}
	
public class PlayerController : MonoBehaviour
{
	public float gravity;
	public float playerSpeed;
	public float dashSpeed;
	public float dashCooldownPeriod;

	public Boundary boundary;

	// Internal attributes to keep track of the player state
	private float _dashNext;
	private float _dashCooldown;
	private float _playerSpeed;
	private float _gravity;

	private GameController gameController;

	void Start () {
		_gravity = gravity;
		_playerSpeed = playerSpeed;
		GetComponent<Rigidbody>().velocity = new Vector3(_gravity, 1, 1);
		gameController = FindObjectOfType(typeof(GameController)) as GameController;

	}

	void Update() {
		ProcessDash();

		// Player want's to use dash ability
		if (Input.GetKeyDown(KeyCode.Space) &&  Time.time > _dashNext) 
		{
			_dashNext = Time.time + dashCooldownPeriod;
			_playerSpeed *= dashSpeed;
			// TODO: have burp/fart effect
			//				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			//				GetComponent<AudioSource>().Play ();
		}

		else
			_playerSpeed = playerSpeed;
	}
	
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
//			float moveVertical   = Input.GetAxis ("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, 0.0f);
		GetComponent<Rigidbody>().velocity = movement * _playerSpeed;
		GetComponent<Rigidbody>().position = new Vector3
			(
				Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
				Mathf.Clamp (GetComponent<Rigidbody>().position.y, boundary.yMin, boundary.yMax),
				0.0f
				);
	
	}
	void ProcessDash() {
		_dashCooldown = _dashNext > Time.time ? _dashNext - Time.time : 0.0f;
		gameController.UpdateDashCooldown(_dashCooldown);
	}
	
}