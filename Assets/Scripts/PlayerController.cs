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
	public float invulnerableCooldownPeriod;

	public Boundary boundary;

	// Internal attributes to keep track of the player state
	private float _playerSpeed;
//	private float _gravity;

	private float _moveHorizontal;
	private float _moveVertical;

	private float _dashNext;
	private float _dashCooldown;

	private bool  _isInvulnerable;
	private float _invulnerableTime;


	private GameController gameController;


	void Start () 
	{
//		_gravity = gravity;
		_isInvulnerable = false;
		_playerSpeed = playerSpeed;
//		GetComponent<Rigidbody2d>().velocity = new Vector3(_gravity, 1, 1);
		gameController = FindObjectOfType(typeof(GameController)) as GameController;

	}
		
	void FixedUpdate ()
	{
		if ( gameController.isLevelOver() && !gameController.d_WIN_LOSE_OFF ) {
			_moveHorizontal = 0.0f;
			_moveVertical   = 0.0f;
		} else {
			_moveHorizontal = Input.GetAxis ("Horizontal");
			_moveVertical   = Input.GetAxis ("Vertical");
		}
//		print(_moveVertical + ", " + _moveHorizontal);


		Vector3 movement = new Vector3 (_moveHorizontal, _moveVertical*100, 0.0f);
		float spd = !_isInvulnerable ? _playerSpeed : _playerSpeed * 0.5f;
		GetComponent<Rigidbody2D>().velocity = movement * _playerSpeed;
		GetComponent<Rigidbody2D>().position = new Vector3 (
			Mathf.Clamp (GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax), 
			Mathf.Clamp (GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax),
			0.0f
			);

		// check after  player being hit
		if ( _isInvulnerable )
		{
			UpdateInvulnerability();
		}
	
	}


	void Update() 
	{
		UpdateDash();

		// Player want's to use dash ability
		if (_moveHorizontal > 0.1 || _moveHorizontal < -0.1) 
		{
			if (Input.GetKeyDown(KeyCode.Space) &&  Time.time > _dashNext) 
			{
				_dashNext = Time.time + dashCooldownPeriod;
				_playerSpeed *= dashSpeed;
				// TODO: have burp/fart effect
				//				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
				//				GetComponent<AudioSource>().Play ();
			}
		} else {
			_playerSpeed = playerSpeed;
		}

	}


	void OnTriggerEnter2D (Collider2D other)
	{
		print(other.tag);
		if(other.tag == "Enemy")
			if ( !_isInvulnerable )
				TakeHit();
	}
	

	// Custom methods
	void UpdateDash() 
	{
		_dashCooldown = _dashNext > Time.time ? _dashNext - Time.time : 0.0f;
		gameController.UpdateDashCooldown(_dashCooldown);
	}


	void TakeHit()
	{
		if ( !_isInvulnerable )
		{
			_isInvulnerable = true;
			gameController.RemoveLife();
		}
	}


	void UpdateInvulnerability()
	{
		_invulnerableTime += Time.time;

		if ( _invulnerableTime < 3f ) {
			float remainder = _invulnerableTime % 0.3f;
			GetComponent<Renderer>().enabled = remainder > 0.15f; 
		} else  {
			GetComponent<Renderer>().enabled = true;
			_isInvulnerable = false;
		}
	}

}