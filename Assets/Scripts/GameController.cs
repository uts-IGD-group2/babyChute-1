using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {	

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
	private bool  _restart;
	private int   _currentLevel;
	private int   _score;
	private int   _lives;
	private float _timeLeft;



	void Start () {
		_gameOver = false;
		_restart  = false;
		_currentLevel = 0;
		restartText.text = "";
		gameOverText.text = "";

		_score = 0;
		_lives = playerLives;
		_timeLeft = timeToWin;
//		UpdateScore ();
		UpdateLife ();
		UpdateTimeleft ();
//		StartCoroutine ( SpawnWaves () );
	}

	void Update () {

		// Process game time mechanics
		ProcessTime();

		// Player has lost all lives
		if (_restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}

		// Player has lasted past the time limit
		if(_timeLeft < 0) {
			GameWin ();
		}

	}

	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
			if (_gameOver) {
				restartText.text = "Press 'R' for Restart";
				_restart = true;
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


	public void RemoveLife (int lifeValue) {
		_lives -= lifeValue;
		UpdateLife ();
	}
	void UpdateLife () {
		livesText.text = "Lives x " + _lives;
	}


	void ProcessTime() {
		_timeLeft = timeToWin - Time.time;
		UpdateTimeleft();
	}

	void UpdateTimeleft() {
		float tt = _gameOver ? 0.0f : _timeLeft;
		string floatToTime = string.Format(
			"{0:#0}:{1:00}.{2:0}",
			Mathf.Floor(tt / 60),//minutes
			Mathf.Floor(tt) % 60,//seconds
			Mathf.Floor((tt*10) % 10)//miliseconds
			);
		timerText.text = "t2Win: " + floatToTime;

	}
	
	public void GameOver () {
		gameOverText.text = "Game Over!";
		_gameOver = true;
	}

	public void GameWin () {
		gameOverText.text = "Game Win!";
//		Application.LoadLevel("Win");
		_gameOver = true;
	}

	public void UpdateDashCooldown(float dashCooldown) {
		dashCooldownText.text = "t2Dash: " + dashCooldown;
	}
}

