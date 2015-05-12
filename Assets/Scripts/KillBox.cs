using UnityEngine;
using System.Collections;

public class KillBox : MonoBehaviour 
{
	private GameController gameController;

	// Use this for initialization
	void Start () 
	{
		gameController = FindObjectOfType(typeof(GameController)) as GameController;
	}

	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			gameController.LevelLose();
		}
	}
}
