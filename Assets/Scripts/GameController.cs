using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {	

	// DEBUG
	public bool d_WIN_LOSE_OFF;
    public bool d_DEBUG;

	// Paramaters for Enemies
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	// Paramaters for Player
	public float timeForWin = 30;
	public int  playerLives = 3;


	// GUI entities to give feedback for the user
//	public GUIText scoreText;
    public GUIText stageText;
	public GUIText livesText;
    public GameObject lifeDecal;
	public GUIText timerText;
	public GUIText dashCooldownText;
    

	// Game play state
	public GUIText restartText;
	public GUIText gameOverText;

    private GameObject[] _lifeDecals;

	// Internal attributes to keep track of the game state
	private bool  _gameOver;
	private bool  _gameWin;
	private bool  _stageLose;
	private bool  _stageWin;
	private int   _currentstage;

	private int   _score;
	private int   _lives;
	private float _timeLeft;


    void Awake()
    {
        _lives = playerLives;
        UpdateLife();
    }

	void Start () {
		_gameOver  = false;
		_stageLose = false;
		_stageWin  = false;

		_currentstage = 1;
        UpdateStage();

		restartText.text = "";
		gameOverText.text = "";

        //_score = 0;
		
        _timeLeft = timeForWin;

        // InitLife();
		
		UpdateTimeleft();

//		SpawnWaves ();
		StartCoroutine ( SpawnWaves () );
	}

	void Update () {

		// Process game time mechanics
		UpdateTimers();


		if ( !d_WIN_LOSE_OFF )
		{
			// Player has lost all lives
			if (_stageLose)
				StageLose();

			// Player has lasted past the time limit
			if(_timeLeft < 0) 
				StageWin();
		}

        if (Input.GetKeyDown(KeyCode.X))
        {
            DoStageWin();
        }

	}

    // Spawn enemies
	IEnumerator SpawnWaves () 
	{
		yield return new WaitForSeconds( startWait );
		while (true) 
		{
			for (int i = 0; i < hazardCount; i++) 
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 ( 
                     Random.Range( -spawnValues.x, spawnValues.x ), 
                     spawnValues.y, 
                     spawnValues.z
                     );
				Quaternion spawnRotation = Quaternion.identity;
				Destroy(Instantiate( hazard, spawnPosition, spawnRotation ),11);
				//Instantiate( hazard, spawnPosition, spawnRotation );
				yield return new WaitForSeconds( spawnWait );
			}
			yield return new WaitForSeconds( waveWait );
			
			if (_stageLose || _timeLeft < 0 ) 
			{
				break;
			}
		}
	}
	void OnTriggerExit (Collider other) 
	{
		Destroy(other.gameObject);
	}


//	public void AddScore (int newScoreValue) {
//		_score += newScoreValue;
//		UpdateScore ();
//	}
//	void UpdateScore () {
//		scoreText.text = "Score: " + _score;
//	}

	public void UpdateDashCooldown(float dashCooldown) 
	{
		dashCooldownText.text = "t2Dash: " + dashCooldown;
	}


	public void RemoveLife (int lifeValue=1) {
		if ( _lives <= 0 )
			_stageLose = true;
		else
        {
			_lives -= lifeValue;
			UpdateLife();
		}
	}


    void InitLife() 
    {

        _lifeDecals[0] = lifeDecal;
        Vector3 pos = lifeDecal.transform.position;
        Vector3 max = lifeDecal.GetComponent<SpriteRenderer>().bounds.max;

        for (int ii = 1; ii < playerLives; ii++)
        {
            pos.x += max.x * ii;
            print(pos);
            _lifeDecals[ii] = Instantiate(lifeDecal, pos, Quaternion.identity) as GameObject;
        }

    }

	void UpdateLife() 
    {
		livesText.text = "x " + _lives;
        //if ( _lifeDecals.Length < _lives )
        //    //TODO: IMPLEMENT ICONS TO REPRESENT LIFE COUNT.
        //    Destroy( _lifeDecals[_lives] );
	}


	void UpdateTimers() 
    {
        _timeLeft = timeForWin - Time.time;
		UpdateTimeleft();
	}


	void UpdateTimeleft() 
    {
		float tt = _stageLose ? 0.0f : _timeLeft;
		string floatToTime = string.Format(
			"{0:#0}:{1:00}.{2:0}",
			Mathf.Floor(tt / 60),//minutes
			Mathf.Floor(tt) % 60,//seconds
			Mathf.Floor((tt*10) % 10)//miliseconds
			);
		timerText.text = "t2Win: " + floatToTime;
	}


    void UpdateStage()
    {
        stageText.text = "Stage " + _currentstage;
    }

	void StageWin () 
	{
		gameOverText.text = "Stage Complete!";
		_stageWin = true;

		restartText.text = "Press 'Space' to Continue";
		if ( Input.GetKeyDown(KeyCode.Space) ) 
		{
			Nextstage();
		}

	}

	public void StageLose() 
	{
		_stageLose = true;
		gameOverText.text = "Game Over!";
		restartText.text  = "Press 'R' for Restart";
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			Application.LoadLevel (Application.loadedLevel);
		}
	}

	public bool isStageOver() 
	{
		return _stageLose;
	}

	void Nextstage() 
	{
		_currentstage++;
        Application.LoadLevel ("stage_" + _currentstage);
	}

    void DoStageWin()
    {

    }
}


 