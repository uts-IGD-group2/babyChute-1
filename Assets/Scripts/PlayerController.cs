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
	public float dashSpeed = 6;

	public  float dashSuckRate = 0.1f;

	public float invulnerableCooldownPeriod = 3.0f;

	public AudioClip babyLaugh;
	public AudioClip branchBreak;
	public AudioClip balloonPop;
	public AudioClip birdSqwark;
	public GameObject fart;
	public Boundary boundary;

	public float rainCooldown = 3f;
	private bool gotDiaper = false;
	// Internal attributes to keep track of the player state
    private bool _playerIsDashing;
	private bool rainSpeed = false;

	private float _moveHorizontal;
	private float _moveVertical;
	
	private float _dashPool;

	private bool  _isInvulnerable;
    private float _invulnerableCooldown;

	//private bool  _WetNappy;
	public bool _rainLevel;
	private float _rainEffect;

	private float speedMag;

    private GameController Game_Ctrl;

	void Awake () 
	{
		DontDestroyOnLoad (this);
	}

	void Start () 
	{
		_isInvulnerable = false;

		Game_Ctrl = FindObjectOfType( typeof(GameController) ) as GameController;

		if (Application.loadedLevel == 4) 
			BackgroundRepeater.main.scrollSpeed = 2;
	}
		
	void FixedUpdate ()
	{
		_moveHorizontal = Input.GetAxis ("Horizontal");
		_moveVertical   = Input.GetAxis ("Vertical");
        // Player want's to use dash ability
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if ( _moveHorizontal > 0.01 || _moveHorizontal < -0.01 )
				DashTry();
		}


        Vector3 movement = new Vector3(_moveHorizontal, _moveVertical * 5, 0.0f);
		// check to see if player is dashing
		speedMag = PlayerMagUpdate();

        GetComponent<Rigidbody2D>().velocity = movement * speedMag;
		GetComponent<Rigidbody2D>().position = new Vector3 (
			Mathf.Clamp (GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax), 
			Mathf.Clamp (GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax),
			0.0f
			);
	}


	void Update() 
	{
		// check invulnrability state
		if ( _isInvulnerable )
			InvulnerabilityUpdate();


		// Apply Rain level stage attributes.
		if (Application.loadedLevel == 4) {
			rainCooldown -= Time.deltaTime;

			if(rainSpeed == true)
			BackgroundRepeater.main.scrollSpeed += 0.01f;
		
			if (BackgroundRepeater.main.scrollSpeed >= 15)
				BackgroundRepeater.main.scrollSpeed = 15;

			if (rainCooldown <= 0) {
				//BackgroundRepeater.main.scrollSpeed += 0.1f;
				rainSpeed = true;
				EnemyKinematics.main.speed += 1;
				rainCooldown += Time.deltaTime + 1.0f;
			}
		}

		_dashPool += 0.01f;
		Game_Ctrl.DashUpdate(_dashPool);

		_rainEffect = _rainEffect + 0.05f;
	}


	void OnTriggerEnter2D (Collider2D other)
	{
        //if (Game_Ctrl.d_DEBUG)
            print("player trig: " + other.tag);

        if (other.tag == "Bird") {
			TakeHit ();
			Vector3 reflect = Vector3.Reflect( transform.position, Vector3.up ) * -100;
			other.GetComponent<EnemyKinematics>().isHit = true;
			other.GetComponent<Rigidbody2D>().AddForce(new Vector2(reflect.x, reflect.y));
			//other.GetComponent<Rigidbody2D> ().velocity = transform.up * -5;
			GetComponent<AudioSource> ().PlayOneShot (birdSqwark);
	
		} else if (other.tag == "Branch") {

			TakeHit ();
			GetComponent<AudioSource> ().PlayOneShot (branchBreak);
			Destroy (other.gameObject);
		} else if (other.tag == "Balloon") {
			
			TakeHit ();
			GetComponent<AudioSource> ().PlayOneShot (balloonPop);
			Destroy (other.gameObject);
		} else if (other.tag == "Diaper") {
			DiaperCollect (other);

		}
	}
	


	// Custom methods
	void DashTry()
	{
		if ( _dashPool >= 0.9 )
		{
			_dashPool = 0;
			Game_Ctrl.DashUpdate(_dashPool);
			
			GetComponent<AudioSource>().PlayOneShot(babyLaugh);
			Destroy(Instantiate(fart), 3);
			_playerIsDashing = true; 
		}
	}

	float PlayerMagUpdate()
    {


        if (Game_Ctrl.StageIsOver() && !Game_Ctrl.d_WIN_LOSE_OFF)
            return 0.0f;
        else if ( _isInvulnerable )
			return playerSpeed * 1.0f;

		_playerIsDashing =  _dashPool <= 0.33;
        float mag = _playerIsDashing ? dashSpeed : playerSpeed;
        return mag;
    }


	void TakeHit()
	{
		_isInvulnerable = true;
        _invulnerableCooldown = 0.0f;
        
        if (Game_Ctrl.d_DEBUG)  print("RemoveLife");
		Game_Ctrl.LifeRemove();
		InvulnerabilityUpdate();
	}


    void DiaperCollect(Collider2D other)
    {
        DestroyObject(other.gameObject);
		// reset difficultyå
		EnemyKinematics.main.speed = 2;
		BackgroundRepeater.main.scrollSpeed = 0;
//		rainCooldown = Time.deltaTime + 3f;
	}


    void RainCloudCollide(Collider2D other)
    {
        //TODO: make RainCloud rain
		//_WetNappy = true;
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
//        if (Game_Ctrl.d_DEBUG)  print("isInvulnerable: " + _isInvulnerable); 
	
    }

}