using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {	

	// DEBUG
	public bool d_WIN_LOSE_OFF;

	// Paramaters for Enemies
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	// Paramaters for the Player
	public float timeToWin;
	public int playerLives;


	// GUI entities to give feedback for the palyer
//	public GUIText scoreText;
	public GUIText livesText;
	public GUIText timerText;
	public GUIText dashCooldownText;


	// Game play state
	public GUIText restartText;
	public GUIText gameOverText;


	// Internal attributes to keep track of the game state
	private bool  _gameOver;
	private bool  _gameWin;
	private bool  _levelLose;
	private bool  _levelWin;
	private int   _currentLevel;

	private int   _score;
	private int   _lives;
	private float _timeLeft;



	void Start () {
		_gameOver  = false;
		_levelLose = false;
		_levelWin  = false;
		_currentLevel = 0;
		restartText.text = "";
		gameOverText.text = "";

		_score = 0;
		_lives = playerLives;
		_timeLeft = timeToWin;

//		UpdateScore ();
		UpdateLife ();
		UpdateTimeleft ();

//		SpawnWaves ();
		StartCoroutine ( SpawnWaves () );
	}

	void Update () {

		// Process game time mechanics
		UpdateTimers();


		if ( !d_WIN_LOSE_OFF )
		{
			// Player has lost all lives
			if (_levelLose)
				LevelLose();

			// Player has lasted past the time limit
			if(_timeLeft < 0) 
				LevelWin();
		}

	}

//	void SpawnWaves ()
//	{
//		GameObject hazard = hazards [Random.Range (0, hazards.Length)];
//		Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
//		Quaternion spawnRotation = Quaternion.identity;
//		Instantiate (hazard, spawnPosition, spawnRotation);
//	}

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
				Instantiate( hazard, spawnPosition, spawnRotation );
				yield return new WaitForSeconds( spawnWait );
			}
			yield return new WaitForSeconds( waveWait );
			
			if (_levelLose || _timeLeft < 0 ) 
			{
				break;
			}
		}
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
		if (_lives > 0)
		{
			_lives -= lifeValue;
			UpdateLife();
		}
		else
			_levelLose = true;
	}


	void UpdateLife () {
		livesText.text = "Lives x " + _lives;
	}


	void UpdateTimers() {
		_timeLeft = timeToWin - Time.time;
		UpdateTimeleft();
	}


	void UpdateTimeleft() {
		float tt = _levelLose ? 0.0f : _timeLeft;
		string floatToTime = string.Format(
			"{0:#0}:{1:00}.{2:0}",
			Mathf.Floor(tt / 60),//minutes
			Mathf.Floor(tt) % 60,//seconds
			Mathf.Floor((tt*10) % 10)//miliseconds
			);
		timerText.text = "t2Win: " + floatToTime;
	}


	void LevelWin () 
	{
		gameOverText.text = "Game Win!";
		_levelWin = true;

		restartText.text = "Press 'Space' to Continue";
		if ( Input.GetKeyDown(KeyCode.Space) ) 
		{
			// NextLevel();
		}

	}


	public void LevelLose() 
	{
		_levelLose = true;
		gameOverText.text = "Game Over!";
		restartText.text = "Press 'R' for Restart";
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			Application.LoadLevel (Application.loadedLevel);
		}
	}


	public bool isLevelOver() 
	{
		return _levelLose;
	}

	void NextLevel() 
	{
		_currentLevel++;
		Application.LoadLevel( "level_" + _currentLevel );
	}

}

