using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class GameController : MonoBehaviour {   

    // DEBUG
    public bool d_WIN_LOSE_OFF;
    public bool d_DEBUG;


    // Parameters for Enemies
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;


    // Parameters for Player
    public float timeForWin = 30;
    public int  playerLives = 3;


    // GUI entities to give feedback for the user

    public Text stageText;
    public Text lifeText;
    public SpriteRenderer lifeDecal;
    public Text timerText;
    public Image boostBar;
    public Text  d_cooldownText;
    

    // Game play state
    public Text keyPromptText;
    public Text gameOverText;

    private SpriteRenderer[] _lifeDecals;

    // Internal attributes to keep track of the game state
	private bool  _gameHasStarted;
	private bool  _gameOver;
    private bool  _gameWin;
    private bool  _stageLose;
    private bool  _stageWin;

    private int   _score;
    private int   _lives;
    private float _timeLeftStage;
	private float _timeLeftTutorial;

    private string _Boss_Stage = "stage_5";
    private string _Story_Win  = "StoryWin";



    void Awake()
    {
        _lives = playerLives;
        LifeUpdate ( );

        // DontDestroyOnLoad( this );
    }

    void Start () 
	{
		_gameHasStarted = false;
        _gameOver  = false;
        _stageLose = false;
        _stageWin  = false;

        StageUpdate ( );

        keyPromptText.text = "";
        gameOverText.text = "";
        
		_timeLeftStage    = timeForWin;
		_timeLeftTutorial = startWait;
        TimeleftUpdate ( );

        StartCoroutine ( SpawnWaves () );
    }

    void Update () {

        // Process game time mechanics
        TimersUpdate ( );

        if ( !d_WIN_LOSE_OFF )
        {
            if ( _stageLose )
				StageLose ( );  // Player has lost all lives
            else if ( _stageWin )
                StageWin ( );
        }

		// DEBUG: keys
        if ( Input.GetKeyDown(KeyCode.Z) )
            StageWin ( );
        if ( Input.GetKeyDown(KeyCode.X) )
            StageNext ( );
        if ( Input.GetKeyDown(KeyCode.K) )
            LifeRemove ( ) ;
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
                // Instantiate( hazard, spawnPosition, spawnRotation );
				Destroy(Instantiate( hazard, spawnPosition, spawnRotation ),11);
                yield return new WaitForSeconds( spawnWait );
            }
            yield return new WaitForSeconds( waveWait );
            
			if (_stageLose || _timeLeftStage < 0 ) 
            {
                break;
            }
        }
    }


	public void DashCooldownUpdate(float value) 
    {
		boostBar.fillAmount = value;
        if (d_DEBUG)
			d_cooldownText.text = "t2Dash: " + value;
    }


    /// <summary>
	/// --- LIFE methods ---
	/// </summary>
    public void LifeRemove ( int lifeValue=1 ) {
		if ( _lives <=  0 ) 
            _stageLose = true;
		else
			_lives -= lifeValue;
		LifeUpdate ( );
    }


    void LifeInit() 
    {
        _lifeDecals[0] = lifeDecal;
        Vector3 pos = lifeDecal.transform.position;
        Vector3 max = lifeDecal.GetComponent<SpriteRenderer>().bounds.max;
        
        // TODO: IMPLEMENT ICONS TO REPRESENT LIFE COUNT.
        //for (int ii = 1; ii < playerLives; ii++)
        //{
        //    pos.x += max.x * ii;
        //    print(pos);
        //    _lifeDecals[ii] = Instantiate(lifeDecal, pos, Quaternion.identity) as GameObject;
        //}
    }


    void LifeUpdate() 
    {
        lifeText.text = "x" + _lives;
        //if ( _lifeDecals.Length < _lives )
        //    //TODO: IMPLEMENT ICONS TO REPRESENT LIFE COUNT.
        //    Destroy( _lifeDecals[_lives] );
    }



    /// <summary>
	/// --- TIMER methods --- 
	/// </summary>
    void TimersUpdate() 
    {
		if( _timeLeftTutorial > 0 )
		{
			_timeLeftTutorial = startWait - Time.time;
			return;
		}

		if ( _timeLeftStage > 0 )
		{
			_timeLeftStage -= Time.deltaTime;
			_timeLeftStage = timeForWin - Time.time;
		}

        TimeleftUpdate();
        _stageWin = ( _timeLeftStage <= 0 ) ;
    }


    void TimeleftUpdate() 
    {
		float tt = StageIsOver() ? 0.0f : _timeLeftStage;
        string floatToTime = string.Format(
            "{0:#0}:{1:00}.{2:0}",
            Mathf.Floor(tt / 60),     // minutes
            Mathf.Floor(tt) % 60,     // seconds
            Mathf.Floor((tt*10) % 10) // miliseconds
            );
        timerText.text = "t2Win: " + floatToTime;
    }

	
    /// <summary>
	/// --- STAGE methods 
	/// </summary>
    void StageUpdate()
    {
        stageText.text = "Stage " + Application.loadedLevel.ToString();
    }
    

    void StageWin()
    {
        gameOverText.text = "Stage " + Application.loadedLevel.ToString() + " Complete!";
        _stageWin = true;

        keyPromptText.text = "Press 'Space' to Continue";
        
        if ( Input.GetKeyDown(KeyCode.Space) ) 
            StageNext();
    }


    void StageNext() 
    {
        _stageWin = false;

        if (Application.loadedLevel == Application.levelCount - 1)
            Application.LoadLevel (Application.levelCount);
        else 
            Application.LoadLevel (Application.loadedLevel + 1);
    }


    public void StageLose() 
    {
        _stageLose = true;
        gameOverText.text = "Game Over!";
        keyPromptText.text = "Press 'R' for Restart";

        if (Input.GetKeyDown (KeyCode.R))
            Application.LoadLevel (Application.loadedLevel);
    }


    public bool StageIsOver() 
    {
        return _stageLose || _stageWin;
    }
    
}


 