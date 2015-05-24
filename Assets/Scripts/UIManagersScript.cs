using UnityEngine;
using System.Collections;

public class UIManagersScript : MonoBehaviour {
	public GameObject     startscreen;
	public SpriteRenderer frame1;
	public SpriteRenderer frame2;
	public SpriteRenderer frame3;


	void Start(){
		this.enabled = false;
		frame1.enabled = false;
		frame2.enabled = false;
		frame3.enabled = false;
	}

	public void RestartGame() {
		Application.LoadLevel (2);
	}
	
	public void StartStory() {
		this.enabled = true;
		startscreen.GetComponent<Renderer>().enabled = false;


		Invoke ("loadStory1", 0);
		Invoke ("loadStory2", 5);
		Invoke ("loadStory3", 10);
		Invoke ("StartGame", 15);
	}
	
	void loadStory1() {
		frame1.enabled = true;
	}
	
	void loadStory2() {
		frame1.enabled = false;
		frame2.enabled = true;
	}
	
	void loadStory3() {
		frame2.enabled = false;
		frame3.enabled = true;
	}
	
	void StartGame() {
		Application.LoadLevel (2);
	}
}