using UnityEngine;
using System.Collections;

public class StoryController2 : MonoBehaviour {

	private SpriteRenderer EndStory1;
	private SpriteRenderer EndStory2;
	private SpriteRenderer babySprite;
	private GameObject babyObject;
	private GameObject buttonCanvas;


	void Start () {
		EndStory1 = GameObject.Find ("EndStory1").GetComponent<SpriteRenderer> ();
		EndStory2 = GameObject.Find ("EndStory2").GetComponent<SpriteRenderer> ();
		babyObject = GameObject.Find ("babyparachute");
		babySprite = babyObject.GetComponent<SpriteRenderer> ();
		buttonCanvas = GameObject.Find ("Canvas");

		Invoke ("loadStory1", 0);
		Invoke ("move1", 0);
		Invoke ("loadStory2", 4);
	}

	void loadStory1() {
		buttonCanvas.SetActive(false);
		EndStory1.enabled = true;
		babySprite.enabled = true;
	}

	void loadStory2() {
		buttonCanvas.SetActive(true);
		babySprite.enabled = false;
		EndStory1.enabled = false;
		EndStory2.enabled = true;
	}

	void move1() {
		iTween.MoveTo (babyObject,new Vector3(0,-2,0),30);
	}

	void loadGame() {
		Application.LoadLevel ("stage_1");
	}
}
