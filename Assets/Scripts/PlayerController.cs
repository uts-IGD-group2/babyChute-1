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
	public float dashSpeed = 15;
	public float dashCooldownPeriod = 3.0f;
	public float invulnerableCooldownPeriod = 3.0f;

	public Boundary boundary;

	// Internal attributes to keep track of the player state
    private bool _playerIsDashing;

	private float _moveHorizontal;
	private float _moveVertical;

	private float _dashNext;
	private float _dashCooldown;

	private bool  _isInvulnerable;
    private float _invulnerableCooldown;

    private GameController Game_Ctrl;


	void Start () 
	{;
		_isInvulnerable = false;
		Game_Ctrl = FindObjectOfType( typeof(GameController) ) as GameController;

	}
		
	void FixedUpdate ()
	{
		_moveHorizontal = Input.GetAxis ("Horizontal");
		_moveVertical   = Input.GetAxis ("Vertical");
        // Player want's to use dash ability
        if (Input.GetKeyDown(KeyCode.Space))
            if (CanDash())
                StartDash();


        Vector3 movement = new Vector3(_moveHorizontal, _moveVertical * 50, 0.0f);
        float speedMag = UpdatePlayerMag();

        GetComponent<Rigidbody2D>().velocity = movement * speedMag;
		GetComponent<Rigidbody2D>().position = new Vector3 (
			Mathf.Clamp (GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax), 
			Mathf.Clamp (GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax),
			0.0f
			);
	}


	void Update() 
	{
		UpdateDash();

		// check invulnrability state
		if ( _isInvulnerable )
			UpdateInvulnerability();
	}


	void OnTriggerEnter2D (Collider2D other)
	{
        //if (Game_Ctrl.d_DEBUG)
            print("player trig: " + other.tag);

        if ( other.tag == "Enemy" || other.tag == "Branch" )
        {
            if( !_isInvulnerable )
            {
                if (Game_Ctrl.d_DEBUG) 
                    print("TakeHit");
                
                TakeHit();
            }
        }

        else if( other.tag == "Diaper" )
            GotDiaper(other);
        
        else if( other.tag == "RainCloud" )
            HitRainCloud(other);

	}
	


	// Custom methods
	void UpdateDash() 
	{
        _dashCooldown = _dashNext > Time.time ? _dashNext - Time.time : 0.0f;
        _playerIsDashing = ( _dashCooldown > 0.1f );

        Game_Ctrl.UpdateDashCooldown(_dashCooldown); 
	}


    bool CanDash()
    {
        if (_moveHorizontal > 0.01 || _moveHorizontal < -0.01) 
	    {
            if ( Time.time > _dashNext )
                return true;
        }
        return false;
    }


    void StartDash()
    {
        _dashNext = Time.time + dashCooldownPeriod;
        _playerIsDashing = true;

        // TODO: have burp/fart effect
        //				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        //				GetComponent<AudioSource>().Play ()
    }


    float UpdatePlayerMag()
    {
        if ( Game_Ctrl.isStageOver() && !Game_Ctrl.d_WIN_LOSE_OFF )
            return 0.0f;
        else if ( _isInvulnerable )
            return 0.5f;

        float mag = _playerIsDashing ? dashSpeed : playerSpeed;
        return mag;
    }


	void TakeHit()
	{
		_isInvulnerable = true;
        _invulnerableCooldown = 0.0f;
        
        if (Game_Ctrl.d_DEBUG)  print("RemoveLife");
        Game_Ctrl.RemoveLife();
        UpdateInvulnerability();
	}


    void GotDiaper(Collider2D other)
    {
        DestroyObject(other.gameObject);
        //TODO: remove negative effect
    }


    void HitRainCloud(Collider2D other)
    {
        //TODO: make RainCloud rain
    }


	void UpdateInvulnerability()
	{
        _invulnerableCooldown += Time.deltaTime;
        if ( _invulnerableCooldown < invulnerableCooldownPeriod)
        {
            float remainder = _invulnerableCooldown % 0.3f;
			GetComponent<Renderer>().enabled = remainder > 0.15f; 
		} 
        else  
        {
			GetComponent<Renderer>().enabled = true;
			_isInvulnerable = false;
		}
        if (Game_Ctrl.d_DEBUG)  print("isInvulnerable: " + _isInvulnerable); 
	
    }

}