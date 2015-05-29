using UnityEngine;
using System.Collections;

public class BackgroundRepeater : MonoBehaviour
{
	public float scrollSpeed;
	public float tileSizeY;

    public GameObject[] clouds;
    public Vector3 spawnPosRange;
    public int cloudCount = 10;
    public float spawnWait;

	public static BackgroundRepeater main;

	private Vector3 startPosition;
	
	void Start ()
	{
		startPosition = transform.position;

        StartCoroutine(SpawnClouds());
	}

	void Start (float speed)
	{
		scrollSpeed = speed;

		startPosition = transform.position;
		
		StartCoroutine(SpawnClouds());
	}
	
	void Update ()
	{
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeY);
		transform.position = startPosition + Vector3.up * newPosition;
	}

	void Awake () 
	{
		main = this;    
	}


    // Spawn enemies
    IEnumerator SpawnClouds()
    {
        while (true)
        {
            for (int i = 0; i < cloudCount; i++)
            {
                GameObject hazard = clouds[Random.Range(0, clouds.Length)];
                Vector3 spawnPosition = new Vector3(
                     Random.Range(-spawnPosRange.x, spawnPosRange.x),
                     spawnPosRange.y,
                     spawnPosRange.z
                     );
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
        }
    }
}