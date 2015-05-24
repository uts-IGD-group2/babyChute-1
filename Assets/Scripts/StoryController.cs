using UnityEngine;
using System.Collections;

public class StoryController : MonoBehaviour {

	private SpriteRenderer Story1;
	private SpriteRenderer Story2;
	private SpriteRenderer Story3;

	void Start () {
		Invoke ("loadStory1", 0);
		Invoke ("loadStory2", 5);
		Invoke ("loadStory3", 10);
		Invoke ("startGame", 15);
	}

	void loadStory1() {
		Story1 = GameObject.Find ("Story1").GetComponent<SpriteRenderer> ();
		Story1.enabled = true;
	}

	void loadStory2() {
		Story1.enabled = false;
		Story2 = GameObject.Find ("Story2").GetComponent<SpriteRenderer> ();
		Story2.enabled = true;
	}

	void loadStory3() {
		Story2.enabled = false;
		Story3 = GameObject.Find ("Story3").GetComponent<SpriteRenderer> ();
		Story3.enabled = true;
	}

	void startGame() {
		Application.LoadLevel ("stage_1");
	}

}
