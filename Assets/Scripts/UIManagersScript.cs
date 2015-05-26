using UnityEngine;
using System.Collections;

public class UIManagersScript : MonoBehaviour {
	//private Transform      Startscreen;
	private SpriteRenderer Story1;
	private SpriteRenderer Story2;
	private SpriteRenderer Story3;



	void Start()
	{
		Story1 = GameObject.Find ("Story1").GetComponent<SpriteRenderer>();
		Story2 = GameObject.Find ("Story2").GetComponent<SpriteRenderer>();
		Story3 = GameObject.Find ("Story3").GetComponent<SpriteRenderer>();
		
		this.enabled   = true;
		Story1.enabled = false;
		Story2.enabled = false;
		Story3.enabled = false;
	}

	public void RestartGame() 
	{
		Application.LoadLevel (2); // The first plyable scene
	}
	
	void StartStory () 
	{
		Invoke ("loadStory1", 0);
		Invoke ("loadStory2", 5);
		Invoke ("loadStory3", 10);
		Invoke ( "startGame", 15);
	}
	
	void loadStory1() {

		Story1.enabled = false;
		Story1.enabled = true;
	}
	
	void loadStory2() {
		Story1.enabled = false;
		Story2.enabled = true;
	}
	
	void loadStory3() {
		Story2.enabled = false;
		Story3.enabled = true;
	}

	void StartGame() {
		Application.LoadLevel(2);
	}
}

