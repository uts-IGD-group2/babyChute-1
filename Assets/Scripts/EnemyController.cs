using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class EnemyController : MonoBehaviour {	

    // Paramaters for Enemies
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

    private GameController gameController;

	void Start () 
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
		StartCoroutine ( SpawnWaves() );
	}


    // Spawn enemies
	IEnumerator SpawnWaves ( ) 
	{
		yield return new WaitForSeconds( startWait );
		while ( true ) 
		{
			for ( int i = 0; i < hazardCount; i++ ) 
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

            if (gameController.StageIsOver()) 
				break;
		}
	}	
}


 