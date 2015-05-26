using UnityEngine;
using System.Collections;


public class StartScreenController : MonoBehaviour {
    private GameObject StartScreen;
    private GameObject Story1;
    private GameObject Story2;
    private GameObject Story3;


	void Start()
	{
        StartScreen = GameObject.Find("StartScreen");
		Story1 = GameObject.Find ("Story1");
		Story2 = GameObject.Find ("Story2");
		Story3 = GameObject.Find ("Story3");
		
		this.enabled   = true;
        Story1.SetActive(false);
        Story2.SetActive(false);
        Story3.SetActive(false);
	}


	public void RestartGame() 
	{
		Application.LoadLevel (2); // The first plyable scene
	}
	

	public void StartStory() 
	{

		Invoke ("loadStory1", 1);
		Invoke ("loadStory2", 5);
		Invoke ("loadStory3", 10);
        Invoke( "StartGame", 15);
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
	}

	void StartGame() {
        Application.LoadLevel(Application.loadedLevel + 1);
	}
}

