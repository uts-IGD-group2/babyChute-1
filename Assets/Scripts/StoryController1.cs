using UnityEngine;
using System.Collections;

public class StoryController1 : MonoBehaviour {

	private SpriteRenderer Story1;
	private SpriteRenderer Story2;
	private SpriteRenderer Story3;
	private SpriteRenderer craneSprite;
	private GameObject craneObject;
	private SpriteRenderer clothSprite;
	private GameObject clothObject;

	void Start () {
		Story1 = GameObject.Find ("Story1").GetComponent<SpriteRenderer> ();
		Story2 = GameObject.Find ("Story2").GetComponent<SpriteRenderer> ();
		Story3 = GameObject.Find ("Story3").GetComponent<SpriteRenderer> ();
		craneObject = GameObject.Find ("crane");
		craneSprite = craneObject.GetComponent<SpriteRenderer> ();
		clothObject = GameObject.Find ("cloth");
		clothSprite = clothObject.GetComponent<SpriteRenderer> ();


		Invoke ("loadStory1", 0);
		Invoke ("loadStory2", 5);
		Invoke ("loadStory3", 10);
		Invoke ("move1", 10);
		Invoke ("move2", 12);
		Invoke ("loadGame", 14);
	}

	void loadStory1() {
		Story1.enabled = true;
	}

	void loadStory2() {
		Story1.enabled = false;
		Story2.enabled = true;
	}

	void loadStory3() {
		Story2.enabled = false;
		Story3.enabled = true;

		craneSprite.enabled = true;
		clothSprite.enabled = true;
	}

	void move1() {
		iTween.MoveTo (craneObject,new Vector3(6,0,0),30);
		iTween.MoveTo (clothObject, new Vector3 (6, 0, 0), 25);
	}

	void move2() {
		iTween.MoveTo (clothObject, new Vector3 (3, -8, 0), 10);
	}

	void loadGame() {
		Application.LoadLevel ("stage_1");
	}
}
