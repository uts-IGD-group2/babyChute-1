using UnityEngine;
using System.Collections;

public class SpawnEnity : MonoBehaviour {
	public GameObject[] entity;
	public Vector3 spawnPosRange;
	public int spawnCount = 10;
	public float spawnWait;


	// Use this for initialization
	void Start () {
		StartCoroutine(SpawnEntity());
	}
	
	// Spawn clouds
	IEnumerator SpawnEntity()
	{
		while (true)
		{
			for (int i = 0; i < spawnCount; i++)
			{
				GameObject hazard = entity[Random.Range(0, entity.Length)];
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
