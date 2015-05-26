using UnityEngine;
using System.Collections;


public class StartScreenController : MonoBehaviour {
    public GameObject StartScreen;
    public GameObject Story1;
    public GameObject Story2;

    public GameObject Story3;
    public GameObject craneObject;
    public GameObject clothObject;

    Vector3[] keysFrames = 
        new[] { 
            new Vector3 ( 6, 0, 0 ), 
            new Vector3 ( 3,-8, 0 ) 
        };


	void Start()
	{
		this.enabled   = true;
        Story1.SetActive(false);
        Story2.SetActive(false);
        Story3.SetActive(false);
	}


	public void Play() 
	{

		Invoke ("loadStory1", 1);
		Invoke ("loadStory2", 5);
		Invoke ("loadStory3", 10);
        Invoke ("loadGame", 14);
	}	

	void loadStory1() {
        Story1.SetActive(true);
        StartScreen.SetActive(false);
	}
	
	void loadStory2() {
        Story2.SetActive(true);
        Story1.SetActive(false);
	}
	
	void loadStory3() {
        Story3.SetActive(true);
        Story2.SetActive(false);

        Invoke("move1", 0);
        Invoke("move2", 3);

        Invoke("StartGame", 5);
	}

    void move1() {
        iTween.MoveTo ( craneObject, keysFrames[0] , 20 );
    }

    void move2() {
        iTween.MoveTo( clothObject, keysFrames[1], 10);
    }

    public void RestartGame()
    {
        Application.LoadLevel(1);
    }

	void StartGame() {
        Application.LoadLevel(Application.loadedLevel + 1);
	}

}



//void Start () {

//        craneObject = GameObject.Find ("crane");
//        craneSprite = craneObject.GetComponent<SpriteRenderer> ();
//        clothObject = GameObject.Find ("cloth");
//        clothSprite = clothObject.GetComponent<SpriteRenderer> ();



//    void loadStory3() {
//        Story2.enabled = false;
//        Story3.enabled = true;

//        craneSprite.enabled = true;
//        clothSprite.enabled = true;
//    }


//    }

//}
