using UnityEngine;
using System.Collections;

public class Generate : MonoBehaviour {
	public GameObject branches;
	private Transform spawnPoint;
	
	// Use this for initialization
	void Start()
	{

		InvokeRepeating("CreateObstacle", 1f, 1.5f);
		spawnPoint = GameObject.Find("SpawnPoint").transform;
	}
	
	void CreateObstacle()
	{
		Instantiate(branches);
	}


}
