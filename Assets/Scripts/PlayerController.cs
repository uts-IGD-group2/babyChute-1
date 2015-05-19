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
	public float dashCooldownPeriod = 2.0f;
	public float invulnerableCooldownPeriod = 3.0f;

	public Boundary boundary;

	// Internal attributes to keep track of the player state
	private float _playerSpeed;

	private float _moveHorizontal;
	private float _moveVertical;

	private float _dashNext;
	private float _dashCooldown;

	private bool  _isInvulnerable;
	private float _vulnerableNext;


	private GameController gameController;


	void Start () 
	{;
		_isInvulnerable = false;
        _vulnerableNext = 0.0f;
		_playerSpeed = playerSpeed;

		gameController = FindObjectOfType( typeof(GameController) ) as GameController;

	}
		
	void FixedUpdate ()
	{
		if ( gameController.isStageOver() && !gameController.d_WIN_LOSE_OFF ) {
			_moveHorizontal = 0.0f;
			_moveVertical   = 0.0f;
		} else {
			_moveHorizontal = Input.GetAxis ("Horizontal");
			_moveVertical   = Input.GetAxis ("Vertical");
		}
//		print(_moveVertical + ", " + _moveHorizontal);

		Vector3 movement = new Vector3 (_moveHorizontal, _moveVertical*100, 0.0f);
		float spd = !_isInvulnerable ? _playerSpeed : _playerSpeed * 0.5f;
		GetComponent<Rigidbody2D>().velocity = movement * spd;
		GetComponent<Rigidbody2D>().position = new Vector3 (
			Mathf.Clamp (GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax), 
			Mathf.Clamp (GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax),
			0.0f
			);
	}


	void Update() 
	{
		UpdateDash();

		// Player want's to use dash ability
		if (_moveHorizontal > 0.1 || _moveHorizontal < -0.1) 
		{
            if (Input.GetKeyDown(KeyCode.Space) && Time.deltaTime > _dashNext) 
                _playerSpeed = DoDash();
            else
                _playerSpeed = playerSpeed;
		} 
   


		// check invulnrability state
		if ( _isInvulnerable )
		{
			UpdateInvulnerability();
		}

	}


	void OnTriggerEnter2D (Collider2D other)
	{
        
        if (other.tag == "Enemy")
        {
            print("other " + other.tag);
            if (!_isInvulnerable)
                print("TakeHit");
            TakeHit();
        }
	}
	


	// Custom methods
	void UpdateDash() 
	{
        _dashCooldown = _dashNext > Time.deltaTime ? _dashNext - Time.deltaTime : 0.0f;
		gameController.UpdateDashCooldown(_dashCooldown);
	}

    float DoDash()
    {
        _dashNext = Time.deltaTime + dashCooldownPeriod;
        // TODO: have burp/fart effect
        //				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        //				GetComponent<AudioSource>().Play ()

        return _playerSpeed * dashSpeed;
    }

	void TakeHit()
	{
		_isInvulnerable = true;
        _vulnerableNext = Time.deltaTime + invulnerableCooldownPeriod;
		print("RemoveLife");
		gameController.RemoveLife();
	}


	void UpdateInvulnerability()
	{
		print("isInvulnerable: " + _isInvulnerable);
        _dashCooldown = _dashNext > Time.deltaTime ? _dashNext - Time.deltaTime : 0.0f;
        _vulnerableNext += Time.deltaTime;
        print("_invulnerableTime: " + _vulnerableNext + ", Period: " + invulnerableCooldownPeriod);
        if (_vulnerableNext < invulnerableCooldownPeriod)
        {
            float remainder = _vulnerableNext % 0.3f;
			GetComponent<Renderer>().enabled = remainder > 0.15f; 
		} 
        else  
        {
			GetComponent<Renderer>().enabled = true;
			_isInvulnerable = false;
		}                                                         
        print("isInvulnerable: " + _isInvulnerable); 
	
    }

}