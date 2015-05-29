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
    public  float dashSpeed    = 6;

    public  float dashSuckRate = 10.0f;
    public  float dashRechargeRate = 10.0f;

    public float invulnerableCooldownPeriod = 3.0f;

    public AudioClip babyLaugh;
    public AudioClip branchBreak;
    public AudioClip birdSqwark;
    public GameObject fart;

    public Boundary boundary;

    // Internal attributes to keep track of the player state

    private float _moveHorizontal;
    private float _moveVertical;

	private float _dashPool = 100;
    private float _dashCooldown;

    private bool  _isInvulnerable;
  	private float _invulnerableCooldown;

    private bool  _WetNappy;

    private GameController Game_Ctrl;

    void Awake () 
    {
        DontDestroyOnLoad (this);
    }

    void Start () 
    {
        _isInvulnerable = false;
        Game_Ctrl = FindObjectOfType( typeof(GameController) ) as GameController;

    }
        
    void FixedUpdate ()
    {
        _moveHorizontal = Input.GetAxis ("Horizontal");
        _moveVertical   = Input.GetAxis ("Vertical");
        // Player want's to use dash ability
        if (Input.GetKeyDown(KeyCode.Space))
		{
			if ( _moveHorizontal > 0.01 || _moveHorizontal < -0.01 )
				DashDo();
		}

        Vector3 movement = new Vector3(_moveHorizontal, _moveVertical * 50, 0.0f);
        float speedMag = PlayerMagUpdate();

        GetComponent<Rigidbody2D>().velocity = movement * speedMag;
        GetComponent<Rigidbody2D>().position = new Vector3 (
            Mathf.Clamp (GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax), 
            Mathf.Clamp (GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax),
            0.0f
            );
    }


    void Update() 
    {
        // check invulnerability state
        if ( _isInvulnerable )
            InvulnerabilityUpdate();
    }


    void OnTriggerEnter2D (Collider2D other)
    {
        if (Game_Ctrl.d_DEBUG)
            print("player trig: " + other.tag);

        if (other.tag == "Bird") {
            TakeHit ();
			if ( other.GetComponent<EnemyEntity>() )
				other.GetComponent<EnemyEntity>().WackMe();

            
            GetComponent<AudioSource>().PlayOneShot(birdSqwark);
    
        }
		else if (other.tag == "Branch") 
		{
            TakeHit();
            GetComponent<AudioSource>().PlayOneShot(branchBreak);
			Destroy(other.gameObject);
        }
        else if (other.tag == "Balloon") 
		{
            TakeHit();
			if ( other.GetComponent<EnemyEntity>() )
				other.GetComponent<EnemyEntity>().PopMe();
        }
        else if ( other.tag == "Diaper" )
            DiaperCollect(other);
        
        else if ( other.tag == "RainCloud" )
            RainCloudCollide(other);

    }
    

    void DashDo()
    {
		if ( _dashPool >= 0 ) 
		{
			_dashPool -= dashSuckRate;

			Game_Ctrl.DashCooldownUpdate(_dashPool);
			
			GetComponent<AudioSource>().PlayOneShot(babyLaugh);
			Destroy(Instantiate(fart), 3);
		}
    }


	bool DashingIsPlayer ( )
	{
		return _dashCooldown > 0.01f;
	}


    float PlayerMagUpdate()
    {
        if (Game_Ctrl.StageIsOver() && !Game_Ctrl.d_WIN_LOSE_OFF)
            return 0.0f;
        else if ( _isInvulnerable )
            return 0.5f;
        else if ( _WetNappy )
            return 0.5f;

		float mag = DashingIsPlayer() ? dashSpeed : playerSpeed;
        return mag;
    }


    void TakeHit()
    {
		if (!_isInvulnerable)
		{
        	Game_Ctrl.LifeRemove();
			_invulnerableCooldown = 0.0f;

			if (Game_Ctrl.d_DEBUG)
				print ("RemoveLife");
		}

		_isInvulnerable = true;
        InvulnerabilityUpdate();
    }


    void DiaperCollect(Collider2D other)
    {
        DestroyObject(other.gameObject);
        _WetNappy = false;
        //TODO: remove negative effect
    }


    void RainCloudCollide(Collider2D other)
    {
        //TODO: make RainCloud rain
        _WetNappy = true;
    }


    void InvulnerabilityUpdate()
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
//        if (Game_Ctrl.d_DEBUG)  
//			print("isInvulnerable: " + _isInvulnerable); 
    
    }

}